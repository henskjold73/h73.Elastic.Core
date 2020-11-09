using System.Collections.Generic;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class AggregationBase : IAggregation
    {
        [JsonProperty("aggs", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, IAggregation> Aggregations { get; set; }

        [JsonProperty("filter", NullValueHandling = NullValueHandling.Ignore)]
        public IFilter Filter { get; set; }
    }
}
