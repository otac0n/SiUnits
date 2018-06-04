// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

using System;

namespace SiUnits
{
    public struct DoubleWithUnits
    {
        public DoubleWithUnits(double value, Units units)
        {
            this.Value = value;
            this.Units = units;
        }

        public Units Units { get; }

        public double Value { get; }

        public static DoubleWithUnits operator *(DoubleWithUnits left, DoubleWithUnits right) =>
            new DoubleWithUnits(left.Value * right.Value, left.Units * right.Units);

        public static DoubleWithUnits operator /(DoubleWithUnits left, DoubleWithUnits right) =>
            new DoubleWithUnits(left.Value / right.Value, left.Units / right.Units);

        public static double operator /(DoubleWithUnits left, Units right)
        {
            var units = left.Units / right;
            if (!units.Factors.IsConstant())
            {
                throw new InvalidOperationException($"Could not convert units of '{units}' to a constant.");
            }

            return left.Value * units.Factors.AsConstant();
        }

        public override string ToString() => $"{this.Value} {this.Units}";
    }
}
