// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a factor made up of multiple component factors.
    /// </summary>
    public sealed class CompositeFactor : Factor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeFactor"/> class.
        /// </summary>
        /// <param name="factors">The component factors that make up this composite factor.</param>
        public CompositeFactor(IEnumerable<Factor> factors)
        {
            if (factors == null)
            {
                throw new ArgumentNullException(nameof(factors));
            }

            this.Factors = SimplifyFactors(factors).AsReadOnly();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeFactor"/> class.
        /// </summary>
        /// <param name="factors">The component factors that make up this composite factor.</param>
        public CompositeFactor(params Factor[] factors)
            : this(factors.AsEnumerable())
        {
        }

        /// <summary>
        /// Gets the component factors that make up this composite factor.
        /// </summary>
        public ReadOnlyCollection<Factor> Factors { get; }

        /// <inheritdoc />
        public override Factor Pow(int power) => new CompositeFactor(this.Factors.Select(f => f.Pow(power)));

        /// <inheritdoc />
        public override string ToString() => string.Join("*", this.Factors.Select(f => f.ToString()).ToArray());
    }
}
