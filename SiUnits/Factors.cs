// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public static class Factors
    {
        public static readonly NameFactor Ampere;
        public static readonly NumberFactor Atto;
        public static readonly NameFactor Candela;
        public static readonly NumberFactor Centi;
        public static readonly Factor Coulomb;
        public static readonly NumberFactor Deca;
        public static readonly NumberFactor Deci;
        public static readonly NumberFactor Exa;
        public static readonly Factor Farad;
        public static readonly NumberFactor Femto;
        public static readonly NumberFactor Giga;
        public static readonly NameFactor Gram;
        public static readonly NumberFactor Hecto;
        public static readonly Factor Henry;
        public static readonly Factor Hertz;
        public static readonly Factor Joule;
        public static readonly NameFactor Kelvin;
        public static readonly NumberFactor Kilo;
        public static readonly Factor Kilogram;
        public static readonly NumberFactor Mega;
        public static readonly NameFactor Meter;
        public static readonly NumberFactor Micro;
        public static readonly NumberFactor Milli;
        public static readonly NameFactor Mole;
        public static readonly NumberFactor Nano;
        public static readonly Factor Newton;
        public static readonly Factor Ohm;
        public static readonly NumberFactor One;
        public static readonly Factor Pascal;
        public static readonly NumberFactor Peta;
        public static readonly NumberFactor Pico;
        public static readonly NameFactor Second;
        public static readonly Factor Siemens;
        public static readonly NumberFactor Tera;
        public static readonly Factor Tesla;
        public static readonly Factor Volt;
        public static readonly Factor Watt;
        public static readonly Factor Weber;
        public static readonly NumberFactor Yocto;
        public static readonly NumberFactor Yotta;
        public static readonly NumberFactor Zepto;
        public static readonly NumberFactor Zetta;

        [SuppressMessage("Performance", "CA1810:Initialize reference type static fields inline", Justification = "The order of field initialization is important.")]
        static Factors()
        {
            One = new NumberFactor(10, 0);

            Deca = new NumberFactor(10, 1);
            Hecto = new NumberFactor(10, 2);
            Kilo = new NumberFactor(10, 3);
            Mega = new NumberFactor(10, 6);
            Giga = new NumberFactor(10, 9);
            Tera = new NumberFactor(10, 12);
            Peta = new NumberFactor(10, 15);
            Exa = new NumberFactor(10, 18);
            Zetta = new NumberFactor(10, 21);
            Yotta = new NumberFactor(10, 24);
            Deci = new NumberFactor(10, -1);
            Centi = new NumberFactor(10, -2);
            Milli = new NumberFactor(10, -3);
            Micro = new NumberFactor(10, -6);
            Nano = new NumberFactor(10, -9);
            Pico = new NumberFactor(10, -12);
            Femto = new NumberFactor(10, -15);
            Atto = new NumberFactor(10, -18);
            Zepto = new NumberFactor(10, -21);
            Yocto = new NumberFactor(10, -24);

            Ampere = new NameFactor("ampere");
            Candela = new NameFactor("candela");
            Gram = new NameFactor("gram");
            Kelvin = new NameFactor("kelvin");
            Meter = new NameFactor("meter");
            Mole = new NameFactor("mole");
            Second = new NameFactor("second");

            Hertz = Second.Pow(-1);
            Kilogram = Kilo * Gram;
            Newton = Kilogram * Meter / Second.Pow(2);
            Pascal = Newton / Meter.Pow(2);
            Joule = Newton * Meter;
            Watt = Joule / Second;
            Coulomb = Second * Ampere;
            Volt = Watt / Ampere;
            Farad = Coulomb / Volt;
            Ohm = Volt / Ampere;
            Siemens = Ohm.Pow(-1);
            Weber = Volt * Second;
            Tesla = Weber / Meter.Pow(2);
            Henry = Weber / Ampere;
        }

        /// <summary>
        /// Converts the given factor into a constant value.  Only <see cref="NumberFactor">number factors</see> or <see cref="CompositeFactor">composite factors</see>
        /// containing number factors are supported.  To convert a value with additional factors, divide by the expected units first.
        /// </summary>
        /// <param name="factor">The factor to convert to a constant.</param>
        /// <returns>The simplified constant value.</returns>
        public static double AsConstant(this Factor factor) => factor switch
        {
            NumberFactor number => Math.Pow(number.Number, number.Power),
            CompositeFactor composite => composite.Factors.Select(f => f.AsConstant()).Aggregate((a, b) => a * b),
            _ => throw new InvalidOperationException($"Could not convert factor of '{factor}' to a constant."),
        };
    }
}
