using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class AvgAggregation : AggregationBase, IStatAggregation
    {
        public AvgAggregation(string fieldName)
        {
            Avg = new Aggr {Field = fieldName};
        }
        [JsonProperty("avg")] public Aggr Avg { get; set; }
    }
}