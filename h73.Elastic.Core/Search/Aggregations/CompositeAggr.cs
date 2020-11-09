using System.Collections.Generic;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class CompositeAggr : Aggr
    {
        [JsonProperty("sources")]
        public List<IAggregation> Sources { get; set; }
    }
}