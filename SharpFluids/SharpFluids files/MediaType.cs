using Newtonsoft.Json;
using System;

namespace SharpFluids
{

    public enum MixType
    {
        None = 0,
        Mass,
        Vol
    }

    public class MediaType : Attribute
    {

        [JsonProperty(PropertyName = "BT", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public string BackendType { get; set; }

        [JsonProperty(PropertyName = "IN", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public string InternalName { get; set; }

        [JsonProperty(PropertyName = "MF", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public double? MassFration { get; set; }

        [JsonProperty(PropertyName = "Mix", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public MixType Mix { get; set; }

        public string DisplayName { get; set; }

        public MediaType()
        {

        }

        public MediaType(string backendType, string internalName, string displayname = "", MixType mix = MixType.None)
        {

            BackendType = backendType;

            InternalName = internalName;

            if (displayname != "")
                DisplayName = displayname;
            else
                DisplayName = InternalName;

            //MassFration = massFration;

            Mix = mix;

        }

        public void Copy(MediaType other)
        {

            BackendType = other.BackendType;
            InternalName = other.InternalName;
            DisplayName = other.DisplayName;
            MassFration = other.MassFration;
            Mix = other.Mix;
        }

        public static bool operator ==(MediaType other1, MediaType other2)
        {

            if (other1 is null || other2 is null)
            {
                return false;
            }
            else if
               (other1.BackendType == other2.BackendType &&
                other1.InternalName == other2.InternalName &&
                other1.DisplayName == other2.DisplayName &&
                other1.MassFration == other2.MassFration &&
                other1.Mix == other2.Mix)
            {
                return true;
            }

            return false;

        }
        public static bool operator !=(MediaType other1, MediaType other2)
        {

            return !(other1 == other2);
        }
    }
}
