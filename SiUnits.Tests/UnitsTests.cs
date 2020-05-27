// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits.Tests
{
    using System;
    using Xunit;

    public class UnitsTests
    {
        [Fact]
        public void Convert_WhenUnitsAreEmpty_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => (Units)"");
        }

        [Fact]
        public void Divide_WhenGiventDistanceAndTime_ReturnsSpeed()
        {
            var distance = Units.Meter;
            var time = Units.Second;
            var speed = distance / time;

            Assert.Equal("meter*second^-1", speed.ToString());
        }

        [Fact]
        public void Multiply_WhenGivenSpeedAndTime_ReturnsDistance()
        {
            var speed = (Units)"m/s";
            var time = Units.Second;
            var distance = speed * time;

            Assert.Equal("meter", distance.ToString());
        }
    }
}
