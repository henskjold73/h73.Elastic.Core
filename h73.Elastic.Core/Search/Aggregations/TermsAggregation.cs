using h73.Elastic.Core.Enums;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class TermsAggregation : AggregationBase
    {
        public TermsAggregation(string fieldName, int? size = null, AggsOrder order = null)
        {
            Terms = new Aggr {Field = fieldName, Size = size, Order = order};
        }

        public TermsAggregation(string fieldName, AggsOrderBy orderBy, AggsOrderDirection orderDirection,
            int? size = null)
        {
            Terms = new Aggr {Field = fieldName, Size = size, Order = new AggsOrder(orderBy, orderDirection)};
        }
        [JsonProperty("terms")] public Aggr Terms { get; set; }
    }
}