using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrafanaEdnaApi.Models.Response
{
    public class TargetResponse
    {
        public TargetResponse(string target, List<List<double>> dataPoints)
        {
            Target = target;
            DataPoints = dataPoints;
        }

        [Required]
        [JsonProperty("target")]
        public string Target { get; }

        [Required]
        [JsonProperty("datapoints")]
        public List<List<double>> DataPoints { get; }

    }
}
