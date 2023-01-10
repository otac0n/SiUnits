// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Represents a factor of a named unit.
    /// </summary>
    /// <typeparam name="T">The underlying floating point representation for factors.</typeparam>
    public sealed class NameFactor<T> : Factor<T>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameFactor{T}"/> class.
        /// </summary>
        /// <param name="name">The unit name being used as a factor.</param>
        /// <param name="power">The power of this factor.</param>
        public NameFactor(string name, int power = 1)
        {
            this.Name = name;
            this.Power = power;
        }

        /// <summary>
        /// Gets the unit name being used as a factor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the power of this factor.
        /// </summary>
        public int Power { get; }

        /// <inheritdoc />
        public override Factor<T> Pow(int power) => power == 1 ? this : new NameFactor<T>(this.Name, this.Power * power);

        /// <inheritdoc />
        public override string ToString() => this.Power == 1 ? this.Name : $"{this.Name}^{this.Power}";

        /// <inheritdoc/>
        public override int GetHashCode() =>
            unchecked(this.Power * this.Name.GetHashCode(StringComparison.Ordinal));

        /// <inheritdoc/>
        public override bool Equals(Factor<T> other) => other switch
        {
            NameFactor<T> name => name.Name.Equals(this.Name, StringComparison.Ordinal),
            CompositeFactor<T> composite => composite.Equals(this),
            _ => false,
        };
    }
}
