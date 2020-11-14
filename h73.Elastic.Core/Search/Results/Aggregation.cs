using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Results
{
    public class Aggregation : Bucket
    {
        [JsonProperty("doc_count", NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include)]
        public new int? DocCount { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include)]
        public double? Value { get; set; }

        [JsonProperty("doc_count_error_upper_bound", NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include)]
        public int? DocCountErrorUpperBound { get; set; }

        [JsonProperty("sum_other_doc_count", NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include)]
        public int? SumOtherDocCount { get; set; }

        [JsonProperty("buckets", NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include)]
        public Bucket[] Buckets { get; set; }

        [JsonProperty("bounds", NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include)]
        public Bounds Bounds { get; set; }

        [JsonProperty("nested", NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include)]
        public new Aggregation Nested { get; set; }
    }
}