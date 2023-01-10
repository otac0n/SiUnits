// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Numerics;

    /// <summary>
    /// Represents a set of factors bound together as a unit.
    /// </summary>
    /// <typeparam name="T">The underlying floating point representation for factors.</typeparam>
    public static class Units<T>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        public static readonly NumberFactor<T> Atto;
        public static readonly NumberFactor<T> Centi;
        public static readonly NumberFactor<T> Deca;
        public static readonly NumberFactor<T> Deci;
        public static readonly NumberFactor<T> Exa;
        public static readonly NumberFactor<T> Femto;
        public static readonly NumberFactor<T> Giga;
        public static readonly NumberFactor<T> Hecto;
        public static readonly NumberFactor<T> Kilo;
        public static readonly NumberFactor<T> Mega;
        public static readonly NumberFactor<T> Micro;
        public static readonly NumberFactor<T> Milli;
        public static readonly NumberFactor<T> Nano;
        public static readonly NumberFactor<T> One;
        public static readonly NumberFactor<T> Peta;
        public static readonly NumberFactor<T> Pico;
        public static readonly NumberFactor<T> Quecto;
        public static readonly NumberFactor<T> Quetta;
        public static readonly NumberFactor<T> Ronna;
        public static readonly NumberFactor<T> Ronto;
        public static readonly NumberFactor<T> Tera;
        public static readonly NumberFactor<T> Yocto;
        public static readonly NumberFactor<T> Yotta;
        public static readonly NumberFactor<T> Zepto;
        public static readonly NumberFactor<T> Zetta;

        [SuppressMessage("Performance", "CA1810:Initialize reference type static fields inline", Justification = "The order of field initialization is important.")]
        static Units()
        {
            var ten = T.CreateChecked(10);
            One = new NumberFactor<T>(ten, 0);

            Deca = new NumberFactor<T>(ten, 1);
            Hecto = new NumberFactor<T>(ten, 2);
            Kilo = new NumberFactor<T>(ten, 3);
            Mega = new NumberFactor<T>(ten, 6);
            Giga = new NumberFactor<T>(ten, 9);
            Tera = new NumberFactor<T>(ten, 12);
            Peta = new NumberFactor<T>(ten, 15);
            Exa = new NumberFactor<T>(ten, 18);
            Zetta = new NumberFactor<T>(ten, 21);
            Yotta = new NumberFactor<T>(ten, 24);
            Ronna = new NumberFactor<T>(ten, 27);
            Quetta = new NumberFactor<T>(ten, 30);
            Deci = new NumberFactor<T>(ten, -1);
            Centi = new NumberFactor<T>(ten, -2);
            Milli = new NumberFactor<T>(ten, -3);
            Micro = new NumberFactor<T>(ten, -6);
            Nano = new NumberFactor<T>(ten, -9);
            Pico = new NumberFactor<T>(ten, -12);
            Femto = new NumberFactor<T>(ten, -15);
            Atto = new NumberFactor<T>(ten, -18);
            Zepto = new NumberFactor<T>(ten, -21);
            Yocto = new NumberFactor<T>(ten, -24);
            Ronto = new NumberFactor<T>(ten, -27);
            Quecto = new NumberFactor<T>(ten, -30);
        }

        /// <summary>
        /// Contains distance uints.
        /// </summary>
        public static class Distance
        {
            public static readonly NameFactor<T> Meter = new NameFactor<T>("meter");
        }

        /// <summary>
        /// Contains electrical capacitance units.
        /// </summary>
        public static class ElectricalCapacitance
        {
            public static readonly Factor<T> Farad = ElectricCharge.Coulomb / ElectricPotential.Volt;
        }

        /// <summary>
        /// Contains electrical inductance units.
        /// </summary>
        public static class ElectricalInductance
        {
            public static readonly Factor<T> Henry = MagneticFlux.Weber / ElectricCurrent.Ampere;
        }

        /// <summary>
        /// Contains electrical resistance units.
        /// </summary>
        public static class ElectricalResistance
        {
            public static readonly Factor<T> Ohm = ElectricPotential.Volt / ElectricCurrent.Ampere;
        }

        /// <summary>
        /// Contains electric charge units.
        /// </summary>
        public static class ElectricCharge
        {
            public static readonly Factor<T> Coulomb = Time.Second * ElectricCurrent.Ampere;
        }

        /// <summary>
        /// Contains electric conductance units.
        /// </summary>
        public static class ElectricConductance
        {
            public static readonly Factor<T> Siemens = ElectricalResistance.Ohm.Pow(-1);
        }

        /// <summary>
        /// Contains electric current units.
        /// </summary>
        public static class ElectricCurrent
        {
            public static readonly NameFactor<T> Ampere = new NameFactor<T>("ampere");
        }

        /// <summary>
        /// Contains electric potential units.
        /// </summary>
        public static class ElectricPotential
        {
            public static readonly Factor<T> Volt = Power.Watt / ElectricCurrent.Ampere;
        }

        /// <summary>
        /// Contains energy units.
        /// </summary>
        public static class Energy
        {
            public static readonly Factor<T> Joule = Force.Newton * Distance.Meter;
        }

        /// <summary>
        /// Contains force units.
        /// </summary>
        public static class Force
        {
            public static readonly Factor<T> Newton = Mass.Kilogram * Distance.Meter / Time.Second.Pow(2);
        }

        /// <summary>
        /// Contains frequency units.
        /// </summary>
        public static class Frequency
        {
            public static readonly Factor<T> Hertz = Time.Second.Pow(-1);
        }

        /// <summary>
        /// Contains luminous intensity units.
        /// </summary>
        public static class LuminousIntensity
        {
            public static readonly NameFactor<T> Candela = new NameFactor<T>("candela");
        }

        /// <summary>
        /// Contains magnetic flux units.
        /// </summary>
        public static class MagneticFlux
        {
            public static readonly Factor<T> Weber = ElectricPotential.Volt * Time.Second;
        }

        /// <summary>
        /// Contains magnetic unductance units.
        /// </summary>
        public static class MagneticInductance
        {
            public static readonly Factor<T> Tesla = MagneticFlux.Weber / Distance.Meter.Pow(2);
        }

        /// <summary>
        /// Contains mass units.
        /// </summary>
        public static class Mass
        {
            public static readonly NameFactor<T> Gram = new NameFactor<T>("gram");

            public static readonly Factor<T> Kilogram = Kilo * Gram;
        }

        /// <summary>
        /// Contains physical units.
        /// </summary>
        public static class Physical
        {
            public static readonly Factor<T> JouleSeconds = Energy.Joule * Time.Second;
        }

        /// <summary>
        /// Contains power units.
        /// </summary>
        public static class Power
        {
            public static readonly Factor<T> Watt = Energy.Joule / Time.Second;
        }

        /// <summary>
        /// Contains pressure units.
        /// </summary>
        public static class Pressure
        {
            public static readonly Factor<T> Pascal = Force.Newton / Distance.Meter.Pow(2);
        }

        /// <summary>
        /// Contains quantity units.
        /// </summary>
        public static class Quantity
        {
            public static readonly NameFactor<T> Mole = new NameFactor<T>("mole");

            public static readonly NumberFactor<T> One = NumberFactor<T>.Unit;
        }

        /// <summary>
        /// Contains temperature units.
        /// </summary>
        public static class Temperature
        {
            public static readonly NameFactor<T> Kelvin = new NameFactor<T>("kelvin");
        }

        /// <summary>
        /// Contains time units.
        /// </summary>
        public static class Time
        {
            public static readonly NameFactor<T> Second = new NameFactor<T>("second");
        }

        /// <summary>
        /// Contains velocity units.
        /// </summary>
        public static class Velocity
        {
            public static readonly Factor<T> MetersPerSecond = Distance.Meter / Time.Second;
        }
    }
}
