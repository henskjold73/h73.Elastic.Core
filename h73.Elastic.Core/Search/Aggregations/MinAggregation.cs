using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class MinAggregation : AggregationBase, IStatAggregation
    {
        public MinAggregation(string fieldName)
        {
            Min = new Aggr {Field = fieldName};
        }
        [JsonProperty("min")] public Aggr Min { get; set; }
    }
}