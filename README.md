[![NuGet](https://img.shields.io/nuget/v/SharpFluids)](https://www.nuget.org/packages/SharpFluids/)
![Platform](https://img.shields.io/badge/platform-win--32%20%7C%20win--64-lightgrey)
[![License](https://img.shields.io/github/license/MadsKirkFoged/SharpFluids)](https://github.com/MadsKirkFogd/SharpFluids/blob/master/LICENSE)


## SharpFluids
Unit-safe fluid properties using [CoolProp] and [EngineeringUnits]

[CoolProp]: http://www.coolprop.org/
[EngineeringUnits]:https://github.com/MadsKirkFoged/EngineeringUnits

### Getting started

Looking up properties for `Ammonia`
```c#
using EngineeringUnits;
using EngineeringUnits.Units;
using SharpFluids;
.
.
.

Fluid R717 = new Fluid(FluidList.Ammonia);
R717.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(100));

Console.WriteLine(R717.Density); // 5.751 kg/m³
Console.WriteLine(R717.DynamicViscosity); // 1.286e-05 Pa·s
```

Available properties

* `Compressibility` 
* `Conductivity` (W/m/K)
* `CriticalPressure` (Pa)
* `CriticalTemperature` (K)
* `Density` - (kg/m3)
* `DynamicViscosity` (Pa*s)
* `Enthalpy` (J/kg)
* `Entropy` (J/kg/K)
* `InternalEnergy` (J/kg)
* `MolarMass` (kg/mol)
* `Phase`
* `Prandtl`
* `Pressure`(Pa)
* `Quality`
* `SoundSpeed` (m/s)
* `SpecificHeat` (J/kg/K)
* `SurfaceTension` (N/m)
* `Temperature` (K)
* `TriplePressure` (Pa)
* `TripleTemperature` (K)


## What is unit-safety?
"As long as you do all your calculation in SI-units.." is the normal saying but if you have tried spending days debugging code to figure out you 'just' had a wrong unit - then unit-safety is your new friend

This is an example of a common unit mistake - With unit-safety you get an error where you did the mistake.
```c#
Mass mass = new Mass(10, MassUnit.Kilogram);
Volume volume = new Volume(4, VolumeUnit.CubicMeter);

Density D1 = mass / volume; // 2.5 kg/m³
Density D2 = volume / mass; // WrongUnitException: 'This is NOT a [kg/m³] as expected! Your Unit is a [m³/kg]'

```
## Converting between units is not your headache anymore

You just input the units you have and ask for the units you want:
```c#
Length length = new Length(5.485, LengthUnit.Inch);
Length height = new Length(12.4, LengthUnit.Centimeter);

Area area = length * height; // 0.01728 m²
Console.WriteLine(area.ToUnit(AreaUnit.SquareFoot)); // 0.186 ft²
Console.WriteLine(area.ToUnit(AreaUnit.SquareCentimeter)); // 172.8 cm²
```

