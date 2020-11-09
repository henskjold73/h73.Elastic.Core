using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class SumAggregation : AggregationBase, IStatAggregation
    {
        public SumAggregation(string fieldName)
        {
            Sum = new Aggr {Field = fieldName};
        }
        [JsonProperty("sum")] public Aggr Sum { get; set; }
    }
}