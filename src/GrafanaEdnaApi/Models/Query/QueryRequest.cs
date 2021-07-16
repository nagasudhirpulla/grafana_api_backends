using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GrafanaEdnaApi.Models.Query
{
    [Serializable]
    [JsonObject(IsReference = false)]
    public class QueryRequest
    {
        [JsonProperty("format")]
        public string Format { get; set; }

        [Required]
        [JsonProperty("intervalMs")]
        public int IntervalMs { get; set; }

        [Required]
        [JsonProperty("maxDataPoints")]
        public int MaxDataPoints { get; set; }

        [Required]
        [JsonProperty("range")]
        public QueryDateTimeRange Range { get; set; }

        [Required]
        [JsonProperty("targets")]
        public QueryTarget[] Targets { get; set; }

        [JsonProperty("adhocFilters")]
        public AdhocFilter[] AdhocFilters { get; set; }
    }
}
