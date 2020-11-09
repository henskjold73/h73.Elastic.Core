using h73.Elastic.Core.Const;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search
{
    public class HighlightingValue
    {
        public HighlightingValue()
        {
            Type = Strings.Plain;
        }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
