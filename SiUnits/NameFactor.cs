// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    /// <summary>
    /// Represents a factor of a named unit.
    /// </summary>
    public sealed class NameFactor : Factor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameFactor"/> class.
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
        public override Factor Pow(int power) => new NameFactor(this.Name, this.Power * power);

        /// <inheritdoc />
        public override string ToString() => this.Power == 1 ? this.Name : $"{this.Name}^{this.Power}";
    }
}
