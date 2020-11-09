using System.Collections.Generic;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class Aggr
    {
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }

        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public int? Size { get; set; }

        [JsonProperty("order", NullValueHandling = NullValueHandling.Ignore)]
        public AggsOrder Order { get; set; }

        [JsonProperty("after", NullValueHandling = NullValueHandling.Ignore)]
        public After After { get; set; }

        [JsonProperty("aggs", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, IAggregation> Aggs { get; set; }
    }
}
