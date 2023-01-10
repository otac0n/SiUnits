// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Represents a scalar value with units attached.
    /// </summary>
    /// <typeparam name="T">The type of value with units attached.</typeparam>
    public readonly struct ValueWithUnits<T>
        where T : IFloatingPoint<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueWithUnits{T}"/> struct.
        /// </summary>
        /// <param name="value">The scalar value.</param>
        /// <param name="units">The attached units.</param>
        public ValueWithUnits(T value, Units units)
        {
            this.Value = value;
            this.Units = units ?? throw new ArgumentNullException(nameof(units));
        }

        /// <summary>
        /// Gets the attached units.
        /// </summary>
        public Units Units { get; }

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

        public static T operator /(ValueWithUnits<T> left, Units right)
        {
            var units = left.Units / right;
            try
            {
                return left.Value * T.CreateChecked(units.Factors.AsConstant());
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Could not convert units of '{units}' to a constant.", ex);
            }
        }

        public static ValueWithUnits<T> operator +(ValueWithUnits<T> left, ValueWithUnits<T> right) =>
            new ValueWithUnits<T>(left.Value + right / left.Units, left.Units);

        /// <inheritdoc/>
        public override string ToString() => $"{this.Value} {this.Units}";
    }
}
