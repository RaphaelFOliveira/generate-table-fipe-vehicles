using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateTableFipeVehicles.Entities
{
    internal class AllVehicles
    {
        [JsonProperty(PropertyName = "modelos")]
        public Vehicles[] Vehicles { get; set; }
    }
}
