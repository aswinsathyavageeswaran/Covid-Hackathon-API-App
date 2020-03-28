using Newtonsoft.Json;
using System;
using Microsoft.Azure.Cosmos.Spatial;

namespace HackCovidAPICore.Model
{
    public class BusinessTypeModel
    {
        [JsonProperty(PropertyName = "TypeId")]
        public int TypeId { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

    }
}
