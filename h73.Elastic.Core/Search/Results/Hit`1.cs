using System.Collections.Generic;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Results
{
    public class Hit<T>
    {
        [JsonProperty("_index")]
        public string Index { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_score")]
        public float? Score { get; set; }

        [JsonProperty("_source")]
        public T Source { get; set; }

        [JsonProperty("inner_hits", NullValueHandling = NullValueHandling.Ignore)]
        public InnerHitsResult InnerHits { get; set; }
    }

    public class InnerHitsResult : Dictionary<string, InnerHitsResultItem>
    {
    }

    public class InnerHitsResultItem
    {
        [JsonProperty("hits")]
        public Hits<dynamic> Hit { get; set; }
    }
}
