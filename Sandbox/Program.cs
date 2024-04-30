// See https://aka.ms/new-console-template for more information
using EngineeringUnits;
using SharpFluids;

Console.WriteLine("Hello, World!");

var test = new Fluid(FluidList.Ammonia);

test.UpdatePX(Pressure.FromBar(45), 0);

Console.WriteLine("Hello, World!");

