namespace SiUnits.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class FactorTests
    {
        public static readonly object[][] VariousFactors =
        {
            new object[] { Factors.Joule },
            new object[] { Factors.Deci },
            new object[] { new CompositeFactor(Factors.Femto, Factors.Farad) },
        };

        public static readonly object[][] EquivalentFactors =
        {
            new object[] { new CompositeFactor(Factors.Kilo, Factors.Second) },
            new object[] { new CompositeFactor(new CompositeFactor(Factors.Kilo), new CompositeFactor(Factors.Second)) },
            new object[] { new CompositeFactor(Factors.One, new CompositeFactor(Factors.Kilo, Factors.Second)) },
            new object[] { new CompositeFactor(Factors.Deca, Factors.Hecto, Factors.Second) },
            new object[] { new CompositeFactor(new CompositeFactor(Factors.Deca, new NumberFactor(10)), new CompositeFactor(Factors.Deca, Factors.Second)) },
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
        public void Equals_WithAnEquivalentFactor_ReturnsTrue(Factor left, Factor right)
        {
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(GetEquivalentPairs))]
        public void GetHashCode_WithAnEquivalentFactor_ReturnsTheSameValue(Factor left, Factor right)
        {
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }
    }
}
