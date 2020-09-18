using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;
using SharpFluids;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {

            //................................................

            //Find the Density of water at 13°C
            Fluid Water = new Fluid(FluidList.Water);
            Water.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(13));
            Console.WriteLine("Density of water at 13°C: " + Water.Density);

            //Giving water a Massflow
            Water.MassFlow = MassFlow.FromKilogramsPerHour(100);

            //What is the volumeFlow for this water?
            Console.WriteLine("VolumeFlow of the water: " + Water.VolumeFlow);

            //What is the boiling point of the water?
            Water.UpdatePX(Pressure.FromBars(1.013), 0);             //X=0 it is 100% liquid and 0% gas but at it boiling point
            Console.WriteLine("Boiling point of this water is: " + Water.Temperature);

            //..and if you want to display it in another unit
            Console.WriteLine("Boiling point of this water is: " + Water.Temperature.ToUnit(TemperatureUnit.DegreeCelsius));

            //Display Dynamic Viscosity of the water
            Console.WriteLine("Dynamic Viscosity of this water is: " + Water.Viscosity);




            //Create a CO2 fluid
            Fluid ref1 = new Fluid(FluidList.CO2);

            //Update it with 25bar and X=1
            ref1.UpdatePX(Pressure.FromBars(25), 1);

            Console.WriteLine(ref1.Cp);
            Console.WriteLine(ref1.Temperature);
            Console.WriteLine("Prandtl is : " + ref1.Prandtl);
            Console.WriteLine("Cp is : " + ref1.Cp);
            Console.WriteLine("Cv is : " + ref1.Cv);
            Console.WriteLine("Surface Tension is : " + ref1.SurfaceTension);


            //A Fluid that is a mix of Ammonia and water
            Fluid ref2 = new Fluid(FluidList.MixAmmoniaAQ);


            //Set the fraction between ammonia(80%) and water(20%)
            ref2.SetFraction(0.2);

            //Update it with 10bars and 10°C
            ref2.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(10));


            //Copy fluid type
            Fluid ref3 = new Fluid();
            ref3.CopyType(ref1);
            ref3.UpdatePX(Pressure.FromBars(25), 1);

            Console.WriteLine(ref3.Cp);
            Console.WriteLine(ref3.Temperature);
            Console.WriteLine("Prandtl is : " + ref3.Prandtl);
            Console.WriteLine("Cp is : " + ref3.Cp);
            Console.WriteLine("Cv is : " + ref3.Cv);
            Console.WriteLine("Surface Tension is : " + ref3.SurfaceTension);

            //Saving and loading to JSON
            Fluid ref4 = new Fluid();
            string JSON = ref3.SaveAsJSON();
            ref4 = ref4.LoadFromJSON(JSON);

            Console.WriteLine(ref4.Cp);
            Console.WriteLine(ref4.Temperature);
            Console.WriteLine("Prandtl is : " + ref4.Prandtl);
            Console.WriteLine("Cp is : " + ref4.Cp);
            Console.WriteLine("Cv is : " + ref4.Cv);
            Console.WriteLine("Surface Tension is : " + ref4.SurfaceTension);

            ref4.UpdatePX(Pressure.FromBars(45), 1);

            Console.WriteLine(ref4.Cp);
            Console.WriteLine(ref4.Temperature);
            Console.WriteLine("Prandtl is : " + ref4.Prandtl);
            Console.WriteLine("Cp is : " + ref4.Cp);
            Console.WriteLine("Cv is : " + ref4.Cv);
            Console.WriteLine("Surface Tension is : " + ref4.SurfaceTension);

            ref4.UpdatePX(Pressure.FromBars(25), 1);

            Console.WriteLine(ref4.Cp);
            Console.WriteLine(ref4.Temperature);
            Console.WriteLine("Prandtl is : " + ref4.Prandtl);
            Console.WriteLine("Cp is : " + ref4.Cp);
            Console.WriteLine("Cv is : " + ref4.Cv);
            Console.WriteLine("Surface Tension is : " + ref4.SurfaceTension);

            Console.WriteLine(ref2.Density);

            Console.ReadKey();
        }
    }
}
