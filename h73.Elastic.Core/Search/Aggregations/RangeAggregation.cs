using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class RangeAggregation<T> : AggregationBase
    {
        public RangeAggregation(string field)
        {
            Range = new RangeAggr<T>{Field = field};
        }

        public RangeAggregation(string field, params RangeAggrValue<T>[] rav)
        {
            Range = new RangeAggr<T>{Field = field, Ranges = rav};
        }

        [JsonProperty("range")] public RangeAggr<T> Range { get; set; }
    }

    public class RangeAggr<T> : Aggr
    {
        [JsonProperty("ranges")] public RangeAggrValue<T>[] Ranges { get; set; }
    }

    public class RangeAggrValue<T>
    {
        [JsonProperty("key")] public string Key { get; set; }
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)] public T From { get; set; }
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)] public T To { get; set; }
    }
}