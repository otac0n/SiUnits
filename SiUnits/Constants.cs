// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System.Numerics;

    public static class Constants<T>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        public static readonly ValueWithUnits<T> Avogadro = T.CreateChecked(6.02214076e23) * Units<T>.Quantity.Mole.Pow(-1);
        public static readonly ValueWithUnits<T> Boltzmann = T.CreateChecked(1.380649e-23) * (Units<T>.Energy.Joule / Units<T>.Temperature.Kelvin);
        public static readonly ValueWithUnits<T> ElementaryCharge = T.CreateChecked(1.602176634e-19) * Units<T>.ElectricCharge.Coulomb;
        public static readonly ValueWithUnits<T> Mole = T.MultiplicativeIdentity * Units<T>.Quantity.Mole;
        public static readonly ValueWithUnits<T> One = T.MultiplicativeIdentity * Units<T>.Quantity.One;
        public static readonly ValueWithUnits<T> Planck = T.CreateChecked(6.62607015e-34) * Units<T>.Physical.JouleSeconds;
        public static readonly ValueWithUnits<T> SpeedOfLight = T.CreateChecked(299792458) * Units<T>.Velocity.MetersPerSecond;
        public static readonly ValueWithUnits<T> Pi = T.Pi * Units<T>.Quantity.One;
        public static readonly ValueWithUnits<T> Tau = T.Tau * Units<T>.Quantity.One;
    }
}
