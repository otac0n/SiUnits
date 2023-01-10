// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Represents a scalar value with units attached.
    /// </summary>
    /// <typeparam name="T">The type of value with units attached.</typeparam>
    public readonly struct ValueWithUnits<T> : IEquatable<ValueWithUnits<T>>
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

        public static ValueWithUnits<T> operator -(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value - right / left.Units, left.Units);

        public static ValueWithUnits<T> operator *(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value * right.Value, left.Units * right.Units);

        public static ValueWithUnits<T> operator *(ValueWithUnits<T> left, T right) =>
            new ValueWithUnits<T>(left.Value * right, left.Units);

        public static ValueWithUnits<T> operator *(T left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left * right.Value, right.Units);

        public static ValueWithUnits<T> operator /(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value / right.Value, left.Units / right.Units);

        public static ValueWithUnits<T> operator /(ValueWithUnits<T> left, T right) =>
            new ValueWithUnits<T>(left.Value / right, left.Units);

        public static ValueWithUnits<T> operator /(T left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left / right.Value, right.Units.Pow(-1));

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

        public static ValueWithUnits<T> operator +(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value + right / left.Units, left.Units);

        /// <inheritdoc/>
        public static bool operator ==(ValueWithUnits<T> left, ValueWithUnits<T> right) => left.Equals(right);

        /// <inheritdoc/>
        public static bool operator !=(ValueWithUnits<T> left, ValueWithUnits<T> right) => !(left == right);

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ValueWithUnits<T> other && this.Equals(other);

        /// <inheritdoc/>
        public bool Equals(ValueWithUnits<T> other)
        {
            var x = this / other;
            return x.Value == T.MultiplicativeIdentity && x.Units == Units<T>.Quantity.One;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return new CompositeFactor<T>(
                new NumberFactor<T>(T.CreateChecked(this.Value)),
                this.Units).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString() => $"{this.Value} {this.Units}";
    }
}
