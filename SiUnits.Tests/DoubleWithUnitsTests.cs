// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits.Tests
{
    using Xunit;

    public class DoubleWithUnitsTests
    {
        [Fact]
        public void Divide_WhenUnitsDifferByAConstantFactor_ScalesTheValueByTheConstantFactor()
        {
            var mass = 1 * Units.Kilogram;
            var massInGrams = mass / Units.Gram;
            Assert.Equal(1000, massInGrams);
        }

        [Fact]
        public void Integration_SpeedEquation()
        {
            var speed = 10 * (Units)"m*s^-1";
            var distance = 2 * Units.Kilometer;
            var time = distance / speed;

            var timeInSeconds = time / Units.Second;

            Assert.Equal(200.0, timeInSeconds);
        }

        [Fact]
        public void Integration_SumOfDistances()
        {
            var distanceA = 1 * (Units)"km";
            var distanceB = 2 * (Units)"m";
            var distanceC = 3 * (Units)"cm";

            var distanceInNano = (distanceA + distanceB + distanceC) / (Units)"nm";

            Assert.Equal(1002030000000.0, distanceInNano);
        }
    }
}
