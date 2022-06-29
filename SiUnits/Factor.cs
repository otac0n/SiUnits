// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// The base class for factors in SI Units.
    /// </summary>
    public abstract class Factor : IEquatable<Factor>
    {
        /// <summary>
        /// The list of factors to use if an empty list of factors is provided.
        /// </summary>
        protected static readonly ReadOnlyCollection<Factor> EmptyFactorList = new List<Factor> { Factors.One }.AsReadOnly();

        internal Factor()
        {
        }

        /// <summary>
        /// Multiplies two factors.
        /// </summary>
        /// <param name="left">The left factor to multiply.</param>
        /// <param name="right">The right factor to multiply.</param>
        /// <returns>The multiplied factor.</returns>
        public static Factor operator *(Factor left, Factor right) => Multiply(left, right);

        /// <summary>
        /// Divides one factor by another.
        /// </summary>
        /// <param name="left">The factor to divide.</param>
        /// <param name="right">The factor to divide by.</param>
        /// <returns>The combined factor.</returns>
        public static Factor operator /(Factor left, Factor right)
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
        public static Factor Multiply(params Factor[] factors)
        {
            var factorGroups = GroupFactors(factors);
            return factorGroups.Count == 1
                ? factorGroups.Values.Single()
                : new CompositeFactor(factorGroups);
        }

        /// <summary>
        /// Collapses a collection of factors into the simplest form.
        /// </summary>
        /// <param name="factors">The collection of factors to simplify.</param>
        /// <returns>A simplified list of factors.</returns>
        public static ReadOnlyCollection<Factor> SimplifyFactors(IEnumerable<Factor> factors)
        {
            var groups = GroupFactors(factors);
            var result = groups.Count == 0
                ? EmptyFactorList
                : groups.Values.ToList().AsReadOnly();
            return result;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Factor other && this.Equals(other);

        /// <inheritdoc/>
        public abstract bool Equals(Factor other);

        /// <inheritdoc/>
        public abstract override int GetHashCode();

        /// <summary>
        /// Raises the factor to the specified power.
        /// </summary>
        /// <param name="power">The power to which this factor should be raised.</param>
        /// <returns>A new factor equal to this factor raised to the specified power.</returns>
        public abstract Factor Pow(int power);

        /// <inheritdoc />
        public abstract override string ToString();

        /// <summary>
        /// Collapses a collection of factors into groups.
        /// </summary>
        /// <param name="factors">The collection of factors to simplify.</param>
        /// <returns>A collection of grouped factors.</returns>
        protected static Dictionary<object, Factor> GroupFactors(IEnumerable<Factor> factors)
        {
            var singleFactor = new Factor[1];
            var values = new Dictionary<object, Factor>();
            foreach (var factor in factors ?? Array.Empty<Factor>())
            {
                IEnumerable<Factor> subFactors;
                if (factor is CompositeFactor composite)
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
                        case NameFactor name:
                            if (name.Power != 0)
                            {
                                if (values.TryGetValue(name.Name, out var value))
                                {
                                    var newPower = ((NameFactor)value).Power + name.Power;
                                    if (newPower == 0)
                                    {
                                        values.Remove(name.Name);
                                    }
                                    else
                                    {
                                        values[name.Name] = new NameFactor(name.Name, newPower);
                                    }
                                }
                                else
                                {
                                    values[name.Name] = name;
                                }
                            }

                            break;

                        case NumberFactor number:
                            if (number.Number != 1 && number.Power != 0)
                            {
                                if (values.TryGetValue(number.Number, out var value))
                                {
                                    var newPower = ((NumberFactor)value).Power + number.Power;
                                    if (newPower == 0)
                                    {
                                        values.Remove(number.Number);
                                    }
                                    else
                                    {
                                        values[number.Number] = new NumberFactor(number.Number, newPower);
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
