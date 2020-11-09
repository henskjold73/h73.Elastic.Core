using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class BucketSortAggregation : AggregationBase, IStatAggregation
    {
        [JsonProperty("bucket_sort")] public BucketSort BucketSort { get; set; }
    }

    public class BucketSort
    {
        [JsonProperty("sort")] public Sort[] Sorts { get; set; }
        [JsonProperty("size")] public int? Size { get; set; }
        [JsonProperty("from")] public int? From { get; set; }
    }
}