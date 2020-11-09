using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class NestedAggregation : AggregationBase
    {
        public NestedAggregation(string path)
        {
            Nested = new Aggr {Path = path};
        }
        [JsonProperty("nested")] public Aggr Nested { get; set; }
    }
}