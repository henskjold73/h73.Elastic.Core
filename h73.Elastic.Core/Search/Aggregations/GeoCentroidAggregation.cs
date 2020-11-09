using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class GeoCentroidAggregation : AggregationBase
    {
        public GeoCentroidAggregation(string fieldName, int? size = null)
        {
            GeoCentroid = new Aggr { Field = fieldName, Size = size };
        }

        [JsonProperty("geo_centroid")]
        public Aggr GeoCentroid { get; set; }
    }
}
