// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Represents a number factor.
    /// </summary>
    /// <typeparam name="T">The underlying floating point representation for factors.</typeparam>
    public sealed class NumberFactor<T> : Factor<T>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFactor{T}"/> class.
        /// </summary>
        /// <param name="number">The positive number being used as a factor.</param>
        /// <param name="power">The power of this factor.</param>
        public NumberFactor(T number, int power = 1)
        {
            if (number <= T.AdditiveIdentity)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            this.Number = number;
            this.Power = power;
        }

        /// <summary>
        /// Gets the unit factor.
        /// </summary>
        public static NumberFactor<T> Unit { get; } = new NumberFactor<T>(T.MultiplicativeIdentity);

        /// <summary>
        /// Gets the number being used as a factor.
        /// </summary>
        public T Number { get; }

        /// <summary>
        /// Gets the power of this factor.
        /// </summary>
        public int Power { get; }

        /// <inheritdoc/>
        public override T AsConstant() => T.Pow(this.Number, T.CreateChecked(this.Power));

        /// <inheritdoc/>
        public override bool IsConstant() => true;

        /// <inheritdoc/>
        public override bool IsConstant(out T value)
        {
            value = this.AsConstant();
            return true;
        }

        /// <inheritdoc />
        public override NumberFactor<T> Pow(int power) =>
            power == 1 ? this :
            power == 0 || this.Number == T.MultiplicativeIdentity ? Unit :
            new NumberFactor<T>(this.Number, this.Power * power);

        /// <inheritdoc />
        public override string ToString() => this.Power == 1 ? $"{this.Number}" : $"{this.Number}^{this.Power}";

        /// <inheritdoc/>
        public override int GetHashCode() => 0;

        /// <inheritdoc/>
        public override bool Equals(Factor<T> other) => other switch
        {
            NumberFactor<T> number => this.Power == number.Power && this.Number.Equals(number.Number),
            CompositeFactor<T> composite => composite.Equals(this),
            _ => false,
        };
    }
}
