SiUnits
=======

[![MIT Licensed](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](license.md)
[![Get it on NuGet](https://img.shields.io/nuget/v/SiUnits.svg?style=flat-square)](http://nuget.org/packages/SiUnits)

[![Appveyor Build](https://img.shields.io/appveyor/ci/otac0n/SiUnits.svg?style=flat-square)](https://ci.appveyor.com/project/otac0n/SiUnits)
[![Test Coverage](https://img.shields.io/codecov/c/github/otac0n/SiUnits.svg?style=flat-square)](https://codecov.io/gh/otac0n/SiUnits)
[![Pre-release packages available](https://img.shields.io/nuget/vpre/SiUnits.svg?style=flat-square)](http://nuget.org/packages/SiUnits)

Getting Started
---------------

```C#
using Units = SiUnits.Factor<double>;

...

var speed = 10 * (Units)"m/s"; // Create a variable containing a speed.
var distance = 2 * (Units)"kilometer"; // Create a variable containing a distance.
var time = distance / speed; // Divide the distance by the speed to obtain a total time.

var timeInSeconds = time / (Units)"second"; // Divide the time by the desired units to obtain a constant.
// Returns 200.0
```

The parser supports many arbitrary forms for units, for example, the following are all equivalent:

* `kilometer / second`
* `km/s`
* `10^3 m s^-1`
* `1/(10^-3*m^-1*s)`
