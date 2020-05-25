// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a factor made up of multiple component factors.
    /// </summary>
    public class CompositeFactor : Factor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeFactor"/> class.
        /// </summary>
        /// <param name="factors">The component factors that make up this composite factor.</param>
        public CompositeFactor(IEnumerable<Factor> factors)
        {
            var singleFactor = new Factor[1];
            var values = new Dictionary<object, Factor>();
            foreach (var factor in factors)
            {
                IEnumerable<Factor> subFactors;
                if (factor is CompositeFactor composite)
                {
                    subFactors = composite.Factors;
                }
                else
                {
                    singleFactor[0] = factor;
                    subFactors = singleFactor;
                }

                foreach (var subFactor in subFactors)
                {
                    switch (subFactor)
                    {
                        case NameFactor name:
                            if (name.Power != 0)
                            {
                                if (values.TryGetValue(name.Name, out var value))
                                {
                                    var newPower = ((NameFactor)value).Power + name.Power;
                                    if (newPower == 0)
                                    {
                                        values.Remove(name.Name);
                                    }
                                    else
                                    {
                                        values[name.Name] = new NameFactor(name.Name, newPower);
                                    }
                                }
                                else
                                {
                                    values[name.Name] = name;
                                }
                            }

                            break;

                        case NumberFactor number:
                            if (number.Number != 1 && number.Power != 0)
                            {
                                if (values.TryGetValue(number.Number, out var value))
                                {
                                    var newPower = ((NumberFactor)value).Power + number.Power;
                                    if (newPower == 0)
                                    {
                                        values.Remove(number.Number);
                                    }
                                    else
                                    {
                                        values[number.Number] = new NumberFactor(number.Number, newPower);
                                    }
                                }
                                else
                                {
                                    values[number.Number] = number;
                                }
                            }

                            break;

                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            var result = values.Count == 0
                ? new List<Factor> { SiUnits.Factors.Unit }
                : values.Values.ToList();

            this.Factors = result.AsReadOnly();
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
