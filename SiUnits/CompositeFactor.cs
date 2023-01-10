// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Represents a factor made up of multiple component factors.
    /// </summary>
    /// <typeparam name="T">The underlying floating point representation for factors.</typeparam>
    public sealed class CompositeFactor<T> : Factor<T>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        private readonly ImmutableDictionary<object, Factor<T>> factors;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeFactor{T}"/> class.
        /// </summary>
        /// <param name="factors">The component factors that make up this composite factor.</param>
        public CompositeFactor(params Factor<T>[] factors)
            : this(factors.AsEnumerable())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeFactor{T}"/> class.
        /// </summary>
        /// <param name="factors">The component factors that make up this composite factor.</param>
        public CompositeFactor(IEnumerable<Factor<T>> factors)
            : this(GroupFactors(factors ?? throw new ArgumentNullException(nameof(factors))))
        {
        }

        internal CompositeFactor(IDictionary<object, Factor<T>> groups)
        {
            this.factors = CoalesceEmptyGroupsToNull(groups)?.ToImmutableDictionary();
        }

        /// <summary>
        /// Gets the component factors that make up this composite factor.
        /// </summary>
        public IEnumerable<Factor<T>> Factors => this.factors?.Values ?? EmptyFactorList;

        /// <inheritdoc/>
        public override T AsConstant() => this.Factors.Select(f => f.AsConstant()).Aggregate((a, b) => a * b);

        /// <inheritdoc/>
        public override bool IsConstant() => this.Factors.All(f => f.IsConstant());

        /// <inheritdoc/>
        public override bool IsConstant(out T value)
        {
            using var e = this.Factors.GetEnumerator();
            if (!e.MoveNext() || !e.Current.IsConstant(out value))
            {
                value = default;
                return false;
            }

            while (e.MoveNext())
            {
                if (e.Current.IsConstant(out var innerValue))
                {
                    value *= innerValue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override Factor<T> Pow(int power) => power switch {
            0 => NumberFactor<T>.Unit,
            1 => this,
            _ => new CompositeFactor<T>(this.Factors.Select(f => f.Pow(power)))
        };

        /// <inheritdoc />
        public override string ToString() =>
            string.Join(
                "*",
                from f in this.factors
                orderby f.Key is string, f.Key
                select f.Value.ToString());

        /// <inheritdoc/>
        public override int GetHashCode() => this.Factors.Select(f => f.GetHashCode()).Aggregate((a, b) => unchecked(a + b));

        /// <inheritdoc/>
        public override bool Equals(Factor<T> other) => other switch
        {
            null => false,
            CompositeFactor<T> composite => Equals(this.factors, composite.factors),
            _ => Equals(this.factors, CoalesceEmptyGroupsToNull(GroupFactors(new[] { other }))),
        };

        private static IDictionary<object, Factor<T>> CoalesceEmptyGroupsToNull(IDictionary<object, Factor<T>> groups) => groups?.Count > 0 ? groups : null;

        private static bool Equals(IDictionary<object, Factor<T>> left, IDictionary<object, Factor<T>> right)
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
