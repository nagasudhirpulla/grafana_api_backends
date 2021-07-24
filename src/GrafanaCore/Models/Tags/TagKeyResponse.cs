using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrafanaCore.Models.Tags
{
    public class TagKeyResponse
    {
        public TagKeyResponse(string type, string text)
        {
            Type = type;
            Text = text;
        }

        [Required]
        [JsonProperty("type")]
        public string Type { get; set; }

        [Required]
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
