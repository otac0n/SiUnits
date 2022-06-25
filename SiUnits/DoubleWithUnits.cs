// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;

    /// <summary>
    /// Represents a scalar value with units attached.
    /// </summary>
    /// <typeparam name="T">The type of value with units attached.</typeparam>
    public struct DoubleWithUnits<T>
        where T : IFloatingPoint<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleWithUnits{T}"/> struct.
        /// </summary>
        /// <param name="value">The scalar value.</param>
        /// <param name="units">The attached units.</param>
        public DoubleWithUnits(T value, Units units)
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

        public static DoubleWithUnits<T> operator -(DoubleWithUnits<T> left, DoubleWithUnits<T> right) =>
            new DoubleWithUnits<T>(left.Value - right / left.Units, left.Units);

        public static DoubleWithUnits<T> operator *(DoubleWithUnits<T> left, DoubleWithUnits<T> right) =>
            new DoubleWithUnits<T>(left.Value * right.Value, left.Units * right.Units);

        public static DoubleWithUnits<T> operator *(DoubleWithUnits<T> left, T right) =>
            new DoubleWithUnits<T>(left.Value * right, left.Units);

        public static DoubleWithUnits<T> operator *(T left, DoubleWithUnits<T> right) =>
            new DoubleWithUnits<T>(left * right.Value, right.Units);

        public static DoubleWithUnits<T> operator /(DoubleWithUnits<T> left, DoubleWithUnits<T> right) =>
            new DoubleWithUnits<T>(left.Value / right.Value, left.Units / right.Units);

        public static DoubleWithUnits<T> operator /(DoubleWithUnits<T> left, T right) =>
            new DoubleWithUnits<T>(left.Value / right, left.Units);

        public static DoubleWithUnits<T> operator /(T left, DoubleWithUnits<T> right) =>
            new DoubleWithUnits<T>(left / right.Value, right.Units.Pow(-1));

        public static T operator /(DoubleWithUnits<T> left, Units right)
        {
            var units = left.Units / right;
            try
            {
                return left.Value * T.Create(units.Factors.AsConstant());
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Could not convert units of '{units}' to a constant.", ex);
            }
        }

        public static DoubleWithUnits<T> operator +(DoubleWithUnits<T> left, DoubleWithUnits<T> right) =>
            new DoubleWithUnits<T>(left.Value + right / left.Units, left.Units);

        /// <inheritdoc/>
        public override string ToString() => $"{this.Value} {this.Units}";
    }
}
