// See https://aka.ms/new-console-template for more information
using EngineeringUnits;
using SharpFluids;

Console.WriteLine("Hello, World!");

var test = new Fluid(FluidList.Ammonia);

test.UpdateDT(Density.FromKilogramsPerCubicMeter(9), Temperature.FromKelvin(400));

Console.WriteLine("Hello, World!");

