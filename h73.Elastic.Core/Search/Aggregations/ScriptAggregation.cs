using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class ScriptAggregation : AggregationBase
    {
        [JsonProperty("avg", NullValueHandling = NullValueHandling.Ignore)]
        public ScriptAggr Avg { get; set; }
    }
}
