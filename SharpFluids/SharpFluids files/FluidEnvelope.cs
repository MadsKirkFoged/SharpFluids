//using EngineeringUnits;
using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.Linq;
//using EngineeringUnits.Serialization.JsonNet;

namespace SharpFluids
{
    public partial class Fluid
    {

        public List<(Pressure, SpecificEnergy)> GetEnvelopePhase()
        {

            var localListLiq = new List<(Pressure, SpecificEnergy)>();
            var localListGas = new List<(Pressure, SpecificEnergy)>();
            var CompleteList = new List<(Pressure, SpecificEnergy)>();

            Pressure Increment = (CriticalPressure - LimitPressureMin) / 10000;

            //List<Pressure> PressureList = new List<Pressure>();

            //for (Pressure i = CriticalPressure; i > LimitPressureMin; i -= Increment)
            //{

            //    PressureList.Add(i);

            //    Increment *= 1.10;
            //}

            for (Pressure i = CriticalPressure - Pressure.FromBar(1); i > LimitPressureMin; i -= Increment)
            {
                UpdatePX(i, 0);

                if (!FailState)
                    localListLiq.Add((i, Enthalpy));

                UpdatePX(i, 1);

                if (!FailState)
                    localListGas.Add((i, Enthalpy));

                Increment *= 1.10;
            }

            //localListLiq.Sort((x, y) => y.Item1.CompareTo(x.Item1));

            //We dont want the Critical point to be there twice
            //localListLiq.RemoveAt(0);

            //Merging into one list
            foreach ((Pressure, SpecificEnergy) item in localListLiq.AsEnumerable().Reverse())
                CompleteList.Add(item);

            foreach ((Pressure, SpecificEnergy) item in localListGas)
                CompleteList.Add(item);

            return CompleteList;
        }
    }
}
