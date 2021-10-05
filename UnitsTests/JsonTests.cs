using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
//using UnitsNet;
using EngineeringUnits;
using Newtonsoft.Json;

namespace UnitsTests
{
    [TestClass]
    public class JsonTests
    {

        [TestMethod]
        public void JSONToObjectAndBack()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);

            string jsonString1 = JsonConvert.SerializeObject(R717);
            Fluid R717Json = JsonConvert.DeserializeObject<Fluid>(jsonString1);
            string jsonString2 = JsonConvert.SerializeObject(R717Json);

            Density setDensity = Density.FromKilogramsPerCubicMeter(15.362783819911829);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1045846.5098055181);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;
            R717Json.UpdateDH(setDensity, setEnthalpy);
            R717Json.MassFlow = setMassFlow;



            string jsonString3 = JsonConvert.SerializeObject(R717);
            Fluid JSON2 = JsonConvert.DeserializeObject<Fluid>(jsonString3);
            string jsonString4 = JsonConvert.SerializeObject(R717);

            string jsonString5 = JsonConvert.SerializeObject(R717Json);
            Fluid JSON3 = JsonConvert.DeserializeObject<Fluid>(jsonString5);
            string jsonString6 = JsonConvert.SerializeObject(JSON3);

            //Assert
            Assert.AreEqual(jsonString1, jsonString2);
            Assert.AreEqual(jsonString3, jsonString4);
            Assert.AreEqual(jsonString3, jsonString5);
            Assert.AreEqual(jsonString3, jsonString6);
        }
        

    }
}