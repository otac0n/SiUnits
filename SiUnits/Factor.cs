// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The base class for factors in SI Units.
    /// </summary>
    /// <typeparam name="T">The underlying floating point representation for factors.</typeparam>
    public abstract class Factor<T> : IEquatable<Factor<T>>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        /// <summary>
        /// The list of factors to use if an empty list of factors is provided.
        /// </summary>
        protected static readonly ReadOnlyCollection<Factor<T>> EmptyFactorList = new List<Factor<T>> { Factors<T>.One }.AsReadOnly();

        internal Factor()
        {
        }

        public static explicit operator Factor<T>(string factors) => new Parser<T>().Parse(factors);

        public static ValueWithUnits<T> operator *(T value, Factor<T> units) => new ValueWithUnits<T>(value, units);

        public static ValueWithUnits<T> operator /(T value, Factor<T> units)
        {
            if (units is null)
            {
                throw new ArgumentNullException(nameof(units));
            }

            return new ValueWithUnits<T>(value, units.Pow(-1));
        }

        /// <summary>
        /// Multiplies two factors.
        /// </summary>
        /// <param name="left">The left factor to multiply.</param>
        /// <param name="right">The right factor to multiply.</param>
        /// <returns>The multiplied factor.</returns>
        public static Factor<T> operator *(Factor<T> left, Factor<T> right) => Multiply(left, right);

        public static bool operator ==(Factor<T> left, Factor<T> right) =>
            left is not null ? left.Equals(right) : right is null;

        public static bool operator !=(Factor<T> left, Factor<T> right) =>
            !(left == right);

        /// <summary>
        /// Divides one factor by another.
        /// </summary>
        /// <param name="left">The factor to divide.</param>
        /// <param name="right">The factor to divide by.</param>
        /// <returns>The combined factor.</returns>
        public static Factor<T> operator /(Factor<T> left, Factor<T> right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            var rightInverted = (right ?? throw new ArgumentNullException(nameof(right))).Pow(-1);

            return Multiply(left, rightInverted);
        }

        /// <summary>
        /// Multiplies a collection of factors.
        /// </summary>
        /// <param name="factors">The collection of factors to multiply.</param>
        /// <returns>The multiplied factor.</returns>
        public static Factor<T> Multiply(params Factor<T>[] factors)
        {
            var factorGroups = GroupFactors(factors);
            return factorGroups.Count == 1
                ? factorGroups.Values.Single()
                : new CompositeFactor<T>(factorGroups);
        }

        /// <summary>
        /// Collapses a collection of factors into the simplest form.
        /// </summary>
        /// <param name="factors">The collection of factors to simplify.</param>
        /// <returns>A simplified list of factors.</returns>
        public static ReadOnlyCollection<Factor<T>> SimplifyFactors(IEnumerable<Factor<T>> factors)
        {
            var groups = GroupFactors(factors);
            var result = groups.Count == 0
                ? EmptyFactorList
                : groups.Values.ToList().AsReadOnly();
            return result;
        }

        /// <summary>
        /// Converts the given factor into a constant value.  Only <see cref="NumberFactor{T}">number factors</see> or <see cref="CompositeFactor{T}">composite factors</see>
        /// containing number factors are supported.  To convert a value with additional factors, divide by the expected units first.
        /// </summary>
        /// <returns>The constant numeric value.</returns>
        public virtual T AsConstant() => throw new InvalidOperationException($"Could not convert factor of '{this}' to a constant.");

        /// <summary>
        /// Checks if the given factor is a constant value.  Only <see cref="NumberFactor{T}">number factors</see> or <see cref="CompositeFactor{T}">composite factors</see>
        /// containing number factors are supported.  To convert a value with additional factors, divide by the expected units first.
        /// </summary>
        /// <returns>A boolean indicating if the factor is a constant numeric value.</returns>
        public virtual bool IsConstant() => false;

        /// <summary>
        /// Checks if the given factor is a constant value.  Only <see cref="NumberFactor{T}">number factors</see> or <see cref="CompositeFactor{T}">composite factors</see>
        /// containing number factors are supported.  To convert a value with additional factors, divide by the expected units first.
        /// </summary>
        /// <param name="value">A variable that will be set to the constant numeric value.</param>
        /// <returns>A boolean indicating if the factor is a constant numeric value.</returns>
        public virtual bool IsConstant(out T value)
        {
            value = default;
            return false;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Factor<T> other && this.Equals(other);

        /// <inheritdoc/>
        public abstract bool Equals(Factor<T> other);

        /// <inheritdoc/>
        public abstract override int GetHashCode();

        /// <summary>
        /// Raises the factor to the specified power.
        /// </summary>
        /// <param name="power">The power to which this factor should be raised.</param>
        /// <returns>A new factor equal to this factor raised to the specified power.</returns>
        public abstract Factor<T> Pow(int power);

        /// <inheritdoc />
        public abstract override string ToString();

        /// <summary>
        /// Collapses a collection of factors into groups.
        /// </summary>
        /// <param name="factors">The collection of factors to simplify.</param>
        /// <returns>A collection of grouped factors.</returns>
        protected static Dictionary<object, Factor<T>> GroupFactors(IEnumerable<Factor<T>> factors)
        {
            var singleFactor = new Factor<T>[1];
            var values = new Dictionary<object, Factor<T>>();
            foreach (var factor in factors ?? Array.Empty<Factor<T>>())
            {
                IEnumerable<Factor<T>> subFactors;
                if (factor is CompositeFactor<T> composite)
                {
                    subFactors = composite.Factors;
                }
                else
                {
                    singleFactor[0] = factor;
                    subFactors = singleFactor;
                }

                foreach (var subFactor in subFactors)
                {
                    switch (subFactor)
                    {
                        case NameFactor<T> name:
                            if (name.Power != 0)
                            {
                                if (values.TryGetValue(name.Name, out var value))
                                {
                                    var newPower = ((NameFactor<T>)value).Power + name.Power;
                                    if (newPower == 0)
                                    {
                                        values.Remove(name.Name);
                                    }
                                    else
                                    {
                                        values[name.Name] = new NameFactor<T>(name.Name, newPower);
                                    }
                                }
                                else
                                {
                                    values[name.Name] = name;
                                }
                            }

                            break;

                        case NumberFactor<T> number:
                            if (number.Number != NumberFactor<T>.Unit.Number && number.Power != 0)
                            {
                                if (values.TryGetValue(number.Number, out var value))
                                {
                                    var newPower = ((NumberFactor<T>)value).Power + number.Power;
                                    if (newPower == 0)
                                    {
                                        values.Remove(number.Number);
                                    }
                                    else
                                    {
                                        values[number.Number] = new NumberFactor<T>(number.Number, newPower);
                                    }
                                }
                                else
                                {
                                    values[number.Number] = number;
                                }
                            }

                            break;

                        default:
                            throw new InvalidOperationException();
                    }
                }
            }

            return values;
        }
    }
}
