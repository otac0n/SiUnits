// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Factors}")]
    public class Units
    {
        public static readonly Units Ampere = new Units(SiUnits.Factors.Ampere);
        public static readonly Units Atto = new Units(SiUnits.Factors.Atto);
        public static readonly Units Candela = new Units(SiUnits.Factors.Candela);
        public static readonly Units Centi = new Units(SiUnits.Factors.Centi);
        public static readonly Units Coulomb = new Units(SiUnits.Factors.Coulomb);
        public static readonly Units Deca = new Units(SiUnits.Factors.Deca);
        public static readonly Units Deci = new Units(SiUnits.Factors.Deci);
        public static readonly Units Exa = new Units(SiUnits.Factors.Exa);
        public static readonly Units Farad = new Units(SiUnits.Factors.Farad);
        public static readonly Units Femto = new Units(SiUnits.Factors.Femto);
        public static readonly Units Giga = new Units(SiUnits.Factors.Giga);
        public static readonly Units Gram = new Units(SiUnits.Factors.Gram);
        public static readonly Units Hecto = new Units(SiUnits.Factors.Hecto);
        public static readonly Units Henry = new Units(SiUnits.Factors.Henry);
        public static readonly Units Hertz = new Units(SiUnits.Factors.Hertz);
        public static readonly Units Joule = new Units(SiUnits.Factors.Joule);
        public static readonly Units Kelvin = new Units(SiUnits.Factors.Kelvin);
        public static readonly Units Kilo = new Units(SiUnits.Factors.Kilo);
        public static readonly Units Kilogram = new Units(SiUnits.Factors.Kilogram);
        public static readonly Units Mega = new Units(SiUnits.Factors.Mega);
        public static readonly Units Meter = new Units(SiUnits.Factors.Meter);
        public static readonly Units Micro = new Units(SiUnits.Factors.Micro);
        public static readonly Units Milli = new Units(SiUnits.Factors.Milli);
        public static readonly Units Mole = new Units(SiUnits.Factors.Mole);
        public static readonly Units Nano = new Units(SiUnits.Factors.Nano);
        public static readonly Units Newton = new Units(SiUnits.Factors.Newton);
        public static readonly Units Ohm = new Units(SiUnits.Factors.Ohm);
        public static readonly Units One = new Units(new NumberFactor(1));
        public static readonly Units Pascal = new Units(SiUnits.Factors.Pascal);
        public static readonly Units Peta = new Units(SiUnits.Factors.Peta);
        public static readonly Units Pico = new Units(SiUnits.Factors.Pico);
        public static readonly Units Second = new Units(SiUnits.Factors.Second);
        public static readonly Units Siemens = new Units(SiUnits.Factors.Siemens);
        public static readonly Units Tera = new Units(SiUnits.Factors.Tera);
        public static readonly Units Tesla = new Units(SiUnits.Factors.Tesla);
        public static readonly Units Unit = new Units(SiUnits.Factors.Unit);
        public static readonly Units Volt = new Units(SiUnits.Factors.Volt);
        public static readonly Units Watt = new Units(SiUnits.Factors.Watt);
        public static readonly Units Weber = new Units(SiUnits.Factors.Weber);
        public static readonly Units Yocto = new Units(SiUnits.Factors.Yocto);
        public static readonly Units Yotta = new Units(SiUnits.Factors.Yotta);
        public static readonly Units Zepto = new Units(SiUnits.Factors.Zepto);
        public static readonly Units Zetta = new Units(SiUnits.Factors.Zetta);

        public Units(Factor factors)
        {
            this.Factors = factors ?? throw new ArgumentNullException(nameof(factors));
        }

        public Factor Factors { get; }

        public static explicit operator Units(string factors) => new Units(new Parser().Parse(factors));

        public static DoubleWithUnits operator *(double value, Units units) => new DoubleWithUnits(value, units);

        public static Units operator *(Units left, Units right) => new Units(left.Factors * right.Factors);

        public static DoubleWithUnits operator /(double value, Units units) => new DoubleWithUnits(value, new Units(units.Factors.Pow(-1)));

        public static Units operator /(Units left, Units right) => new Units(left.Factors / right.Factors);

        public Units Pow(int power) => power == 1 ? this : new Units(this.Factors.Pow(power));

        /// <inheritdoc />
        public override string ToString() => this.Factors.ToString();
    }
}
