using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using UnitsNet;
using EngineeringUnits;
//using UnitsNet.Units;
using SharpFluids;
using Newtonsoft.Json;
using System.Diagnostics;
using EngineeringUnits.Units;
using Serilog;
using System.Globalization;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {

            Fluid WaterIn = new Fluid(FluidList.Water);
            Fluid WaterOut = new Fluid(FluidList.Water);

            Fluid WaterIn2 = new Fluid(FluidList.Water);
            Fluid WaterOut2 = new Fluid(FluidList.Water);

            WaterIn.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreesCelsius(50));
            WaterOut.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreesCelsius(60));

            WaterIn2.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreesCelsius(50));
            WaterOut2.UpdatePT(Pressure.FromBar(3.5), Temperature.FromDegreesCelsius(60));

            Power power = (WaterOut.Enthalpy - WaterIn.Enthalpy) * MassFlow.FromKilogramPerSecond(16.9);
            Power power2 = (WaterOut2.Enthalpy - WaterIn2.Enthalpy) * MassFlow.FromKilogramPerSecond(16.9);

            Power Diff = (power2 - power).Abs();


            double press = 101000;
            double temp = 273;
            Fluid Oil = new Fluid(FluidList.Custom_SHC226E);

            Oil.UpdatePT(Pressure.FromBar(28.38), Temperature.FromDegreesCelsius(-40));

            //Console.WriteLine(CO2.LimitPressureMin.ToUnit(PressureUnit.Bar));
            //Console.WriteLine(CO2.LimitTemperatureMin.ToUnit(TemperatureUnit.DegreeCelsius));




            Fluid AmmoniaGas2 = new Fluid(FluidList.Ammonia);
            AmmoniaGas2.UpdateXT(1, Temperature.FromDegreesCelsius(25));


            Temperature testingTemp = new Temperature(-50,TemperatureUnit.DegreeCelsius);

            Fluid AmmoniaGas = new Fluid(FluidList.Ammonia);
            Fluid AmmoniaLiq = new Fluid(FluidList.Ammonia);
            Fluid AmmoniaSub = new Fluid(FluidList.Ammonia);
            Fluid AmmoniaSuper = new Fluid(FluidList.Ammonia);
            AmmoniaGas.UpdateXT(1, testingTemp);
            AmmoniaSuper.UpdatePT(AmmoniaGas.Pressure, AmmoniaGas.Temperature + Temperature.FromKelvins(25));

            AmmoniaLiq.UpdateXT(0, testingTemp);
            AmmoniaSub.UpdatePT(AmmoniaLiq.Pressure, AmmoniaLiq.Temperature - Temperature.FromKelvins(25));

            Debug.Print(AmmoniaGas.Pressure.Bar.ToString(new CultureInfo("en-US")));

            Debug.Print(AmmoniaGas.Enthalpy.KilojoulePerKilogram.ToString(new CultureInfo("en-US")));
            Debug.Print(AmmoniaLiq.Enthalpy.KilojoulePerKilogram.ToString(new CultureInfo("en-US")));
            Debug.Print(AmmoniaSuper.Enthalpy.KilojoulePerKilogram.ToString(new CultureInfo("en-US")));
            Debug.Print(AmmoniaSub.Enthalpy.KilojoulePerKilogram.ToString(new CultureInfo("en-US")));

            Debug.Print((1/AmmoniaSuper.Density.KilogramPerCubicMeter).ToString(new CultureInfo("en-US")));

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .CreateLogger();

            Log.Information("Hello, world!");

            Fluid Water = new Fluid(FluidList.Water);
            Water.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreesCelsius(40));

            Fluid Water2 = new Fluid(FluidList.Water);
            Water2.UpdatePT(Pressure.FromBar(20), Temperature.FromDegreesCelsius(40));


            Power Capacity = (Water.Enthalpy - Water2.Enthalpy) * MassFlow.FromKilogramPerSecond(0.1541);




            Fluid Meg = new Fluid(FluidList.MixEthyleneGlycolAQ);
            Meg.SetFraction(0.1);
            for (var i = 0; i <= 100; i++)
            {
                Meg.UpdatePT(Pressure.FromAtmospheres(1), Temperature.FromDegreesCelsius(i));
                Console.WriteLine("Specific Heat of MEG at " + i + " °C and fraction 0.1: " + Meg.Cp.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin));
                Console.WriteLine("Density of MEG at " + i + " °C and fraction 0.1: " + Meg.Density.KilogramsPerCubicMeter);
                Console.WriteLine("Dynamic Viscosity of MEG at " + i + " °C and fraction 0.1: " + Meg.DynamicViscosity.MillipascalSeconds);
            }

            Meg.SetFraction(0.2);
            for (var i = 0; i <= 100; i++)
            {
                Meg.UpdatePT(Pressure.FromAtmospheres(1), Temperature.FromDegreesCelsius(i));
                Console.WriteLine("Specific Heat of MEG at " + i + " °C and fraction 0.2: " + Meg.Cp.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin));
                Console.WriteLine("Density of MEG at " + i + " °C and fraction 0.2: " + Meg.Density.KilogramsPerCubicMeter);
                Console.WriteLine("Dynamic Viscosity of MEG at " + i + " °C and fraction 0.2: " + Meg.DynamicViscosity.MillipascalSeconds);
            }




            MoistAir Air = new MoistAir();

            Air.UpdateAir(Pressure.FromBars(1),
                    DryBulbTemperature: Temperature.FromDegreesCelsius(20),
                    RelativeHumidity: 0.7

                    );

            Air.UpdateAir(Pressure.FromBars(1), HumidityRatio: 0.010397090640274141, RelativeHumidity: 0.7);
            //Air.UpdateAir(Pressure.FromSI(101325), HumidityRatio: 0.011, RelativeHumidity: 0.8);





            
            //Water.UpdateXT(1, Temperature.FromDegreesCelsius(20));



            Fluid Ammonia = new Fluid(FluidList.Ammonia);


            var test11 = Ammonia.GetEnvelopePhase();


            Ammonia.UpdatePX(Pressure.FromBars(25), 0.5);

            Ammonia.UpdateDH(Ammonia.Density, Ammonia.Enthalpy);


            Ammonia.UpdatePT(Pressure.FromSI(2293443.57087332), Temperature.FromSI(405.55873230045091));

            Ammonia.UpdatePT(Pressure.FromSI(2493443), Temperature.FromSI(410));


            Fluid r134a = new Fluid(FluidList.R134a);
            r134a.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(75));
            var phase = r134a.Phase;



            Length length = new Length(5.485, LengthUnit.Inch);
            Length height = new Length(12.4, LengthUnit.Centimeter);

            Area area = length * height; // 0.01728 m²
            Console.WriteLine(area.ToUnit(AreaUnit.SquareFoot)); // 0.186 ft²
            Console.WriteLine(area.ToUnit(AreaUnit.SquareCentimeter)); // 172.8 cm²

            Console.WriteLine(area.GetHashCode());

            Fluid R717 = new Fluid(FluidList.Ammonia);
            R717.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(100));

            Fluid R717Clone = R717.Clone();

            Console.WriteLine(R717.Density); // 5.751 kg/m³
            Console.WriteLine(R717.DynamicViscosity); // 1.286e-05 Pa·s

            //Fluid test12 = new Fluid(FluidList.Custom_SHC226E);
            


            //test12.updateAir("H","T",298.15,"P",101325,"R",0.5);


            Air.UpdateAir(   Pressure.FromBars(1), 
                                DryBulbTemperature: Temperature.FromKelvins(298.15),
                                RelativeHumidity: 0.5

                                );

            Debug.Print($"WetBulbTemperature: {Air.WetBulbTemperature}");
            Debug.Print($"DewPointTemperature: {Air.DewPointTemperature}");
            Debug.Print($"DryBulbTemperature: {Air.Temperature}");

            Debug.Print($"RelativeHumidity: {Air.RelativeHumidity * 100} % ");
            Debug.Print($"HumidityRatio: {Air.HumidityRatio} kg/kg");








            Enthalpy H = Air.Enthalpy;


            int totalcount = 10000;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            

            for (int i = 0; i < totalcount; i++)
            {
                Air.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(10));
                Air.UpdatePT(Pressure.FromBars(2), Temperature.FromDegreesCelsius(300));
            }
            
            
            watch.Stop();
            Log.Information($"Time: {watch.ElapsedTicks / totalcount}");



            string jsonString1 = JsonConvert.SerializeObject(Air);
            Fluid testJSON = JsonConvert.DeserializeObject<Fluid>(jsonString1);
            string jsonString2 = JsonConvert.SerializeObject(testJSON);

            Debug.Print($"{jsonString1 == jsonString2}");

            testJSON.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(10));



            var howManyBytes = jsonString1.Length * sizeof(Char);


            Debug.Print($"Size is: {howManyBytes}");



            //................................................

            Fluid test = new Fluid(FluidList.Water);

            test.UpdatePT(Pressure.FromBars(3), Temperature.FromDegreesCelsius(20));
            test.MassFlow = MassFlow.FromKilogramsPerSecond(1);


            //for (int i = 0; i < 100000; i++)
            //{
            //    test.AddPower(Power.FromWatts(1));
            //}

            Debug.Print("");




            ////Arrange
            //foreach (FluidList suit in (FluidList[])Enum.GetValues(typeof(FluidList)))
            //{
            //    //Arrange
            //    Fluid TestFluid = new Fluid(suit);


            //    using (var loggerFactory = LoggerFactory.Create(builder =>
            //    { builder.AddConsole(); }))

            //    {
            //        ILogger logger = loggerFactory.CreateLogger<Program>();
            //        //logger.LogInformation("Logging has stared");
            //        TestFluid.Log = logger;

            //    }


            //    Debug.Print(TestFluid.Media.InternalName);
            //    TestFluid.Log.LogInformation(TestFluid.Media.InternalName);

            //    TestFluid.GetEnvelopePhase();



            //}











            ////This is want we are aiming for
            //Entropy Aim = Entropy.FromJoulesPerKelvin(1699.7);


            //Temperature Max = my_fluid.LimitTemperatureMax;
            //Temperature Min = my_fluid.LimitTemperatureMin;
            //Temperature Mid = Temperature.Zero;   


            //for (int i = 0; i < 20; i++)
            //{

            //    Mid = Temperature.FromKelvins((Max.Kelvins + Min.Kelvins) / 2);

            //    my_fluid.UpdatePT(Pressure.FromBars(10), Mid);


            //    if (my_fluid.Entropy > Aim) 
            //        Max = Mid;
            //    else
            //        Min = Mid;

            //}











            //my_fluid.UpdatePX(Pressure.FromBars(9), 0.0);

            // Fluid test = new Fluid(FluidList.Water);
            test.UpdatePT(Pressure.FromBars(-10), Temperature.FromDegreesCelsius(300));
            test.UpdatePT(Pressure.FromBars(100000), Temperature.FromDegreesCelsius(300));
            test.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(30000));

            test.UpdatePH(Pressure.FromBars(2), SpecificEnergy.FromKilojoulesPerKilogram(10740.5));

            test.MassFlow = MassFlow.FromKilogramsPerSecond(-0.0054);
            test.AddPower(Power.FromWatts(-86982.42));




            //Fluid r134a = new Fluid(FluidList.R134a);
            r134a.UpdatePT(Pressure.FromBars(2), Temperature.FromDegreesCelsius(13));
            string densité = r134a.Density.MilligramsPerCubicMeter.ToString();

            Density Result = r134a.Density;




            //Arrange
            //Fluid R717 = new Fluid(FluidList.Ammonia);
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
            //Fluid Water = new Fluid(FluidList.Water);
            Water.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(13));
            Console.WriteLine("Density of water at 13°C: " + Water.Density);

            

            Water.UpdateHS(SpecificEnergy.FromJoulesPerKilogram(54697.59), SpecificEntropy.FromJoulesPerKilogramKelvin(195.27));

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
            ref2.SetFraction(0.3);

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
