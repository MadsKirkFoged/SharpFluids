//using EngineeringUnits;
using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SharpFluids;

namespace UnitsTests;

[TestClass]
public class JsonTests
{

    [TestMethod]
    public void JSONToObjectAndBack()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);

        var jsonString1 = JsonConvert.SerializeObject(R717);
        Fluid R717Json = JsonConvert.DeserializeObject<Fluid>(jsonString1);
        var jsonString2 = JsonConvert.SerializeObject(R717Json);

        var setDensity = Density.FromKilogramPerCubicMeter(15.362783819911829);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(1045846.5098055181);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        R717.MassFlow = setMassFlow;
        R717Json.UpdateDH(setDensity, setEnthalpy);
        R717Json.MassFlow = setMassFlow;

        var jsonString3 = JsonConvert.SerializeObject(R717);
        _ = JsonConvert.DeserializeObject<Fluid>(jsonString3);
        var jsonString4 = JsonConvert.SerializeObject(R717);

        var jsonString5 = JsonConvert.SerializeObject(R717Json);
        Fluid JSON3 = JsonConvert.DeserializeObject<Fluid>(jsonString5);
        var jsonString6 = JsonConvert.SerializeObject(JSON3);

        //Assert
        Assert.AreEqual(jsonString1, jsonString2);
        Assert.AreEqual(jsonString3, jsonString4);
        Assert.AreEqual(jsonString3, jsonString5);
        Assert.AreEqual(jsonString3, jsonString6);
    }
}