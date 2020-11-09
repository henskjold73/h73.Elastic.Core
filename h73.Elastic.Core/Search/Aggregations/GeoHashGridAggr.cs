using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class GeoHashGridAggr : Aggr
    {
        [JsonProperty("precision")]
        public int Precision { get; set; }

        public bool ShouldSerializePrecision()
        {
            return Precision >= 1 && Precision <= 12;
        }
    }
}
