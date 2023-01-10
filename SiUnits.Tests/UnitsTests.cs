// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits.Tests
{
    using System;
    using Xunit;
    using static Units<double>;

    public class UnitsTests
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

        [Theory]
        [MemberData(nameof(ExpectedUnits))]
        public void Convert_Always_ReturnsExpectedUnits(string input, Factor<double> expected)
        {
            var actual = (Factor<double>)input;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Convert_WhenUnitsAreEmpty_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => (Factor<double>)"");
        }

        [Fact]
        public void Divide_WhenGiventDistanceAndTime_ReturnsSpeed()
        {
            var distance = Distance.Meter;
            var time = Time.Second;
            var speed = distance / time;

            Assert.Equal("meter*second^-1", speed.ToString());
        }

        [Fact]
        public void Multiply_WhenGivenSpeedAndTime_ReturnsDistance()
        {
            var speed = (Factor<double>)"m/s";
            var time = Time.Second;
            var distance = speed * time;

            Assert.Equal("meter", distance.ToString());
        }
    }
}
