// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using static Units<double>;
    using CompositeFactor = SiUnits.CompositeFactor<double>;
    using Factor = SiUnits.Factor<double>;
    using NumberFactor = SiUnits.NumberFactor<double>;

    public class FactorTests
    {
        public static readonly object[][] VariousFactors =
        {
            new object[] { Energy.Joule },
            new object[] { Deci },
            new object[] { Femto * ElectricalCapacitance.Farad },
        };

        public static readonly object[][] EquivalentFactors =
        {
            new object[] { new CompositeFactor(Kilo, Time.Second) },
            new object[] { new CompositeFactor(new CompositeFactor(Kilo), new CompositeFactor(Time.Second)) },
            new object[] { new CompositeFactor(Quantity.One, new CompositeFactor(Kilo, Time.Second)) },
            new object[] { new CompositeFactor(Deca, Hecto, Time.Second) },
            new object[] { new CompositeFactor(new CompositeFactor(Deca, new NumberFactor(10)), new CompositeFactor(Deca, Time.Second)) },
            new object[] { new CompositeFactor(new NumberFactor(2, 2), new NumberFactor(5, 2), new NumberFactor(10), Time.Second) },
        };

        public static IEnumerable<object[]> GetEquivalentPairs() =>
            from l in EquivalentFactors
            from r in EquivalentFactors
            select new[] { l[0], r[0] };

        [Theory]
        [MemberData(nameof(VariousFactors))]
        public void Equals_WithAMatchingCompositeFactor_ReturnsTrue(Factor factor)
        {
            var composite = new CompositeFactor(factor);
            Assert.True(factor.Equals(composite));
        }

        [Theory]
        [MemberData(nameof(VariousFactors))]
        public void GetHashCode_WithAMatchingCompositeFactor_ReturnsTheSameValue(Factor factor)
        {
            var composite = new CompositeFactor(factor);
            Assert.Equal(factor.GetHashCode(), composite.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(GetEquivalentPairs))]
        public void GetHashCode_WithAnEquivalentFactor_ReturnsTheSameValue(Factor left, Factor right)
        {
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }
    }
}
