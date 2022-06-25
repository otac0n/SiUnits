// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;

    /// <summary>
    /// Represents a scalar value with units attached.
    /// </summary>
    public struct ValueWithUnits
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueWithUnits"/> struct.
        /// </summary>
        /// <param name="value">The scalar value.</param>
        /// <param name="units">The attached units.</param>
        public ValueWithUnits(double value, Units units)
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
        public double Value { get; }

        public static ValueWithUnits operator -(ValueWithUnits left, ValueWithUnits right) =>
            new ValueWithUnits(left.Value - right / left.Units, left.Units);

        public static ValueWithUnits operator *(ValueWithUnits left, ValueWithUnits right) =>
            new ValueWithUnits(left.Value * right.Value, left.Units * right.Units);

        public static ValueWithUnits operator *(ValueWithUnits left, double right) =>
            new ValueWithUnits(left.Value * right, left.Units);

        public static ValueWithUnits operator *(double left, ValueWithUnits right) =>
            new ValueWithUnits(left * right.Value, right.Units);

        public static ValueWithUnits operator /(ValueWithUnits left, ValueWithUnits right) =>
            new ValueWithUnits(left.Value / right.Value, left.Units / right.Units);

        public static ValueWithUnits operator /(ValueWithUnits left, double right) =>
            new ValueWithUnits(left.Value / right, left.Units);

        public static ValueWithUnits operator /(double left, ValueWithUnits right) =>
            new ValueWithUnits(left / right.Value, right.Units.Pow(-1));

        public static double operator /(ValueWithUnits left, Units right)
        {
            var units = left.Units / right;
            try
            {
                return left.Value * units.Factors.AsConstant();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Could not convert units of '{units}' to a constant.", ex);
            }
        }

        public static ValueWithUnits operator +(ValueWithUnits left, ValueWithUnits right) =>
            new ValueWithUnits(left.Value + right / left.Units, left.Units);

        /// <inheritdoc/>
        public override string ToString() => $"{this.Value} {this.Units}";
    }
}
