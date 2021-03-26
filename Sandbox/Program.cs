using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;
using SharpFluids;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {

            //................................................


           



            //MediaType MediaFluid = new MediaType("HEOS", "R513A.MIX");
            //Fluid my_fluid = new Fluid(MediaFluid);

            Fluid my_fluid = new Fluid(FluidList.R513A_mix);


            my_fluid.UpdatePX(Pressure.FromBars(10), 1);


            my_fluid.GetListOfPreMix();


            using (var loggerFactory = LoggerFactory.Create(builder =>
            { builder.AddConsole(); }))

            {
                ILogger logger = loggerFactory.CreateLogger<Program>();
                logger.LogInformation("Logging has stared");
                my_fluid.Log = logger;

            }

            my_fluid.Log.LogInformation(my_fluid.Media.InternalName);


            //Arrange
            foreach (FluidList suit in (FluidList[])Enum.GetValues(typeof(FluidList)))
            {
                //Arrange
                Fluid TestFluid = new Fluid(suit);


                using (var loggerFactory = LoggerFactory.Create(builder =>
                { builder.AddConsole(); }))

                {
                    ILogger logger = loggerFactory.CreateLogger<Program>();
                    //logger.LogInformation("Logging has stared");
                    TestFluid.Log = logger;

                }


                Debug.Print(TestFluid.Media.InternalName);
                TestFluid.Log.LogInformation(TestFluid.Media.InternalName);

                //Act
                Pressure Reduce = TestFluid.CriticalPressure - Pressure.FromBars(5);
                if (Reduce < TestFluid.LimitPressureMin)
                    Reduce = TestFluid.LimitPressureMin;


                TestFluid.UpdatePX(Reduce, 0.5);



            }











            //This is want we are aiming for
            Entropy Aim = Entropy.FromJoulesPerKelvin(1699.7);


            Temperature Max = my_fluid.LimitTemperatureMax;
            Temperature Min = my_fluid.LimitTemperatureMin;
            Temperature Mid = Temperature.Zero;   

            
            for (int i = 0; i < 20; i++)
            {

                Mid = Temperature.FromKelvins((Max.Kelvins + Min.Kelvins) / 2);

                my_fluid.UpdatePT(Pressure.FromBars(10), Mid);


                if (my_fluid.Entropy > Aim) 
                    Max = Mid;
                else
                    Min = Mid;

            }







            



            my_fluid.UpdatePX(Pressure.FromBars(9), 0.0);

            Fluid test = new Fluid(FluidList.Water);
            test.UpdatePT(Pressure.FromBars(-10), Temperature.FromDegreesCelsius(300));
            test.UpdatePT(Pressure.FromBars(100000), Temperature.FromDegreesCelsius(300));
            test.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(30000));

            test.UpdatePH(Pressure.FromBars(2), SpecificEnergy.FromKilojoulesPerKilogram(10740.5));

            test.MassFlow = MassFlow.FromKilogramsPerSecond(-0.0054);
            test.AddPower(Power.FromWatts(-86982.42));




            Fluid r134a = new Fluid(FluidList.R134a);
            r134a.UpdatePT(Pressure.FromBars(2), Temperature.FromDegreesCelsius(13));
            string densité = r134a.Density.MilligramsPerCubicMeter.ToString();

            Density Result = r134a.Density;




            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            Mass setMass = Mass.FromKilograms(43);

            setMass = setMass.ToUnit(MassUnit.Decagram);



            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.Mass = setMass;

            //Save as JSON
            string json = R717.SaveAsJSON();


            //Start new fluid and load as json
            Fluid R717JSON = R717.LoadFromJSON(json);

            Temperature test1 = R717JSON.Tsat;









            //Find the Density of water at 13°C
            Fluid Water = new Fluid(FluidList.Water);
            Water.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(13));
            Console.WriteLine("Density of water at 13°C: " + Water.Density);

            

            Water.UpdateHS(SpecificEnergy.FromJoulesPerKilogram(54697.59), Entropy.FromJoulesPerKelvin(195.27));

            Debug.Print("Density of water is: " + Water.Density);

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
            Console.WriteLine("Dynamic Viscosity of this water is: " + Water.DynamicViscosity);




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
            Console.WriteLine("Density is : " + ref1.Density);


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

            Fluid ref5 = new Fluid();
            ref5.Copy(ref4);

            Console.ReadKey();
        }
    }
}
