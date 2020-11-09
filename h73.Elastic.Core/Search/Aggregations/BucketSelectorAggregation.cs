using System.Collections.Generic;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class BucketSelectorAggregation : AggregationBase, IStatAggregation
    {
        [JsonProperty("bucket_selector")]
        public BucketSelector BucketSelector { get; set; }
    }

    public class BucketSelector
    {
        [JsonProperty("buckets_path")]
        public Dictionary<string,string> BucketsPath { get; set; }
        [JsonProperty("script")]
        public string Script { get; set; }
    }
}