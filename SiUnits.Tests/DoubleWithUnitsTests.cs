// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits.Tests
{
    using Xunit;

    public class DoubleWithUnitsTests
    {
        [Fact]
        public void Divide_WhenUnitsDifferByAConstantFactor_ScalesTheValueByTheConstantFactor()
        {
            var mass = 1 * (Units)"kg";
            var massInGrams = mass / "gram";
            Assert.Equal(1000, massInGrams);
        }
    }
}
