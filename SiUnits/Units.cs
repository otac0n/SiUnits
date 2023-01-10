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
    [DebuggerDisplay("{Factors}")]
    public class Units<T> : IEquatable<Units<T>>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        public static readonly Units<T> Atto = new Units<T>(Factors<T>.Atto);
        public static readonly Units<T> Centi = new Units<T>(Factors<T>.Centi);
        public static readonly Units<T> Deca = new Units<T>(Factors<T>.Deca);
        public static readonly Units<T> Deci = new Units<T>(Factors<T>.Deci);
        public static readonly Units<T> Exa = new Units<T>(Factors<T>.Exa);
        public static readonly Units<T> Femto = new Units<T>(Factors<T>.Femto);
        public static readonly Units<T> Giga = new Units<T>(Factors<T>.Giga);
        public static readonly Units<T> Hecto = new Units<T>(Factors<T>.Hecto);
        public static readonly Units<T> Kilo = new Units<T>(Factors<T>.Kilo);
        public static readonly Units<T> Mega = new Units<T>(Factors<T>.Mega);
        public static readonly Units<T> Micro = new Units<T>(Factors<T>.Micro);
        public static readonly Units<T> Milli = new Units<T>(Factors<T>.Milli);
        public static readonly Units<T> Nano = new Units<T>(Factors<T>.Nano);
        public static readonly Units<T> Peta = new Units<T>(Factors<T>.Peta);
        public static readonly Units<T> Pico = new Units<T>(Factors<T>.Pico);
        public static readonly Units<T> Quecto = new Units<T>(Factors<T>.Quecto);
        public static readonly Units<T> Quetta = new Units<T>(Factors<T>.Quetta);
        public static readonly Units<T> Ronna = new Units<T>(Factors<T>.Ronna);
        public static readonly Units<T> Ronto = new Units<T>(Factors<T>.Ronto);
        public static readonly Units<T> Tera = new Units<T>(Factors<T>.Tera);
        public static readonly Units<T> Yocto = new Units<T>(Factors<T>.Yocto);
        public static readonly Units<T> Yotta = new Units<T>(Factors<T>.Yotta);
        public static readonly Units<T> Zepto = new Units<T>(Factors<T>.Zepto);
        public static readonly Units<T> Zetta = new Units<T>(Factors<T>.Zetta);

        /// <summary>
        /// Initializes a new instance of the <see cref="Units"/> class.
        /// </summary>
        /// <param name="factors">The set of factors that make up this unit.</param>
        public Units(Factor<T> factors)
        {
            this.Factors = factors ?? throw new ArgumentNullException(nameof(factors));
        }

        /// <summary>
        /// Gets the set of factors that make up this unit.
        /// </summary>
        public Factor<T> Factors { get; }

        public static explicit operator Units<T>(string factors) => new Units<T>(new Parser<T>().Parse(factors));

        public static ValueWithUnits<T> operator *(T value, Units<T> units) => new ValueWithUnits<T>(value, units);

        public static Units<T> operator *(Units<T> left, Units<T> right)
        {
            var leftFactors = (left ?? throw new ArgumentNullException(nameof(left))).Factors;
            var rightFactors = (right ?? throw new ArgumentNullException(nameof(right))).Factors;

            return new Units<T>(leftFactors * rightFactors);
        }

        public static ValueWithUnits<T> operator /(T value, Units<T> units)
        {
            var unitsFactors = (units ?? throw new ArgumentNullException(nameof(units))).Factors;
            return new ValueWithUnits<T>(value, new Units<T>(unitsFactors.Pow(-1)));
        }

        public static Units<T> operator /(Units<T> left, Units<T> right)
        {
            var leftFactors = (left ?? throw new ArgumentNullException(nameof(left))).Factors;
            var rightFactors = (right ?? throw new ArgumentNullException(nameof(right))).Factors;

            return new Units<T>(leftFactors / rightFactors);
        }

        public static bool operator ==(Units<T> left, Units<T> right) =>
            left is not null ? left.Equals(right) : right is null;

        public static bool operator !=(Units<T> left, Units<T> right) =>
            !(left == right);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is Units<T> other && this.Equals(other);

        /// <inheritdoc/>
        public bool Equals(Units<T> other) =>
            other is not null && this.Factors == other.Factors;

        /// <inheritdoc/>
        public override int GetHashCode() =>
            this.Factors.GetHashCode();

        public Units<T> Pow(int power) => power == 1 ? this : new Units<T>(this.Factors.Pow(power));

        /// <inheritdoc />
        public override string ToString() => this.Factors.ToString();

        /// <summary>
        /// Contains distance uints.
        /// </summary>
        public static class Distance
        {
            public static readonly Units<T> Meter = new Units<T>(Factors<T>.Meter);
        }

        /// <summary>
        /// Contains electrical capacitance units.
        /// </summary>
        public static class ElectricalCapacitance
        {
            public static readonly Units<T> Farad = new Units<T>(Factors<T>.Farad);
        }

        /// <summary>
        /// Contains electrical inductance units.
        /// </summary>
        public static class ElectricalInductance
        {
            public static readonly Units<T> Henry = new Units<T>(Factors<T>.Henry);
        }

        /// <summary>
        /// Contains electrical resistance units.
        /// </summary>
        public static class ElectricalResistance
        {
            public static readonly Units<T> Ohm = new Units<T>(Factors<T>.Ohm);
        }

        /// <summary>
        /// Contains electric charge units.
        /// </summary>
        public static class ElectricCharge
        {
            public static readonly Units<T> Coulomb = new Units<T>(Factors<T>.Coulomb);
        }

        /// <summary>
        /// Contains electric conductance units.
        /// </summary>
        public static class ElectricConductance
        {
            public static readonly Units<T> Siemens = new Units<T>(Factors<T>.Siemens);
        }

        /// <summary>
        /// Contains electric current units.
        /// </summary>
        public static class ElectricCurrent
        {
            public static readonly Units<T> Ampere = new Units<T>(Factors<T>.Ampere);
        }

        /// <summary>
        /// Contains electric potential units.
        /// </summary>
        public static class ElectricPotential
        {
            public static readonly Units<T> Volt = new Units<T>(Factors<T>.Volt);
        }

        /// <summary>
        /// Contains energy units.
        /// </summary>
        public static class Energy
        {
            public static readonly Units<T> Joule = new Units<T>(Factors<T>.Joule);
        }

        /// <summary>
        /// Contains force units.
        /// </summary>
        public static class Force
        {
            public static readonly Units<T> Newton = new Units<T>(Factors<T>.Newton);
        }

        /// <summary>
        /// Contains frequency units.
        /// </summary>
        public static class Frequency
        {
            public static readonly Units<T> Hertz = new Units<T>(Factors<T>.Hertz);
        }

        /// <summary>
        /// Contains luminous intensity units.
        /// </summary>
        public static class LuminousIntensity
        {
            public static readonly Units<T> Candela = new Units<T>(Factors<T>.Candela);
        }

        /// <summary>
        /// Contains magnetic flux units.
        /// </summary>
        public static class MagneticFlux
        {
            public static readonly Units<T> Weber = new Units<T>(Factors<T>.Weber);
        }

        /// <summary>
        /// Contains magnetic unductance units.
        /// </summary>
        public static class MagneticInductance
        {
            public static readonly Units<T> Tesla = new Units<T>(Factors<T>.Tesla);
        }

        /// <summary>
        /// Contains mass units.
        /// </summary>
        public static class Mass
        {
            public static readonly Units<T> Gram = new Units<T>(Factors<T>.Gram);

            public static readonly Units<T> Kilogram = new Units<T>(Factors<T>.Kilogram);
        }

        /// <summary>
        /// Contains physical units.
        /// </summary>
        public static class Physical
        {
            public static readonly Units<T> JouleSeconds = Energy.Joule * Time.Second;
        }

        /// <summary>
        /// Contains power units.
        /// </summary>
        public static class Power
        {
            public static readonly Units<T> Watt = new Units<T>(Factors<T>.Watt);
        }

        /// <summary>
        /// Contains pressure units.
        /// </summary>
        public static class Pressure
        {
            public static readonly Units<T> Pascal = new Units<T>(Factors<T>.Pascal);
        }

        /// <summary>
        /// Contains quantity units.
        /// </summary>
        public static class Quantity
        {
            public static readonly Units<T> Mole = new Units<T>(Factors<T>.Mole);

            public static readonly Units<T> One = new Units<T>(Factors<T>.One);
        }

        /// <summary>
        /// Contains temperature units.
        /// </summary>
        public static class Temperature
        {
            public static readonly Units<T> Kelvin = new Units<T>(Factors<T>.Kelvin);
        }

        /// <summary>
        /// Contains time units.
        /// </summary>
        public static class Time
        {
            public static readonly Units<T> Second = new Units<T>(Factors<T>.Second);
        }

        /// <summary>
        /// Contains velocity units.
        /// </summary>
        public static class Velocity
        {
            public static readonly Units<T> MetersPerSecond = Distance.Meter / Time.Second;
        }
    }
}
