using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GrafanaEdnaApi.Models.Query
{
    [Serializable]
    [JsonObject(IsReference = false)]
    public class AdhocFilter
    {
        [Required]
        [JsonProperty("key")]
        public string Key { get; set; }

        [Required]
        [JsonProperty("operator")]
        public string Operator { get; set; }

        [Required]
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
