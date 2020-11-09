using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class GeoBoundingBoxAggr
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("wrap_longitude")]
        public bool WrapLongitude { get; set; }
    }
}
