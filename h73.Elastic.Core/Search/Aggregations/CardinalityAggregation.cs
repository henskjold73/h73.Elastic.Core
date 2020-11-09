using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class CardinalityAggregation : AggregationBase, IStatAggregation
    {
        public CardinalityAggregation(string fieldName)
        {
            Cardinality = new Aggr {Field = fieldName};
        }
        [JsonProperty("cardinality")] public Aggr Cardinality { get; set; }
    }
}