using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using EngineeringUnits;
using EngineeringUnits;
//using EngineeringUnits.Units;
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
            //while (true)
            //{
            //    Fluid CompressorIn2 = new Fluid(FluidList.Ammonia);
            //    CompressorIn2.UpdatePX(Pressure.FromBar(10), 1);
            //}


            Fluid brine = new Fluid(FluidList.MixPropyleneGlycolAQ);
            brine.SetFraction(0.5);

            brine.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreesCelsius(0));

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

            EvaporatorIn.MassFlow = MassFlow.FromKilogramPerSecond(10);
            EvaporatorIn.UpdatePX(PEvap, 0);
            CompressorOut.UpdatePX(PEvap, 1);

            EvaporatorIn.AddTo(CompressorOut);



            //Evap
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

                //if ((ExpansionValveOut.Enthalpy - EvaporatorIn.Enthalpy).Abs() < SpecificEnergy.FromKilojoulePerKilogram(1))
                //{
                //    break;
                //}

                EvaporatorIn.Copy(ExpansionValveOut);
            }




            Fluid Issue49 = new Fluid(FluidList.R454B_mix);

            Issue49.SetFraction(0.5);

            Pressure P49 = Pressure.FromBar(10);
            Temperature T49 = Temperature.FromDegreesCelsius(50);

            Issue49.UpdatePT(P49, T49);


            //Oil check
            Fluid HotOil = new Fluid(FluidList.Custom_Number13);
            Fluid CoolOil = new Fluid(FluidList.Custom_Number13);

            HotOil.UpdateCustomFluid(Pressure.FromBar(5), Temperature.FromDegreesCelsius(88.9));
            CoolOil.UpdateCustomFluid(Pressure.FromBar(5), Temperature.FromDegreesCelsius(75));

            SpecificEntropy avgCp = (HotOil.Cp + CoolOil.Cp) / 2;

            Enthalpy test1123 = avgCp * (Temperature.FromDegreesCelsius(88.9) - Temperature.FromDegreesCelsius(75));
            Enthalpy test1123K = avgCp * Temperature.FromKelvins(13.9);



            Enthalpy HotoilEnt = Temperature.FromKelvins(362.05) * avgCp;
            Enthalpy CooloilEnt = Temperature.FromKelvins(348.15) * avgCp;

            Enthalpy delta = HotoilEnt - CooloilEnt;


            MassFlow OilFlow = VolumeFlow.FromLiterPerMinute(308.9) * HotOil.Density;


            Power OilCapacity = (HotOil.Enthalpy - CoolOil.Enthalpy) * OilFlow;

            Power OilCapacityFrick = Enthalpy.FromKilojoulePerKilogram(29.3) * OilFlow;



            Fluid AmmoniaGas2 = new Fluid(FluidList.Ammonia);
            Fluid AmmoniaGas4 = new Fluid(FluidList.Ammonia);
            AmmoniaGas2.UpdateXT(0.5, Temperature.FromDegreesCelsius(90));




            AmmoniaGas2.UpdatePT(AmmoniaGas2.Pressure, Temperature.FromDegreesCelsius(25));
            AmmoniaGas2.UpdatePT(AmmoniaGas2.Pressure, Temperature.FromDegreesCelsius(25),0.01);

            AmmoniaGas2.MassFlow = MassFlow.FromKilogramPerSecond(1);

            Pressure localp = AmmoniaGas2.Pressure;
            SpecificEnergy locale = AmmoniaGas2.Enthalpy;
            Temperature localt = AmmoniaGas2.Temperature;


            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            int Count = 100000;

            for (int i = 0; i < Count; i++)
            {
                //AmmoniaGas2.UpdatePH(localp, locale, 0.000001);
                AmmoniaGas4.UpdatePT(Pressure.Zero, localt, 0.000001);
                //AmmoniaGas2.AddTo(AmmoniaGas3);
                //var testsat = AmmoniaGas2.Tsat;
                //var testsat2 = AmmoniaGas2.Tsat;
                //var testsat3 = AmmoniaGas2.Tsat;

                // AmmoniaGas2.Copy(AmmoniaGas2);

                //var testsat4 = AmmoniaGas2.Tsat;


            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds / (double)Count;




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


            WaterOut2.UpdatePH(WaterOut2.Pressure * 1.002, WaterOut2.Enthalpy* 1.002);

            double press = 101000;
            double temp = 273;
            

            //Console.WriteLine(CO2.LimitPressureMin.ToUnit(PressureUnit.Bar));
            //Console.WriteLine(CO2.LimitTemperatureMin.ToUnit(TemperatureUnit.DegreeCelsius));




            //Fluid AmmoniaGas2 = new Fluid(FluidList.Ammonia);
            //AmmoniaGas2.UpdateXT(0.5, Temperature.FromDegreesCelsius(25));



            var testsete = AmmoniaGas2.Tsat;
            var testsete2 = AmmoniaGas2.Tsat;

            AmmoniaGas2.UpdateXT(0.5, Temperature.FromDegreesCelsius(27));
            var testsete3 = AmmoniaGas2.Tsat;

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
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            

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
