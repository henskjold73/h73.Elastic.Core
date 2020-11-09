using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class GeoBox
    {
        public GeoBox(string topLeft, string bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        [JsonProperty("top_left")]
        public string TopLeft { get; set; }

        [JsonProperty("bottom_right")]
        public string BottomRight { get; set; }
    }
}
