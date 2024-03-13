//using EngineeringUnits;
//using EngineeringUnits.Units;
namespace Sandbox;

internal class Program
{
    private static void Main(string[] args)
    {
        //var R6001 = new Fluid(FluidList.R600);
        //var R6002 = new Fluid(FluidList.R600);
        //var R6003 = new Fluid(FluidList.R600);
        //var R6004 = new Fluid(FluidList.R600);

        //R6001.UpdatePX(Pressure.FromBar(2), 1);
        //R6002.UpdatePT(Pressure.FromBar(2), R6001.Temperature + Temperature.FromKelvin(5));
        //R6003.UpdatePT(Pressure.FromBar(2), R6001.Temperature + Temperature.FromKelvin(20));
        //R6004.UpdatePT(Pressure.FromBar(2), R6001.Temperature + Temperature.FromKelvin(50));
        //_ = new Fluid(FluidList.Water);
        ////Water.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(30));

        ////Fluid WaterInCOMP = new Fluid(FluidList.InCompWater);
        ////WaterInCOMP.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(30));

        //var brine = new Fluid(FluidList.MixPropyleneGlycolAQ);
        //brine.SetFraction(50);
        //brine.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(-5));

        //var brineCopy = new Fluid(FluidList.MixPropyleneGlycolAQ);

        //brineCopy.Copy(brine);
        //brineCopy.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(-5));

        //var brine2 = new Fluid(FluidList.MixPropyleneGlycolAQ);
        //brine2.SetFraction(0);
        //brine2.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(30));

        //var Ammonia = new Fluid(FluidList.Ammonia);
        //Ammonia.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(25));

        //var CO2 = new Fluid(FluidList.CO2);
        //CO2.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(25));

        ////Setting up the fluids
        //var CompressorIn = new Fluid(FluidList.Ammonia);
        //var CompressorOut = new Fluid(FluidList.Ammonia);

        //var CondenserIn = new Fluid(FluidList.Ammonia);
        //var CondenserOut = new Fluid(FluidList.Ammonia);

        //var ExpansionValveIn = new Fluid(FluidList.Ammonia);
        //var ExpansionValveOut = new Fluid(FluidList.Ammonia);

        //var EvaporatorIn = new Fluid(FluidList.Ammonia);
        //var EvaporatorOut = new Fluid(FluidList.Ammonia);

        ////Setting for heatpump
        //var PEvap = Pressure.FromBar(10);
        //var Pcond = Pressure.FromBar(20);
        //var SuperHeat = Temperature.FromKelvins(10);
        //var SubCooling = Temperature.FromKelvins(5);

        //EvaporatorIn.MassFlow = MassFlow.FromKilogramPerSecond(10);
        //EvaporatorIn.UpdatePX(PEvap, 0);
        //CompressorOut.UpdatePX(PEvap, 1);

        //_=EvaporatorIn.AddTo(CompressorOut);

        ////Evap
        //while (true)
        //{
        //    EvaporatorOut.UpdatePX(PEvap, 1);

        //    //Adding superheat to evap
        //    EvaporatorOut.UpdatePT(EvaporatorOut.Pressure, EvaporatorOut.Temperature + SuperHeat);

        //    //Compresser
        //    CompressorIn.Copy(EvaporatorOut);
        //    CompressorOut.UpdatePS(Pcond, CompressorIn.Entropy);
        //    SpecificEnergy H2s = CompressorOut.Enthalpy;

        //    //Compressor equation
        //    SpecificEnergy h2 = ((H2s - CompressorIn.Enthalpy) / 0.85) + CompressorIn.Enthalpy;
        //    CompressorOut.UpdatePH(Pcond, h2);

        //    CondenserIn.Copy(CompressorOut);
        //    CondenserOut.UpdatePX(CondenserIn.Pressure, 0);
        //    CondenserOut.UpdatePT(CondenserOut.Pressure, CondenserOut.Temperature - SubCooling);

        //    ExpansionValveIn.Copy(CondenserOut);
        //    ExpansionValveOut.UpdatePH(EvaporatorIn.Pressure, ExpansionValveIn.Enthalpy);

        //    //if ((ExpansionValveOut.Enthalpy - EvaporatorIn.Enthalpy).Abs() < SpecificEnergy.FromKilojoulePerKilogram(1))
        //    //{
        //    //    break;
        //    //}

        //    EvaporatorIn.Copy(ExpansionValveOut);
        //}

        //var Issue49 = new Fluid(FluidList.R454B_mix);

        //Issue49.SetFraction(0.5);

        //var P49 = Pressure.FromBar(10);
        //var T49 = Temperature.FromDegreeCelsius(50);

        //Issue49.UpdatePT(P49, T49);

        ////Oil check
        //var HotOil = new Fluid(FluidList.Custom_Number13);
        //var CoolOil = new Fluid(FluidList.Custom_Number13);

        //HotOil.UpdateCustomFluid(Pressure.FromBar(5), Temperature.FromDegreeCelsius(88.9));
        //CoolOil.UpdateCustomFluid(Pressure.FromBar(5), Temperature.FromDegreeCelsius(75));

        //SpecificEntropy avgCp = (HotOil.Cp + CoolOil.Cp) / 2;

        //Enthalpy test1123 = avgCp * (Temperature.FromDegreeCelsius(88.9) - Temperature.FromDegreeCelsius(75));
        //Enthalpy test1123K = avgCp * Temperature.FromKelvins(13.9);

        //Enthalpy HotoilEnt = Temperature.FromKelvins(362.05) * avgCp;
        //Enthalpy CooloilEnt = Temperature.FromKelvins(348.15) * avgCp;

        //Enthalpy delta = HotoilEnt - CooloilEnt;

        //MassFlow OilFlow = VolumeFlow.FromLiterPerMinute(308.9) * HotOil.Density;

        //Power OilCapacity = (HotOil.Enthalpy - CoolOil.Enthalpy) * OilFlow;

        //Power OilCapacityFrick = Enthalpy.FromKilojoulePerKilogram(29.3) * OilFlow;

        //var AmmoniaGas2 = new Fluid(FluidList.Ammonia);
        //var AmmoniaGas4 = new Fluid(FluidList.Ammonia);
        //AmmoniaGas2.UpdateXT(0.5, Temperature.FromDegreeCelsius(90));

        //AmmoniaGas2.UpdatePT(AmmoniaGas2.Pressure, Temperature.FromDegreeCelsius(25));
        //AmmoniaGas2.UpdatePT(AmmoniaGas2.Pressure, Temperature.FromDegreeCelsius(25), (Ratio)0.01);

        //AmmoniaGas2.MassFlow = MassFlow.FromKilogramPerSecond(1);

        //Pressure localp = AmmoniaGas2.Pressure;
        //SpecificEnergy locale = AmmoniaGas2.Enthalpy;
        //Temperature localt = AmmoniaGas2.Temperature;

        //var watch = System.Diagnostics.Stopwatch.StartNew();
        //// the code that you want to measure comes here

        //var Count = 100000;

        //for (var i = 0; i < Count; i++)
        //{
        //    //AmmoniaGas2.UpdatePH(localp, locale, 0.000001);
        //    AmmoniaGas4.UpdatePT(Pressure.Zero, localt, (Ratio)0.000001);
        //    //AmmoniaGas2.AddTo(AmmoniaGas3);
        //    //var testsat = AmmoniaGas2.Tsat;
        //    //var testsat2 = AmmoniaGas2.Tsat;
        //    //var testsat3 = AmmoniaGas2.Tsat;

        //    // AmmoniaGas2.Copy(AmmoniaGas2);

        //    //var testsat4 = AmmoniaGas2.Tsat;

        //}

        //watch.Stop();
        //var elapsedMs = watch.ElapsedMilliseconds / (double)Count;

        //var WaterIn = new Fluid(FluidList.Water);
        //var WaterOut = new Fluid(FluidList.Water);

        //var WaterIn2 = new Fluid(FluidList.Water);
        //var WaterOut2 = new Fluid(FluidList.Water);

        //WaterIn.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreeCelsius(50));
        //WaterOut.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreeCelsius(60));

        //WaterIn2.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreeCelsius(50));
        //WaterOut2.UpdatePT(Pressure.FromBar(3.5), Temperature.FromDegreeCelsius(60));

        //Power power = (WaterOut.Enthalpy - WaterIn.Enthalpy) * MassFlow.FromKilogramPerSecond(16.9);
        //Power power2 = (WaterOut2.Enthalpy - WaterIn2.Enthalpy) * MassFlow.FromKilogramPerSecond(16.9);

        //Power Diff = (power2 - power).Abs();

        //WaterOut2.UpdatePH(WaterOut2.Pressure * 1.002, WaterOut2.Enthalpy* 1.002);

        ////Console.WriteLine(CO2.LimitPressureMin.ToUnit(PressureUnit.Bar));
        ////Console.WriteLine(CO2.LimitTemperatureMin.ToUnit(TemperatureUnit.DegreeCelsius));

        ////Fluid AmmoniaGas2 = new Fluid(FluidList.Ammonia);
        ////AmmoniaGas2.UpdateXT(0.5, Temperature.FromDegreeCelsius(25));

        //Temperature testsete = AmmoniaGas2.Tsat;
        //Temperature testsete2 = AmmoniaGas2.Tsat;

        //AmmoniaGas2.UpdateXT(0.5, Temperature.FromDegreeCelsius(27));
        //Temperature testsete3 = AmmoniaGas2.Tsat;

        //var testingTemp = new Temperature(-50, TemperatureUnit.DegreeCelsius);

        //var AmmoniaGas = new Fluid(FluidList.Ammonia);
        //var AmmoniaLiq = new Fluid(FluidList.Ammonia);
        //var AmmoniaSub = new Fluid(FluidList.Ammonia);
        //var AmmoniaSuper = new Fluid(FluidList.Ammonia);
        //AmmoniaGas.UpdateXT(1, testingTemp);
        //AmmoniaSuper.UpdatePT(AmmoniaGas.Pressure, AmmoniaGas.Temperature + Temperature.FromKelvins(25));

        //AmmoniaLiq.UpdateXT(0, testingTemp);
        //AmmoniaSub.UpdatePT(AmmoniaLiq.Pressure, AmmoniaLiq.Temperature - Temperature.FromKelvins(25));

        //Debug.Print(AmmoniaGas.Pressure.Bar.ToString(new CultureInfo("en-US")));

        //Debug.Print(AmmoniaGas.Enthalpy.KilojoulePerKilogram.ToString(new CultureInfo("en-US")));
        //Debug.Print(AmmoniaLiq.Enthalpy.KilojoulePerKilogram.ToString(new CultureInfo("en-US")));
        //Debug.Print(AmmoniaSuper.Enthalpy.KilojoulePerKilogram.ToString(new CultureInfo("en-US")));
        //Debug.Print(AmmoniaSub.Enthalpy.KilojoulePerKilogram.ToString(new CultureInfo("en-US")));

        //Debug.Print((1/AmmoniaSuper.Density.KilogramPerCubicMeter).ToString(new CultureInfo("en-US")));

        //Log.Logger = new LoggerConfiguration()
        //    .WriteTo.Debug()
        //    .CreateLogger();

        //Log.Information("Hello, world!");
        //Fluid Water;
        ////Fluid Water = new Fluid(FluidList.Water);
        //Water.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreeCelsius(40));

        //var Water2 = new Fluid(FluidList.Water);
        //Water2.UpdatePT(Pressure.FromBar(20), Temperature.FromDegreeCelsius(40));

        //Power Capacity = (Water.Enthalpy - Water2.Enthalpy) * MassFlow.FromKilogramPerSecond(0.1541);

        //var Meg = new Fluid(FluidList.MixEthyleneGlycolAQ);
        //Meg.SetFraction(0.1);
        //for (var i = 0; i <= 100; i++)
        //{
        //    Meg.UpdatePT(Pressure.FromAtmospheres(1), Temperature.FromDegreeCelsius(i));
        //    Console.WriteLine("Specific Heat of MEG at " + i + " °C and fraction 0.1: " + Meg.Cp.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin));
        //    Console.WriteLine("Density of MEG at " + i + " °C and fraction 0.1: " + Meg.Density.KilogramPerCubicMeter);
        //    Console.WriteLine("Dynamic Viscosity of MEG at " + i + " °C and fraction 0.1: " + Meg.DynamicViscosity.MillipascalSeconds);
        //}

        //Meg.SetFraction(0.2);
        //for (var i = 0; i <= 100; i++)
        //{
        //    Meg.UpdatePT(Pressure.FromAtmospheres(1), Temperature.FromDegreeCelsius(i));
        //    Console.WriteLine("Specific Heat of MEG at " + i + " °C and fraction 0.2: " + Meg.Cp.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin));
        //    Console.WriteLine("Density of MEG at " + i + " °C and fraction 0.2: " + Meg.Density.KilogramPerCubicMeter);
        //    Console.WriteLine("Dynamic Viscosity of MEG at " + i + " °C and fraction 0.2: " + Meg.DynamicViscosity.MillipascalSeconds);
        //}

        //var Air = new MoistAir();

        //Air.UpdateAir(Pressure.FromBar(1),
        //        DryBulbTemperature: Temperature.FromDegreeCelsius(20),
        //        RelativeHumidity: 0.7

        //        );

        //Air.UpdateAir(Pressure.FromBar(1), HumidityRatio: 0.010397090640274141, RelativeHumidity: 0.7);
        ////Air.UpdateAir(Pressure.FromSI(101325), HumidityRatio: 0.011, RelativeHumidity: 0.8);

        ////Water.UpdateXT(1, Temperature.FromDegreeCelsius(20));

        ////Fluid Ammonia = new Fluid(FluidList.Ammonia);

        //List<(Pressure, SpecificEnergy)> test11 = Ammonia.GetEnvelopePhase();

        //Ammonia.UpdatePX(Pressure.FromBar(25), 0.5);

        //Ammonia.UpdateDH(Ammonia.Density, Ammonia.Enthalpy);

        //Ammonia.UpdatePT(Pressure.FromSI(2293443.57087332), Temperature.FromSI(405.55873230045091));

        //Ammonia.UpdatePT(Pressure.FromSI(2493443), Temperature.FromSI(410));

        //var r134a = new Fluid(FluidList.R134a);
        //r134a.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(75));
        //SharpFluids.Phases phase = r134a.Phase;

        //var length = new Length(5.485, LengthUnit.Inch);
        //var height = new Length(12.4, LengthUnit.Centimeter);

        //Area area = length * height; // 0.01728 m²
        //Console.WriteLine(area.ToUnit(AreaUnit.SquareFoot)); // 0.186 ft²
        //Console.WriteLine(area.ToUnit(AreaUnit.SquareCentimeter)); // 172.8 cm²

        //Console.WriteLine(area.GetHashCode());

        //var R717 = new Fluid(FluidList.Ammonia);
        //R717.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(100));

        //Fluid R717Clone = R717.Clone();

        //Console.WriteLine(R717.Density); // 5.751 kg/m³
        //Console.WriteLine(R717.DynamicViscosity); // 1.286e-05 Pa·s

        ////Fluid test12 = new Fluid(FluidList.Custom_SHC226E);

        ////test12.updateAir("H","T",298.15,"P",101325,"R",0.5);

        //Air.UpdateAir(Pressure.FromBar(1),
        //                    DryBulbTemperature: Temperature.FromKelvins(298.15),
        //                    RelativeHumidity: 0.5

        //                    );

        //Debug.Print($"WetBulbTemperature: {Air.WetBulbTemperature}");
        //Debug.Print($"DewPointTemperature: {Air.DewPointTemperature}");
        //Debug.Print($"DryBulbTemperature: {Air.Temperature}");

        //Debug.Print($"RelativeHumidity: {Air.RelativeHumidity * 100} % ");
        //Debug.Print($"HumidityRatio: {Air.HumidityRatio} kg/kg");

        //Enthalpy H = Air.Enthalpy;

        //var totalcount = 10000;
        ////var watch = System.Diagnostics.Stopwatch.StartNew();

        //for (var i = 0; i < totalcount; i++)
        //{
        //    Air.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(10));
        //    Air.UpdatePT(Pressure.FromBar(2), Temperature.FromDegreeCelsius(300));
        //}

        //watch.Stop();
        //Log.Information($"Time: {watch.ElapsedTicks / totalcount}");

        //var jsonString1 = JsonConvert.SerializeObject(Air);
        //Fluid testJSON = JsonConvert.DeserializeObject<Fluid>(jsonString1);
        //var jsonString2 = JsonConvert.SerializeObject(testJSON);

        //Debug.Print($"{jsonString1 == jsonString2}");

        //testJSON.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(10));

        //var howManyBytes = jsonString1.Length * sizeof(char);

        //Debug.Print($"Size is: {howManyBytes}");

        ////................................................

        //var test = new Fluid(FluidList.Water);

        //test.UpdatePT(Pressure.FromBar(3), Temperature.FromDegreeCelsius(20));
        //test.MassFlow = MassFlow.FromKilogramPerSecond(1);

        ////for (int i = 0; i < 100000; i++)
        ////{
        ////    test.AddPower(Power.FromWatts(1));
        ////}

        //Debug.Print("");

        //////Arrange
        ////foreach (FluidList suit in (FluidList[])Enum.GetValues(typeof(FluidList)))
        ////{
        ////    //Arrange
        ////    Fluid TestFluid = new Fluid(suit);

        ////    using (var loggerFactory = LoggerFactory.Create(builder =>
        ////    { builder.AddConsole(); }))

        ////    {
        ////        ILogger logger = loggerFactory.CreateLogger<Program>();
        ////        //logger.LogInformation("Logging has stared");
        ////        TestFluid.Log = logger;

        ////    }

        ////    Debug.Print(TestFluid.Media.InternalName);
        ////    TestFluid.Log.LogInformation(TestFluid.Media.InternalName);

        ////    TestFluid.GetEnvelopePhase();

        ////}

        //////This is want we are aiming for
        ////Entropy Aim = Entropy.FromJoulesPerKelvin(1699.7);

        ////Temperature Max = my_fluid.LimitTemperatureMax;
        ////Temperature Min = my_fluid.LimitTemperatureMin;
        ////Temperature Mid = Temperature.Zero;   

        ////for (int i = 0; i < 20; i++)
        ////{

        ////    Mid = Temperature.FromKelvins((Max.Kelvins + Min.Kelvins) / 2);

        ////    my_fluid.UpdatePT(Pressure.FromBar(10), Mid);

        ////    if (my_fluid.Entropy > Aim) 
        ////        Max = Mid;
        ////    else
        ////        Min = Mid;

        ////}

        ////my_fluid.UpdatePX(Pressure.FromBar(9), 0.0);

        //// Fluid test = new Fluid(FluidList.Water);
        //test.UpdatePT(Pressure.FromBar(-10), Temperature.FromDegreeCelsius(300));
        //test.UpdatePT(Pressure.FromBar(100000), Temperature.FromDegreeCelsius(300));
        //test.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(30000));

        //test.UpdatePH(Pressure.FromBar(2), SpecificEnergy.FromKilojoulePerKilogram(10740.5));

        //test.MassFlow = MassFlow.FromKilogramPerSecond(-0.0054);
        //_=test.AddPower(Power.FromWatts(-86982.42));

        ////Fluid r134a = new Fluid(FluidList.R134a);
        //r134a.UpdatePT(Pressure.FromBar(2), Temperature.FromDegreeCelsius(13));
        //var densité = r134a.Density.MilligramsPerCubicMeter.ToString();

        //Density Result = r134a.Density;

        ////Arrange
        ////Fluid R717 = new Fluid(FluidList.Ammonia);
        //var setDensity = Density.FromKilogramPerCubicMeter(15.36622602626586);
        //var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(1043420.2106074861);
        //var setMass = Mass.FromKilogram(43);

        //setMass = setMass.ToUnit(MassUnit.Decagram);

        ////Act
        //R717.UpdateDH(setDensity, setEnthalpy);
        //R717.Mass = setMass;

        ////Save as JSON
        //var json = R717.SaveAsJSON();

        ////Start new fluid and load as json
        //Fluid R717JSON = R717.LoadFromJSON(json);

        //Temperature test1 = R717JSON.Tsat;

        ////Find the Density of water at 13°C
        ////Fluid Water = new Fluid(FluidList.Water);
        //Water.UpdatePT(Pressure.FromBar(1.013), Temperature.FromDegreeCelsius(13));
        //Console.WriteLine("Density of water at 13°C: " + Water.Density);

        //Water.UpdateHS(SpecificEnergy.FromJoulePerKilogram(54697.59), SpecificEntropy.FromJoulePerKilogramKelvin(195.27));

        //Debug.Print("Density of water is: " + Water.Density);

        ////Giving water a Massflow
        //Water.MassFlow = MassFlow.FromKilogramPerHour(100);

        ////What is the volumeFlow for this water?
        //Console.WriteLine("VolumeFlow of the water: " + Water.VolumeFlow);

        ////What is the boiling point of the water?
        //Water.UpdatePX(Pressure.FromBar(1.013), 0);             //X=0 it is 100% liquid and 0% gas but at it boiling point
        //Console.WriteLine("Boiling point of this water is: " + Water.Temperature);

        ////..and if you want to display it in another unit
        //Console.WriteLine("Boiling point of this water is: " + Water.Temperature.ToUnit(TemperatureUnit.DegreeCelsius));

        ////Display Dynamic Viscosity of the water
        //Console.WriteLine("Dynamic Viscosity of this water is: " + Water.DynamicViscosity);

        ////Create a CO2 fluid
        //var ref1 = new Fluid(FluidList.CO2);

        ////Update it with 25bar and X=1
        //ref1.UpdatePX(Pressure.FromBar(25), 1);

        //Console.WriteLine(ref1.Cp);
        //Console.WriteLine(ref1.Temperature);
        //Console.WriteLine("Prandtl is : " + ref1.Prandtl);
        //Console.WriteLine("Cp is : " + ref1.Cp);
        //Console.WriteLine("Cv is : " + ref1.Cv);
        //Console.WriteLine("Surface Tension is : " + ref1.SurfaceTension);
        //Console.WriteLine("Density is : " + ref1.Density);

        ////A Fluid that is a mix of Ammonia and water
        //var ref2 = new Fluid(FluidList.MixAmmoniaAQ);

        ////Set the fraction between ammonia(80%) and water(20%)
        //ref2.SetFraction(0.3);

        ////Update it with 10Bar and 10°C
        //ref2.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(10));

        ////Copy fluid type
        //var ref3 = new Fluid();
        //ref3.CopyType(ref1);
        //ref3.UpdatePX(Pressure.FromBar(25), 1);

        //Console.WriteLine(ref3.Cp);
        //Console.WriteLine(ref3.Temperature);
        //Console.WriteLine("Prandtl is : " + ref3.Prandtl);
        //Console.WriteLine("Cp is : " + ref3.Cp);
        //Console.WriteLine("Cv is : " + ref3.Cv);
        //Console.WriteLine("Surface Tension is : " + ref3.SurfaceTension);

        ////Saving and loading to JSON
        //var ref4 = new Fluid();
        //var JSON = ref3.SaveAsJSON();
        //ref4 = ref4.LoadFromJSON(JSON);

        //Console.WriteLine(ref4.Cp);
        //Console.WriteLine(ref4.Temperature);
        //Console.WriteLine("Prandtl is : " + ref4.Prandtl);
        //Console.WriteLine("Cp is : " + ref4.Cp);
        //Console.WriteLine("Cv is : " + ref4.Cv);
        //Console.WriteLine("Surface Tension is : " + ref4.SurfaceTension);

        //ref4.UpdatePX(Pressure.FromBar(45), 1);

        //Console.WriteLine(ref4.Cp);
        //Console.WriteLine(ref4.Temperature);
        //Console.WriteLine("Prandtl is : " + ref4.Prandtl);
        //Console.WriteLine("Cp is : " + ref4.Cp);
        //Console.WriteLine("Cv is : " + ref4.Cv);
        //Console.WriteLine("Surface Tension is : " + ref4.SurfaceTension);

        //ref4.UpdatePX(Pressure.FromBar(25), 1);

        //Console.WriteLine(ref4.Cp);
        //Console.WriteLine(ref4.Temperature);
        //Console.WriteLine("Prandtl is : " + ref4.Prandtl);
        //Console.WriteLine("Cp is : " + ref4.Cp);
        //Console.WriteLine("Cv is : " + ref4.Cv);
        //Console.WriteLine("Surface Tension is : " + ref4.SurfaceTension);

        //Console.WriteLine(ref2.Density);

        //var ref5 = new Fluid();
        //ref5.Copy(ref4);

        //_=Console.ReadKey();
    }
}
