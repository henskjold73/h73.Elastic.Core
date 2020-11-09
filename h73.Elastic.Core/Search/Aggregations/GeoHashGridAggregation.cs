using System.Collections.Generic;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class GeoHashGridAggregation : AggregationBase
    {

        public GeoHashGridAggregation(string fieldName, int precision, int? size = null)
        {
            GeoCentroid = new GeoHashGridAggr { Field = fieldName, Precision = precision, Size = size };
        }

        [JsonProperty("geohash_grid")]
        public GeoHashGridAggr GeoCentroid { get; set; }
    }
}
