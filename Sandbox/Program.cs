﻿// See https://aka.ms/new-console-template for more information
using EngineeringUnits;
using SharpFluids;
using System.Diagnostics;

Console.WriteLine("Hello, World!");



//var bug1 = new Fluid(FluidList.MixPropyleneGlycolAQ);


//bug1.Media.MassFration = 40;

//bug1.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreeCelsius(10));
//bug1.UpdatePH(Pressure.FromBar(5), bug1.Enthalpy);


//var bug2 = new Fluid(FluidList.MixPropyleneGlycolAQ);

//bug2.Copy(bug1);
//bug2.UpdatePH(Pressure.FromBar(5), bug1.Enthalpy);




var test = new Fluid(FluidList.Ammonia);


test.UpdateXT(1, Temperature.FromDegreeCelsius(45));
test.UpdatePT(test.Pressure, test.Temperature + Temperature.FromKelvin(50));


Debug.Print($"Vicosity: {test.DynamicViscosity.ToString()}");
Debug.Print($"Coeff. of thermal expansion: {"missing"}");
Debug.Print($"Specific heat capacity: {test.Cp.ToString()}");
Debug.Print($"Thermal conductivity: {test.Conductivity.ToString()}");

test.UpdateDT(Density.FromKilogramPerCubicMeter(592.2), Temperature.FromDegreeCelsius(26));

for (int i = 0; i < 610; i++)
{
    test.UpdateDT(Density.FromKilogramPerCubicMeter(610) - i * Density.FromKilogramPerCubicMeter(1), Temperature.FromDegreeCelsius(26));
    Console.WriteLine($"{test.Density:G5} {test.Pressure} {test.gibbsmolar_excess}");
}






//26.85C and 10.6bar
test.UpdatePT(Pressure.FromBar(10.80381017402795), Temperature.FromDegreeCelsius(26));


test.UpdateXT(0, Temperature.FromKelvin(300)); //{344 kg/m³}
test.UpdateXT(1, Temperature.FromKelvin(300));  //{130.9 kg/m³}



//test.UpdateXT(1, Temperature.FromDegreeCelsius(29.819942151904));  


//test.UpdateDT(Density.FromKilogramsPerCubicMeter(130.9/2), Temperature.FromKelvin(400));


var span = (test.CriticalTemperature - test.LimitTemperatureMin);
var step = span / 1000;

for (int i = 0; i < 1000; i++)
{
    test.UpdateXT(1, test.CriticalTemperature - step*i);
    Console.WriteLine($"{test.umolar}");
}
Console.WriteLine("Hello, World!");

