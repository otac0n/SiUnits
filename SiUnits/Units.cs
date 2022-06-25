// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a set of factors bound together as a unit.
    /// </summary>
    [DebuggerDisplay("{Factors}")]
    public class Units
    {
        public static readonly Units Atto = new Units(SiUnits.Factors.Atto);
        public static readonly Units Centi = new Units(SiUnits.Factors.Centi);
        public static readonly Units Deca = new Units(SiUnits.Factors.Deca);
        public static readonly Units Deci = new Units(SiUnits.Factors.Deci);
        public static readonly Units Exa = new Units(SiUnits.Factors.Exa);
        public static readonly Units Femto = new Units(SiUnits.Factors.Femto);
        public static readonly Units Giga = new Units(SiUnits.Factors.Giga);
        public static readonly Units Hecto = new Units(SiUnits.Factors.Hecto);
        public static readonly Units Kilo = new Units(SiUnits.Factors.Kilo);
        public static readonly Units Mega = new Units(SiUnits.Factors.Mega);
        public static readonly Units Micro = new Units(SiUnits.Factors.Micro);
        public static readonly Units Milli = new Units(SiUnits.Factors.Milli);
        public static readonly Units Nano = new Units(SiUnits.Factors.Nano);
        public static readonly Units Peta = new Units(SiUnits.Factors.Peta);
        public static readonly Units Pico = new Units(SiUnits.Factors.Pico);
        public static readonly Units Tera = new Units(SiUnits.Factors.Tera);
        public static readonly Units Yocto = new Units(SiUnits.Factors.Yocto);
        public static readonly Units Yotta = new Units(SiUnits.Factors.Yotta);
        public static readonly Units Zepto = new Units(SiUnits.Factors.Zepto);
        public static readonly Units Zetta = new Units(SiUnits.Factors.Zetta);

        /// <summary>
        /// Initializes a new instance of the <see cref="Units"/> class.
        /// </summary>
        /// <param name="factors">The set of factors that make up this unit.</param>
        public Units(Factor factors)
        {
            this.Factors = factors ?? throw new ArgumentNullException(nameof(factors));
        }

        /// <summary>
        /// Gets the set of factors that make up this unit.
        /// </summary>
        public Factor Factors { get; }

        public static explicit operator Units(string factors) => new Units(new Parser().Parse(factors));

        public static DoubleWithUnits operator *(double value, Units units) => new DoubleWithUnits(value, units);

        public static Units operator *(Units left, Units right)
        {
            var leftFactors = (left ?? throw new ArgumentNullException(nameof(left))).Factors;
            var rightFactors = (right ?? throw new ArgumentNullException(nameof(right))).Factors;

            return new Units(leftFactors * rightFactors);
        }

        public static DoubleWithUnits operator /(double value, Units units)
        {
            var unitsFactors = (units ?? throw new ArgumentNullException(nameof(units))).Factors;
            return new DoubleWithUnits(value, new Units(unitsFactors.Pow(-1)));
        }

        public static Units operator /(Units left, Units right)
        {
            var leftFactors = (left ?? throw new ArgumentNullException(nameof(left))).Factors;
            var rightFactors = (right ?? throw new ArgumentNullException(nameof(right))).Factors;

            return new Units(leftFactors / rightFactors);
        }

        public Units Pow(int power) => power == 1 ? this : new Units(this.Factors.Pow(power));

        /// <inheritdoc />
        public override string ToString() => this.Factors.ToString();

        /// <summary>
        /// Contains distance uints.
        /// </summary>
        public static class Distance
        {
            public static readonly Units Meter = new Units(SiUnits.Factors.Meter);
        }

        /// <summary>
        /// Contains electrical capacitance units.
        /// </summary>
        public static class ElectricalCapacitance
        {
            public static readonly Units Farad = new Units(SiUnits.Factors.Farad);
        }

        /// <summary>
        /// Contains electrical inductance units.
        /// </summary>
        public static class ElectricalInductance
        {
            public static readonly Units Henry = new Units(SiUnits.Factors.Henry);
        }

        /// <summary>
        /// Contains electrical resistance units.
        /// </summary>
        public static class ElectricalResistance
        {
            public static readonly Units Ohm = new Units(SiUnits.Factors.Ohm);
        }

        /// <summary>
        /// Contains electric charge units.
        /// </summary>
        public static class ElectricCharge
        {
            public static readonly Units Coulomb = new Units(SiUnits.Factors.Coulomb);
        }

        /// <summary>
        /// Contains electric conductance units.
        /// </summary>
        public static class ElectricConductance
        {
            public static readonly Units Siemens = new Units(SiUnits.Factors.Siemens);
        }

        /// <summary>
        /// Contains electric current units.
        /// </summary>
        public static class ElectricCurrent
        {
            public static readonly Units Ampere = new Units(SiUnits.Factors.Ampere);
        }

        /// <summary>
        /// Contains electric potential units.
        /// </summary>
        public static class ElectricPotential
        {
            public static readonly Units Volt = new Units(SiUnits.Factors.Volt);
        }

        /// <summary>
        /// Contains energy units.
        /// </summary>
        public static class Energy
        {
            public static readonly Units Joule = new Units(SiUnits.Factors.Joule);
        }

        /// <summary>
        /// Contains force units.
        /// </summary>
        public static class Force
        {
            public static readonly Units Newton = new Units(SiUnits.Factors.Newton);
        }

        /// <summary>
        /// Contains frequency units.
        /// </summary>
        public static class Frequency
        {
            public static readonly Units Hertz = new Units(SiUnits.Factors.Hertz);
        }

        /// <summary>
        /// Contains luminous intensity units.
        /// </summary>
        public static class LuminousIntensity
        {
            public static readonly Units Candela = new Units(SiUnits.Factors.Candela);
        }

        /// <summary>
        /// Contains magnetic flux units.
        /// </summary>
        public static class MagneticFlux
        {
            public static readonly Units Weber = new Units(SiUnits.Factors.Weber);
        }

        /// <summary>
        /// Contains magnetic unductance units.
        /// </summary>
        public static class MagneticInductance
        {
            public static readonly Units Tesla = new Units(SiUnits.Factors.Tesla);
        }

        /// <summary>
        /// Contains mass units.
        /// </summary>
        public static class Mass
        {
            public static readonly Units Gram = new Units(SiUnits.Factors.Gram);

            public static readonly Units Kilogram = new Units(SiUnits.Factors.Kilogram);
        }

        /// <summary>
        /// Contains physical units.
        /// </summary>
        public static class Physical
        {
            public static readonly Units JouleSeconds = Energy.Joule * Time.Second;
        }

        /// <summary>
        /// Contains power units.
        /// </summary>
        public static class Power
        {
            public static readonly Units Watt = new Units(SiUnits.Factors.Watt);
        }

        /// <summary>
        /// Contains pressure units.
        /// </summary>
        public static class Pressure
        {
            public static readonly Units Pascal = new Units(SiUnits.Factors.Pascal);
        }

        /// <summary>
        /// Contains quantity units.
        /// </summary>
        public static class Quantity
        {
            public static readonly Units Mole = new Units(SiUnits.Factors.Mole);

            public static readonly Units One = new Units(SiUnits.Factors.One);
        }

        /// <summary>
        /// Contains temperature units.
        /// </summary>
        public static class Temperature
        {
            public static readonly Units Kelvin = new Units(SiUnits.Factors.Kelvin);
        }

        /// <summary>
        /// Contains time units.
        /// </summary>
        public static class Time
        {
            public static readonly Units Second = new Units(SiUnits.Factors.Second);
        }

        /// <summary>
        /// Contains velocity units.
        /// </summary>
        public static class Velocity
        {
            public static readonly Units MetersPerSecond = Distance.Meter / Time.Second;
        }
    }
}
