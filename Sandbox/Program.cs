// See https://aka.ms/new-console-template for more information
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




var input = new Fluid(FluidList.Ammonia);
input.SetFraction(50);


input.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreeCelsius(50));





input.UpdatePH(Pressure.FromBar(15.98), input.Enthalpy);


Enthalpy InputEnthalpy = input.Enthalpy;

input.UpdatePT(input.Pressure, Temperature.FromDegreeCelsius(44));

Enthalpy OutputEnthalpy = input.Enthalpy;


Power capacity = (OutputEnthalpy - InputEnthalpy) * MassFlow.FromKilogramPerSecond(1.595);




Debug.Print($"Vicosity: {input.DynamicViscosity.ToString()}");
Debug.Print($"Coeff. of thermal expansion: {"missing"}");
Debug.Print($"Specific heat capacity: {input.Cp.ToString()}");
Debug.Print($"Thermal conductivity: {input.Conductivity.ToString()}");

input.UpdateDT(Density.FromKilogramPerCubicMeter(592.2), Temperature.FromDegreeCelsius(26));

for (int i = 0; i < 610; i++)
{
    input.UpdateDT(Density.FromKilogramPerCubicMeter(610) - i * Density.FromKilogramPerCubicMeter(1), Temperature.FromDegreeCelsius(26));
    Console.WriteLine($"{input.Density:G5} {input.Pressure} {input.gibbsmolar_excess}");
}






//26.85C and 10.6bar
input.UpdatePT(Pressure.FromBar(10.80381017402795), Temperature.FromDegreeCelsius(26));


input.UpdateXT(0, Temperature.FromKelvin(300)); //{344 kg/m³}
input.UpdateXT(1, Temperature.FromKelvin(300));  //{130.9 kg/m³}



//test.UpdateXT(1, Temperature.FromDegreeCelsius(29.819942151904));  


//test.UpdateDT(Density.FromKilogramsPerCubicMeter(130.9/2), Temperature.FromKelvin(400));


var span = (input.CriticalTemperature - input.LimitTemperatureMin);
var step = span / 1000;

for (int i = 0; i < 1000; i++)
{
    input.UpdateXT(1, input.CriticalTemperature - step*i);
    Console.WriteLine($"{input.umolar}");
}
Console.WriteLine("Hello, World!");

