using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class CompositeAggregation : AggregationBase
    {
        [JsonProperty("composite")]
        public CompositeAggr Composite { get; set; }
    }
}