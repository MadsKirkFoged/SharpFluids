using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFluids
{

    public enum MixType
    {
        Mass,
        Vol
    }



    public class MediaType : Attribute
    {

        public string BackendType { get; set; }
        public string InternalName { get; set; }
        public double MassFration { get; set; }

        public MixType Mix { get; set; }



        public MediaType(string backendType, string internalName, MixType mix = MixType.Mass, double massFration = 1)
        {

            BackendType = backendType;

            InternalName = internalName;

            MassFration = massFration;

            mix = Mix;

        }






    }
}
