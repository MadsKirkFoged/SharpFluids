[![NuGet](https://img.shields.io/nuget/v/SharpFluids)](https://www.nuget.org/packages/SharpFluids/)
[![NuGet](https://img.shields.io/nuget/dt/SharpFluids)](https://www.nuget.org/packages/SharpFluids/)
![Platform](https://img.shields.io/badge/Platform-64%2F32bit-brightgreen)
[![License](https://img.shields.io/github/license/MadsKirkFoged/SharpFluids)](https://github.com/MadsKirkFoged/SharpFluids/blob/master/LICENSE)


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

## Simple example simulating a heat pump
```c#
//Setting up the fluids
 Fluid CompressorIn = new Fluid(FluidList.Ammonia);
 Fluid CompressorOut = new Fluid(FluidList.Ammonia);

 Fluid CondenserIn = new Fluid(FluidList.Ammonia);
 Fluid CondenserOut = new Fluid(FluidList.Ammonia);

 Fluid ExpansionValveIn = new Fluid(FluidList.Ammonia);
 Fluid ExpansionValveOut = new Fluid(FluidList.Ammonia);

 Fluid EvaporatorIn = new Fluid(FluidList.Ammonia);
 Fluid EvaporatorOut = new Fluid(FluidList.Ammonia);

 //Setting for heatpump
 Pressure PEvap = Pressure.FromBar(10);
 Pressure Pcond = Pressure.FromBar(20);
 Temperature SuperHeat = Temperature.FromKelvins(10);
 Temperature SubCooling = Temperature.FromKelvins(5);

 //Starting guess for EvaporatorIn
 EvaporatorIn.UpdatePX(PEvap, 0);


 //Solving the heat pump
 while (true) 
 {
     EvaporatorOut.UpdatePX(PEvap, 1);

     //Adding superheat to evap
     EvaporatorOut.UpdatePT(EvaporatorOut.Pressure, EvaporatorOut.Temperature + SuperHeat);

     //Compresser
     CompressorIn.Copy(EvaporatorOut);
     CompressorOut.UpdatePS(Pcond, CompressorIn.Entropy);
     SpecificEnergy H2s = CompressorOut.Enthalpy;

     //Compressor equation
     SpecificEnergy h2 = ((H2s - CompressorIn.Enthalpy) / 0.85) + CompressorIn.Enthalpy;
     CompressorOut.UpdatePH(Pcond, h2);


     CondenserIn.Copy(CompressorOut);
     CondenserOut.UpdatePX(CondenserIn.Pressure, 0);
     CondenserOut.UpdatePT(CondenserOut.Pressure, CondenserOut.Temperature - SubCooling);

     ExpansionValveIn.Copy(CondenserOut);
     ExpansionValveOut.UpdatePH(EvaporatorIn.Pressure, ExpansionValveIn.Enthalpy);

     //Break out of the loop if it is stable
     if ((ExpansionValveOut.Enthalpy - EvaporatorIn.Enthalpy).Abs() < SpecificEnergy.FromKilojoulePerKilogram(1))
     {
         break;
     }

     EvaporatorIn.Copy(ExpansionValveOut);
 }
```
