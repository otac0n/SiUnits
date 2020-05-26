// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;

    public struct DoubleWithUnits
    {
        public DoubleWithUnits(double value, Units units)
        {
            this.Value = value;
            this.Units = units;
        }

        public Units Units { get; }

        public double Value { get; }

        public static DoubleWithUnits operator -(DoubleWithUnits left, DoubleWithUnits right) =>
            new DoubleWithUnits(left.Value - right / left.Units, left.Units);

        public static DoubleWithUnits operator *(DoubleWithUnits left, DoubleWithUnits right) =>
            new DoubleWithUnits(left.Value * right.Value, left.Units * right.Units);

        public static DoubleWithUnits operator *(DoubleWithUnits left, double right) =>
            new DoubleWithUnits(left.Value * right, left.Units);

        public static DoubleWithUnits operator *(double left, DoubleWithUnits right) =>
            new DoubleWithUnits(left * right.Value, right.Units);

        public static DoubleWithUnits operator /(DoubleWithUnits left, DoubleWithUnits right) =>
            new DoubleWithUnits(left.Value / right.Value, left.Units / right.Units);

        public static DoubleWithUnits operator /(DoubleWithUnits left, double right) =>
            new DoubleWithUnits(left.Value / right, left.Units);

        public static DoubleWithUnits operator /(double left, DoubleWithUnits right) =>
            new DoubleWithUnits(left / right.Value, right.Units.Pow(-1));

        public static double operator /(DoubleWithUnits left, Units right)
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

        public static DoubleWithUnits operator +(DoubleWithUnits left, DoubleWithUnits right) =>
            new DoubleWithUnits(left.Value + right / left.Units, left.Units);

        public override string ToString() => $"{this.Value} {this.Units}";
    }
}
