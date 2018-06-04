// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Factors}")]
    public class Units
    {
        public Units(Factor factors)
        {
            this.Factors = factors ?? throw new ArgumentNullException(nameof(factors));
        }

        public Factor Factors { get; }

        public static implicit operator Units(string factors) => new Units(new Parser().Parse(factors));

        public static DoubleWithUnits operator *(double value, Units units) => new DoubleWithUnits(value, units);

        public static Units operator *(Units left, Units right) => new Units(left.Factors * right.Factors);

        public static DoubleWithUnits operator /(double value, Units units) => new DoubleWithUnits(value, new Units(units.Factors.Pow(-1)));

        public static Units operator /(Units left, Units right) => new Units(left.Factors / right.Factors);

        /// <inheritdoc />
        public override string ToString() => this.Factors.ToString();
    }
}
