// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Diagnostics;
    using System.Numerics;

    /// <summary>
    /// Represents a set of factors bound together as a unit.
    /// </summary>
    /// <typeparam name="T">The underlying floating point representation for factors.</typeparam>
    public static class Units<T>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        /// <summary>
        /// Contains distance uints.
        /// </summary>
        public static class Distance
        {
            public static readonly Factor<T> Meter = Factors<T>.Meter;
        }

        /// <summary>
        /// Contains electrical capacitance units.
        /// </summary>
        public static class ElectricalCapacitance
        {
            public static readonly Factor<T> Farad = Factors<T>.Farad;
        }

        /// <summary>
        /// Contains electrical inductance units.
        /// </summary>
        public static class ElectricalInductance
        {
            public static readonly Factor<T> Henry = Factors<T>.Henry;
        }

        /// <summary>
        /// Contains electrical resistance units.
        /// </summary>
        public static class ElectricalResistance
        {
            public static readonly Factor<T> Ohm = Factors<T>.Ohm;
        }

        /// <summary>
        /// Contains electric charge units.
        /// </summary>
        public static class ElectricCharge
        {
            public static readonly Factor<T> Coulomb = Factors<T>.Coulomb;
        }

        /// <summary>
        /// Contains electric conductance units.
        /// </summary>
        public static class ElectricConductance
        {
            public static readonly Factor<T> Siemens = Factors<T>.Siemens;
        }

        /// <summary>
        /// Contains electric current units.
        /// </summary>
        public static class ElectricCurrent
        {
            public static readonly Factor<T> Ampere = Factors<T>.Ampere;
        }

        /// <summary>
        /// Contains electric potential units.
        /// </summary>
        public static class ElectricPotential
        {
            public static readonly Factor<T> Volt = Factors<T>.Volt;
        }

        /// <summary>
        /// Contains energy units.
        /// </summary>
        public static class Energy
        {
            public static readonly Factor<T> Joule = Factors<T>.Joule;
        }

        /// <summary>
        /// Contains force units.
        /// </summary>
        public static class Force
        {
            public static readonly Factor<T> Newton = Factors<T>.Newton;
        }

        /// <summary>
        /// Contains frequency units.
        /// </summary>
        public static class Frequency
        {
            public static readonly Factor<T> Hertz = Factors<T>.Hertz;
        }

        /// <summary>
        /// Contains luminous intensity units.
        /// </summary>
        public static class LuminousIntensity
        {
            public static readonly Factor<T> Candela = Factors<T>.Candela;
        }

        /// <summary>
        /// Contains magnetic flux units.
        /// </summary>
        public static class MagneticFlux
        {
            public static readonly Factor<T> Weber = Factors<T>.Weber;
        }

        /// <summary>
        /// Contains magnetic unductance units.
        /// </summary>
        public static class MagneticInductance
        {
            public static readonly Factor<T> Tesla = Factors<T>.Tesla;
        }

        /// <summary>
        /// Contains mass units.
        /// </summary>
        public static class Mass
        {
            public static readonly Factor<T> Gram = Factors<T>.Gram;

            public static readonly Factor<T> Kilogram = Factors<T>.Kilogram;
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
            public static readonly Factor<T> Watt = Factors<T>.Watt;
        }

        /// <summary>
        /// Contains pressure units.
        /// </summary>
        public static class Pressure
        {
            public static readonly Factor<T> Pascal = Factors<T>.Pascal;
        }

        /// <summary>
        /// Contains quantity units.
        /// </summary>
        public static class Quantity
        {
            public static readonly Factor<T> Mole = Factors<T>.Mole;

            public static readonly Factor<T> One = Factors<T>.One;
        }

        /// <summary>
        /// Contains temperature units.
        /// </summary>
        public static class Temperature
        {
            public static readonly Factor<T> Kelvin = Factors<T>.Kelvin;
        }

        /// <summary>
        /// Contains time units.
        /// </summary>
        public static class Time
        {
            public static readonly Factor<T> Second = Factors<T>.Second;
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
