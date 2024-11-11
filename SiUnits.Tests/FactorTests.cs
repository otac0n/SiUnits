// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using static SiUnits.Units<double>;
    using CompositeFactor = SiUnits.CompositeFactor<double>;
    using NumberFactor = SiUnits.NumberFactor<double>;
    using Units = SiUnits.Factor<double>;

    public class FactorTests
    {
        public static readonly object[][] ExpectedUnits =
        {
            new object[] { "m", Distance.Meter },
            new object[] { "meter", Distance.Meter },
            new object[] { "metre", Distance.Meter },
            new object[] { "meters", Distance.Meter },
            new object[] { "metres", Distance.Meter },
            new object[] { "s", Time.Second },
            new object[] { "second", Time.Second },
            new object[] { "seconds", Time.Second },
        };

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

        [Test]
        [TestCaseSource(nameof(VariousFactors))]
        public void Equals_WithAMatchingCompositeFactor_ReturnsTrue(Units factor)
        {
            var composite = new CompositeFactor(factor);
            Assert.True(factor.Equals(composite));
        }

        [Theory]
        [TestCaseSource(nameof(VariousFactors))]
        public void GetHashCode_WithAMatchingCompositeFactor_ReturnsTheSameValue(Units factor)
        {
            var composite = new CompositeFactor(factor);
            Assert.That(composite.GetHashCode(), Is.EqualTo(factor.GetHashCode()));
        }

        [Theory]
        [TestCaseSource(nameof(GetEquivalentPairs))]
        public void GetHashCode_WithAnEquivalentFactor_ReturnsTheSameValue(Units left, Units right)
        {
            Assert.That(left.GetHashCode(), Is.EqualTo(right.GetHashCode()));
        }

        [Theory]
        [TestCaseSource(nameof(ExpectedUnits))]
        public void Convert_Always_ReturnsExpectedUnits(string input, Units expected)
        {
            var actual = (Units)input;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Convert_WhenUnitsAreEmpty_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() =>
            {
                var fail = (Units)"";
            });
        }

        [Test]
        public void Divide_WhenGiventDistanceAndTime_ReturnsSpeed()
        {
            var distance = Distance.Meter;
            var time = Time.Second;
            var speed = distance / time;

            Assert.That(speed.ToString(), Is.EqualTo("meter*second^-1"));
        }

        [Test]
        public void Multiply_WhenGivenSpeedAndTime_ReturnsDistance()
        {
            var speed = Units.Parse("m/s");
            var time = Time.Second;
            var distance = speed * time;

            Assert.That(distance.ToString(), Is.EqualTo("meter"));
        }
    }
}
