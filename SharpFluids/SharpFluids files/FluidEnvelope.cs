
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using UnitsNet;
using EngineeringUnits;
//using UnitsNet.Serialization.JsonNet;

namespace SharpFluids
{
    public partial class Fluid
    {



        public List<(Pressure, SpecificEnergy)> GetEnvelopePhase()
        {

            List<(Pressure, SpecificEnergy)> localListLiq = new List<(Pressure, SpecificEnergy)>();
            List<(Pressure, SpecificEnergy)> localListGas = new List<(Pressure, SpecificEnergy)>();
            List<(Pressure, SpecificEnergy)> CompleteList = new List<(Pressure, SpecificEnergy)>();



            Pressure Increment = (CriticalPressure - LimitPressureMin) / (100000);

            //List<Pressure> PressureList = new List<Pressure>();



            //for (Pressure i = CriticalPressure; i > LimitPressureMin; i -= Increment)
            //{

            //    PressureList.Add(i);


            //    Increment *= 1.10;
            //}





            for (Pressure i = CriticalPressure; i > LimitPressureMin; i -= Increment)
            {
                UpdatePX(i, 0);

                if (!FailState)
                    localListLiq.Add((Pressure, Enthalpy));


                UpdatePX(i, 1);

                if (!FailState)
                    localListGas.Add((Pressure, Enthalpy));

                Increment *= 1.10;
            }


            //We dont want the Critical point to be there twice
            //localListLiq.RemoveAt(0);

            //Merging into one list
            foreach (var item in localListLiq.AsEnumerable().Reverse())
                CompleteList.Add(item);

            foreach (var item in localListGas)
                CompleteList.Add(item);


            return CompleteList;
        }




    }
}
