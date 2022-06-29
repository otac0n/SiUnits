// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    /// <summary>
    /// Represents a factor made up of multiple component factors.
    /// </summary>
    public sealed class CompositeFactor : Factor
    {
        private readonly ImmutableDictionary<object, Factor> factors;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeFactor"/> class.
        /// </summary>
        /// <param name="factors">The component factors that make up this composite factor.</param>
        public CompositeFactor(params Factor[] factors)
            : this(factors.AsEnumerable())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeFactor"/> class.
        /// </summary>
        /// <param name="factors">The component factors that make up this composite factor.</param>
        public CompositeFactor(IEnumerable<Factor> factors)
            : this(GroupFactors(factors ?? throw new ArgumentNullException(nameof(factors))))
        {
        }

        internal CompositeFactor(IDictionary<object, Factor> groups)
        {
            this.factors = CoalesceEmptyGroupsToNull(groups)?.ToImmutableDictionary();
        }

        /// <summary>
        /// Gets the component factors that make up this composite factor.
        /// </summary>
        public IEnumerable<Factor> Factors => this.factors?.Values ?? EmptyFactorList;

        private static IDictionary<object, Factor> CoalesceEmptyGroupsToNull(IDictionary<object, Factor> groups) => groups?.Count > 0 ? groups : null;

        /// <inheritdoc />
        public override Factor Pow(int power) => new CompositeFactor(this.Factors.Select(f => f.Pow(power)));

        /// <inheritdoc />
        public override string ToString() =>
            string.Join(
                "*",
                from f in this.factors
                orderby f.Key is string, f.Key
                select f.Value.ToString());

        /// <inheritdoc/>
        public override int GetHashCode() => this.Factors.Sum(f => f.GetHashCode());

        /// <inheritdoc/>
        public override bool Equals(Factor other) => other switch
        {
            CompositeFactor composite => Equals(this.factors, composite.factors),
            _ => Equals(this.factors, CoalesceEmptyGroupsToNull(GroupFactors(new[] { other }))),
        };

        private static bool Equals(IDictionary<object, Factor> left, IDictionary<object, Factor> right)
        {
            if (object.ReferenceEquals(left, right))
            {
                return true;
            }

            if (left?.Count != right?.Count)
            {
                return false;
            }

            foreach (var kvp in left)
            {
                if (!right.TryGetValue(kvp.Key, out var value) ||
                    !kvp.Value.Equals(value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
