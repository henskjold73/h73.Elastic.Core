using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class ScriptAggr
    {
        [JsonProperty("script")]
        public string Script { get; set; }
    }
}
