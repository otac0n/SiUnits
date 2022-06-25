// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using static SiUnits.Units;

    public static class Constants
    {
        public static readonly ValueWithUnits Avogadro = 6.02214076e23 * Quantity.Mole.Pow(-1);
        public static readonly ValueWithUnits Boltzmann = 1.380649e-23 * (Energy.Joule / Temperature.Kelvin);
        public static readonly ValueWithUnits ElementaryCharge = 1.602176634e-19 * ElectricCharge.Coulomb;
        public static readonly ValueWithUnits Mole = 1.0 * Quantity.Mole;
        public static readonly ValueWithUnits One = 1.0 * Quantity.One;
        public static readonly ValueWithUnits Planck = 6.62607015e-34 * Physical.JouleSeconds;
        public static readonly ValueWithUnits SpeedOfLight = 299792458.0 * Velocity.MetersPerSecond;
        public static readonly ValueWithUnits Turn = 2 * Math.PI * Quantity.One;
    }
}
