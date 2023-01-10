// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Numerics;

    public static class Factors<T>
        where T : IFloatingPoint<T>, IPowerFunctions<T>
    {
        public static readonly NameFactor<T> Ampere;
        public static readonly NumberFactor<T> Atto;
        public static readonly NameFactor<T> Candela;
        public static readonly NumberFactor<T> Centi;
        public static readonly Factor<T> Coulomb;
        public static readonly NumberFactor<T> Deca;
        public static readonly NumberFactor<T> Deci;
        public static readonly NumberFactor<T> Exa;
        public static readonly Factor<T> Farad;
        public static readonly NumberFactor<T> Femto;
        public static readonly NumberFactor<T> Giga;
        public static readonly NameFactor<T> Gram;
        public static readonly NumberFactor<T> Hecto;
        public static readonly Factor<T> Henry;
        public static readonly Factor<T> Hertz;
        public static readonly Factor<T> Joule;
        public static readonly NameFactor<T> Kelvin;
        public static readonly NumberFactor<T> Kilo;
        public static readonly Factor<T> Kilogram;
        public static readonly NumberFactor<T> Mega;
        public static readonly NameFactor<T> Meter;
        public static readonly NumberFactor<T> Micro;
        public static readonly NumberFactor<T> Milli;
        public static readonly NameFactor<T> Mole;
        public static readonly NumberFactor<T> Nano;
        public static readonly Factor<T> Newton;
        public static readonly Factor<T> Ohm;
        public static readonly NumberFactor<T> One;
        public static readonly Factor<T> Pascal;
        public static readonly NumberFactor<T> Peta;
        public static readonly NumberFactor<T> Pico;
        public static readonly NumberFactor<T> Quecto;
        public static readonly NumberFactor<T> Quetta;
        public static readonly NumberFactor<T> Ronna;
        public static readonly NumberFactor<T> Ronto;
        public static readonly NameFactor<T> Second;
        public static readonly Factor<T> Siemens;
        public static readonly NumberFactor<T> Tera;
        public static readonly Factor<T> Tesla;
        public static readonly Factor<T> Volt;
        public static readonly Factor<T> Watt;
        public static readonly Factor<T> Weber;
        public static readonly NumberFactor<T> Yocto;
        public static readonly NumberFactor<T> Yotta;
        public static readonly NumberFactor<T> Zepto;
        public static readonly NumberFactor<T> Zetta;

        [SuppressMessage("Performance", "CA1810:Initialize reference type static fields inline", Justification = "The order of field initialization is important.")]
        static Factors()
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

            Ampere = new NameFactor<T>("ampere");
            Candela = new NameFactor<T>("candela");
            Gram = new NameFactor<T>("gram");
            Kelvin = new NameFactor<T>("kelvin");
            Meter = new NameFactor<T>("meter");
            Mole = new NameFactor<T>("mole");
            Second = new NameFactor<T>("second");

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
    }
}
