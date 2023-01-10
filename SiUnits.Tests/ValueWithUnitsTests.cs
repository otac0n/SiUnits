// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using CompositeFactor = SiUnits.CompositeFactor<double>;
    using Factor = SiUnits.Factor<double>;
    using Factors = SiUnits.Factors<double>;
    using NumberFactor = SiUnits.NumberFactor<double>;
    using static Units<double>;

    public class ValueWithUnitsTests
    {
        public static readonly object[][] EquivalentFactors =
        {
            new object[] { new CompositeFactor(Factors.Kilo, Factors.Second) },
            new object[] { new CompositeFactor(new CompositeFactor(Factors.Kilo), new CompositeFactor(Factors.Second)) },
            new object[] { new CompositeFactor(Factors.One, new CompositeFactor(Factors.Kilo, Factors.Second)) },
            new object[] { new CompositeFactor(Factors.Deca, Factors.Hecto, Factors.Second) },
            new object[] { new CompositeFactor(new CompositeFactor(Factors.Deca, new NumberFactor(10)), new CompositeFactor(Factors.Deca, Factors.Second)) },
            new object[] { new CompositeFactor(new NumberFactor(2, 2), new NumberFactor(5, 2), new NumberFactor(10), Factors.Second) },
        };

        public static IEnumerable<object[]> GetEquivalentFactorPairs() =>
            from l in FactorTests.EquivalentFactors
            from r in FactorTests.EquivalentFactors
            select new[] { l[0], r[0] };

        [Fact]
        public void Divide_UnitsAreNotConstant_ThrowsInvalidOperationException()
        {
            var mass = 1 * Mass.Kilogram;
            Assert.Throws<InvalidOperationException>(() => mass / Quantity.One);
        }

        [Fact]
        public void Divide_WhenUnitsDifferByAConstantFactor_ScalesTheValueByTheConstantFactor()
        {
            var mass = 1 * Mass.Kilogram;
            var massInGrams = mass / Mass.Gram;
            Assert.Equal(1000, massInGrams);
        }

        [Fact]
        public void Integration_DifferenceOfDistances()
        {
            var distanceA = 1 * (Units<double>)"km";
            var distanceB = 2 * (Units<double>)"m";
            var distanceC = 3 * (Units<double>)"cm";
            var expected = 1000000000000.0 - 2000000000.0 - 30000000.0;

            var distanceInNano = (distanceA - distanceB - distanceC) / (Units<double>)"nm";

            Assert.Equal(expected, distanceInNano);
        }

        [Fact]
        public void Integration_SpeedEquation()
        {
            var speed = 10 * (Units<double>)"m/s";
            var distance = 2 * (Kilo * Distance.Meter);
            var time = distance / speed;

            var timeInSeconds = time / Time.Second;

            Assert.Equal(200.0, timeInSeconds);
        }

        [Fact]
        public void Integration_SumOfDistances()
        {
            var distanceA = 1 * (Units<double>)"km";
            var distanceB = 2 * (Units<double>)"m";
            var distanceC = 3 * (Units<double>)"cm";
            var expected = 1000000000000.0 + 2000000000.0 + 30000000.0;

            var distanceInNano = (distanceA + distanceB + distanceC) / (Units<double>)"nm";

            Assert.Equal(expected, distanceInNano);
        }

        [Theory]
        [MemberData(nameof(GetEquivalentFactorPairs))]
        public void Equals_WithEquivalentFactors_ReturnsTrue(Factor factorA, Factor factorB)
        {
            var a = 42.0 * new Units<double>(factorA);
            var b = 42.0 * new Units<double>(factorB);
            Assert.True(a.Equals(b));
        }
    }
}
