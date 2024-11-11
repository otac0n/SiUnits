// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using static SiUnits.Units<double>;
    using Units = SiUnits.Factor<double>;

    public class ValueWithUnitsTests
    {
        public static IEnumerable<object[]> GetEquivalentFactorPairs() =>
            from l in FactorTests.EquivalentFactors
            from r in FactorTests.EquivalentFactors
            select new[] { l[0], r[0] };

        [Test]
        public void Divide_UnitsAreNotConstant_ThrowsInvalidOperationException()
        {
            var mass = 1 * Mass.Kilogram;
            Assert.Throws<InvalidOperationException>(() =>
            {
                var fail = mass / Quantity.One;
            });
        }

        [Test]
        public void Divide_WhenUnitsDifferByAConstantFactor_ScalesTheValueByTheConstantFactor()
        {
            var mass = 1 * Mass.Kilogram;
            var massInGrams = mass / Mass.Gram;
            Assert.That(massInGrams, Is.EqualTo(1000));
        }

        [Test]
        public void Integration_DifferenceOfDistances()
        {
            var distanceA = 1 * (Units)"km";
            var distanceB = 2 * (Units)"m";
            var distanceC = 3 * (Units)"cm";
            var expected = 1000000000000.0 - 2000000000.0 - 30000000.0;

            var distanceInNano = (distanceA - distanceB - distanceC) / (Units)"nm";

            Assert.That(distanceInNano, Is.EqualTo(expected));
        }

        [Test]
        public void Integration_SpeedEquation()
        {
            var speed = 10 * (Units)"m/s";
            var distance = 2 * (Kilo * Distance.Meter);
            var time = distance / speed;

            var timeInSeconds = time / Time.Second;

            Assert.That(timeInSeconds, Is.EqualTo(200.0));
        }

        [Test]
        public void Integration_SumOfDistances()
        {
            var distanceA = 1 * (Units)"km";
            var distanceB = 2 * (Units)"m";
            var distanceC = 3 * (Units)"cm";
            var expected = 1000000000000.0 + 2000000000.0 + 30000000.0;

            var distanceInNano = (distanceA + distanceB + distanceC) / (Units)"nm";

            Assert.That(distanceInNano, Is.EqualTo(expected));
        }

        [Theory]
        [TestCaseSource(nameof(GetEquivalentFactorPairs))]
        public void Equals_WithEquivalentFactors_ReturnsTrue(Units factorA, Units factorB)
        {
            var a = 42.0 * factorA;
            var b = 42.0 * factorB;
            Assert.True(a.Equals(b));
        }
    }
}
