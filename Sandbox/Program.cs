// See https://aka.ms/new-console-template for more information
using EngineeringUnits;
using SharpFluids;

Console.WriteLine("Hello, World!");

//var bug1 = new Fluid(FluidList.MixPropyleneGlycolAQ);


//bug1.Media.MassFration = 40;

//bug1.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreeCelsius(10));
//bug1.UpdatePH(Pressure.FromBar(5), bug1.Enthalpy);


//var bug2 = new Fluid(FluidList.MixPropyleneGlycolAQ);

//bug2.Copy(bug1);
//bug2.UpdatePH(Pressure.FromBar(5), bug1.Enthalpy);




var test = new Fluid(FluidList.Ammonia);

//test.UpdateDT(Density.FromKilogramsPerCubicMeter(9), Temperature.FromKelvin(400));

//26.85C and 10.6bar
test.UpdatePT(Pressure.FromBar(10.6), Temperature.FromDegreeCelsius(26));

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

