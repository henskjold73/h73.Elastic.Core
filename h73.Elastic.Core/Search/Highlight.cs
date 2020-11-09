using Newtonsoft.Json;

namespace h73.Elastic.Core.Search
{
    public class Highlight
    {
        [JsonProperty("fields")]
        public Fields Fields { get; set; }
    }
}
