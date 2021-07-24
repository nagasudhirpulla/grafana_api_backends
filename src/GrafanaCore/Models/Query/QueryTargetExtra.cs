using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace GrafanaCore.Models.Query
{
    public class QueryTargetExtra
    {
        [JsonProperty("pnt")]
        public string Pnt { get; set; }

        [JsonProperty("name")]
        public string PntName { get; set; }

        [JsonProperty("period")]
        public int SamplingPeriod { get; set; } = 60;

        [JsonProperty("fetchShiftDays")]
        public int FetchShiftDays { get; set; } = 0;

        [JsonProperty("fetchShiftHrs")]
        public int FetchShiftHrs { get; set; } = 0;

        [JsonProperty("fetchShiftMins")]
        public int FetchShiftMins { get; set; } = 0;

        [JsonProperty("fetchShiftSecs")]
        public int FetchShiftSecs { get; set; } = 0;

        [JsonProperty("fetchFuture")]
        public bool FetchFuture { get; set; } = true;

        // extra fields
        [JsonExtensionData]
        private IDictionary<string, JToken> _extraStuff;
    }
}
