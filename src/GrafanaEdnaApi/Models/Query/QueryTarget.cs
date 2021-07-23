using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GrafanaEdnaApi.Models.Query
{
    [Serializable]
    [JsonObject(IsReference = false)]
    public class QueryTarget
    {
        [Required]
        [JsonProperty("target")]
        public string Target { get; set; }

        [Required]
        [JsonProperty("refId")]
        public string RefId { get; set; }

        [Required]
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public QueryTargetExtra ExtraData { get; set; } = new();
    }
}
