// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;

    /// <summary>
    /// Represents a number factor.
    /// </summary>
    public sealed class NumberFactor : Factor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFactor"/> class.
        /// </summary>
        /// <param name="number">The positive number being used as a factor.</param>
        /// <param name="power">The power of this factor.</param>
        public NumberFactor(double number, int power = 1)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            this.Number = number;
            this.Power = power;
        }

        /// <summary>
        /// Gets the unit factor.
        /// </summary>
        public static NumberFactor Unit { get; } = new NumberFactor(1);

        /// <summary>
        /// Gets the number being used as a factor.
        /// </summary>
        public double Number { get; }

        /// <summary>
        /// Gets the power of this factor.
        /// </summary>
        public int Power { get; }

        /// <inheritdoc />
        public override Factor Pow(int power) =>
            power == 1 ? this :
            power == 0 || this.Number == 1 ? Unit :
            new NumberFactor(this.Number, this.Power * power);

        /// <inheritdoc />
        public override string ToString() => this.Power == 1 ? $"{this.Number}" : $"{this.Number}^{this.Power}";
    }
}
