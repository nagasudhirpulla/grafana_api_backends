using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GrafanaEdnaApi.Models.Tags
{
    public class TagValueResponse
    {
        public TagValueResponse(string text)
        {
            Text = text;
        }

        [Required]
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
