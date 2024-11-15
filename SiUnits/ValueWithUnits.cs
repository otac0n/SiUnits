// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Represents a scalar value with units attached.
    /// </summary>
    /// <typeparam name="T">The type of value with units attached.</typeparam>
    public readonly struct ValueWithUnits<T> :
        IEquatable<ValueWithUnits<T>>,
        IEqualityOperators<ValueWithUnits<T>, ValueWithUnits<T>, bool>,
        IAdditionOperators<ValueWithUnits<T>, ValueWithUnits<T>, ValueWithUnits<T>>,
        ISubtractionOperators<ValueWithUnits<T>, ValueWithUnits<T>, ValueWithUnits<T>>,
        IMultiplyOperators<ValueWithUnits<T>, T, ValueWithUnits<T>>,
        IDivisionOperators<ValueWithUnits<T>, T, ValueWithUnits<T>>,
        IMultiplyOperators<ValueWithUnits<T>, Factor<T>, ValueWithUnits<T>>,
        IDivisionOperators<ValueWithUnits<T>, Factor<T>, T>,
        IMultiplyOperators<ValueWithUnits<T>, ValueWithUnits<T>, ValueWithUnits<T>>,
        IDivisionOperators<ValueWithUnits<T>, ValueWithUnits<T>, ValueWithUnits<T>>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueWithUnits{T}"/> struct.
        /// </summary>
        /// <param name="value">The scalar value.</param>
        /// <param name="units">The attached units.</param>
        public ValueWithUnits(T value, Factor<T> units)
        {
            this.Value = value;
            this.Units = units ?? throw new ArgumentNullException(nameof(units));
        }

        /// <summary>
        /// Gets the attached units.
        /// </summary>
        public Factor<T> Units { get; }

        /// <summary>
        /// Gets the scalar value.
        /// </summary>
        public T Value { get; }

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator -(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value - right / left.Units, left.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked -(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(checked(left.Value - right / left.Units), left.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator *(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value * right.Value, left.Units * right.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked *(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            checked(new ValueWithUnits<T>(left.Value * right.Value, left.Units * right.Units));

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator *(ValueWithUnits<T> left, Factor<T> right) =>
            new ValueWithUnits<T>(left.Value, left.Units * right);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked *(ValueWithUnits<T> left, Factor<T> right) =>
            new ValueWithUnits<T>(left.Value, checked(left.Units * right));

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator *(ValueWithUnits<T> left, T right) =>
            new ValueWithUnits<T>(left.Value * right, left.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked *(ValueWithUnits<T> left, T right) =>
            new ValueWithUnits<T>(checked(left.Value * right), left.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator *(T left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left * right.Value, right.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked *(T left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(checked(left * right.Value), right.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator /(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value / right.Value, left.Units / right.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked /(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            checked(new ValueWithUnits<T>(left.Value / right.Value, left.Units / right.Units));

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator /(ValueWithUnits<T> left, T right) =>
            new ValueWithUnits<T>(left.Value / right, left.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked /(ValueWithUnits<T> left, T right) =>
            new ValueWithUnits<T>(checked(left.Value / right), left.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator /(T left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left / right.Value, right.Units.Pow(-1));

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked /(T left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(checked(left / right.Value), right.Units.Pow(-1));

        /// <inheritdoc/>
        public static T operator /(ValueWithUnits<T> left, Factor<T> right)
        {
            var units = left.Units / right;
            try
            {
                return left.Value * T.CreateChecked(units.AsConstant());
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Could not convert units of '{units}' to a constant.", ex);
            }
        }

        /// <inheritdoc/>
        public static T operator checked /(ValueWithUnits<T> left, Factor<T> right)
        {
            checked
            {
                var units = left.Units / right;
                try
                {
                    return left.Value * T.CreateChecked(units.AsConstant());
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException($"Could not convert units of '{units}' to a constant.", ex);
                }
            }
        }

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator +(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value + right / left.Units, left.Units);

        /// <inheritdoc/>
        public static ValueWithUnits<T> operator checked +(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(checked(left.Value + right / left.Units), left.Units);

        public static bool operator ==(ValueWithUnits<T> left, ValueWithUnits<T> right) => left.Equals(right);

        public static bool operator !=(ValueWithUnits<T> left, ValueWithUnits<T> right) => !(left == right);

        /// <summary>
        /// Converts the value with units into a constant value.  Only <see cref="NumberFactor{T}">number units</see> or <see cref="CompositeFactor{T}">composite units</see>
        /// containing number units are supported.  To convert a value with additional units, divide by the expected units first.
        /// </summary>
        /// <returns>The constant numeric value.</returns>
        public T AsConstant() => this.Value * this.Units.AsConstant();

        /// <summary>
        /// Checks if the given value with units is a constant value.  Only <see cref="NumberFactor{T}">number units</see> or <see cref="CompositeFactor{T}">composite units</see>
        /// containing number units are supported.  To convert a value with additional units, divide by the expected units first.
        /// </summary>
        /// <returns>A boolean indicating if the value is a constant numeric value (no units).</returns>
        public bool IsConstant() => this.Units.IsConstant();

        /// <summary>
        /// Checks if the given value with units is a constant value.  Only <see cref="NumberFactor{T}">number units</see> or <see cref="CompositeFactor{T}">composite units</see>
        /// containing number units are supported.  To convert a value with additional units, divide by the expected units first.
        /// </summary>
        /// <param name="value">A variable that will be set to the constant numeric value.</param>
        /// <returns>A boolean indicating if the value is a constant numeric value (no units).</returns>
        public bool IsConstant(out T value)
        {
            if (this.Units.IsConstant(out value))
            {
                value = this.Value * value;
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ValueWithUnits<T> other && this.Equals(other);

        /// <inheritdoc/>
        public bool Equals(ValueWithUnits<T> other)
        {
            return (this / other).IsConstant(out var value) && value == T.MultiplicativeIdentity;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => this.Units.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => $"{this.Value} {this.Units}";
    }
}
