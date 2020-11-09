using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class MaxAggregation : AggregationBase, IStatAggregation
    {
        public MaxAggregation(string fieldName)
        {
            Max = new Aggr {Field = fieldName};
        }
        [JsonProperty("max")] public Aggr Max { get; set; }
    }
}