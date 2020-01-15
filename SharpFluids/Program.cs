using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace SharpFluids
{
    class Program
    {


        static void Main(string[] args)
        {

            //Create a CO2 fluid
            Fluid ref1 = new Fluid(FluidList.CO2);

            //Update it with 25bar and X=1
            ref1.UpdatePX(Pressure.FromBars(25), 1);

            Console.WriteLine(ref1.Cp);
            Console.WriteLine(ref1.Temperature);
            Console.WriteLine(ref1.Temperature.DegreesCelsius + " °C");


            //A Fluid that is a mix of Ammonia and water
            Fluid ref2 = new Fluid(FluidList.MixAmmoniaAQ);

            //Set the fraction between ammonia(80%) and water(20%)
            ref2.SetFraction(0.2);

            //Update it with 10bars and 10°C
            ref2.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(10));

            Console.WriteLine(ref2.RHO);

            Console.ReadKey();


        }
    }

}
