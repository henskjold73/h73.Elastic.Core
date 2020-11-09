using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Results
{
    public class NestedBuckets
    {
        public Bucket[] Buckets { get; set; }
    }

    public class Bucket
    {
        [JsonProperty("key")] public string Key { get; set; }
        [JsonProperty("key_as_string")] public string KeyAsString { get; set; }
        [JsonProperty("doc_count")] public int DocCount { get; set; }
        [JsonProperty("nested")] public NestedBuckets Nested { get; set; }
        [JsonProperty("to")] public string To { get; set; }
        [JsonProperty("from")] public string From { get; set; }
        [JsonProperty("sum")] public AggValue Sum { get; set; }
        [JsonProperty("max")] public AggValue Max { get; set; }
        [JsonProperty("min")] public AggValue Min { get; set; }
        [JsonProperty("avg")] public AggValue Avg { get; set; }
        [JsonProperty("cardinality")] public AggValue Cardinality { get; set; }
        [JsonProperty("tophits")] public TopHitsResult TopHits { get; set; }

    }

    public class TopHitsResult
    {
        [JsonProperty("hits")] public Hits<object> Hits { get; set; }
    }

    public class TopHitsResult<T> : TopHitsResult
    {
        [JsonProperty("hits")] public new Hits<T> Hits { get; set; }
    }
}