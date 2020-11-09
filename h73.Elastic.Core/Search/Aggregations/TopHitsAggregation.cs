using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class TopHitsAggregation : AggregationBase, IStatAggregation
    {
        [JsonProperty("top_hits")] public TopHits TopHits { get; set; }
    }

    public class TopHits : BucketSort
    {
        [JsonIgnore] public Source SourceObject { get; set; }
        [JsonProperty("_source")] public object Source => SourceObject.Output;
    }
}