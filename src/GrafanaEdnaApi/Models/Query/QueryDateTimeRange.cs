using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GrafanaEdnaApi.Models.Query
{
    [Serializable]
    [JsonObject(IsReference = false)]
    public class QueryDateTimeRange
    {
        [Required]
        [JsonProperty("from")]
        public DateTime From { get; set; }

        [Required]
        [JsonProperty("to")]
        public DateTime To { get; set; }
    }
}
