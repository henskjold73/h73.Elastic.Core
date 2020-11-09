using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class HistogramAggregation<T> : AggregationBase
    {
        public HistogramAggregation(string field, T interval, int? minDocCount = null)
        {
            Histogram = new IntervalAggr<T> {Field = field, Interval = interval};
        }
        [JsonProperty("histogram")] public IntervalAggr<T> Histogram { get; set; }
    }

    public class IntervalAggr<T> : Aggr
    {
        [JsonProperty("interval", NullValueHandling = NullValueHandling.Ignore)]
        public T Interval { get; set; }

        [JsonProperty("min_doc_count", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinDocCount { get; set; }
    }
}