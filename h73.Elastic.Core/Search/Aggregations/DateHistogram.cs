using h73.Elastic.Core.Enums;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class DateHistogramAggregation : AggregationBase
    {
        public DateHistogramAggregation(string fieldName, string interval, int? minDocCount = null, AggsOrder order = null)
        {
            DateHistogram = new DateHistogramAggr
            {
                Field = fieldName,
                Interval = interval,
                MinDocCount = minDocCount,
                Order = order
            };
        }

        public DateHistogramAggregation(string fieldName, string interval, AggsOrderBy orderBy, AggsOrderDirection orderDirection, int? minDocCount = null)
        {
            DateHistogram = new DateHistogramAggr
            {
                Field = fieldName,
                Interval = interval,
                MinDocCount = minDocCount,
                Order = new AggsOrder(orderBy, orderDirection)
            };
        }

        [JsonProperty("date_histogram")] 
        public DateHistogramAggr DateHistogram { get; set; }
    }

    public class DateHistogramAggr : Aggr
    {
        [JsonProperty("interval")] 
        public string Interval { get; set; }
        [JsonProperty("min_doc_count")] 
        public int? MinDocCount { get; set; }
    }
}