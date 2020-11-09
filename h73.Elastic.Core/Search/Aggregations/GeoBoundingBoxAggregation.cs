using System.Collections.Generic;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class GeoBoundingBoxAggregation : AggregationBase
    {
        public GeoBoundingBoxAggregation(string fieldName, bool wrapLongitude = true)
        {
            GeoBounds = new GeoBoundingBoxAggr { Field = fieldName, WrapLongitude = wrapLongitude };
        }

        [JsonProperty("geo_bounds")]
        public GeoBoundingBoxAggr GeoBounds { get; set; }
    }
}
