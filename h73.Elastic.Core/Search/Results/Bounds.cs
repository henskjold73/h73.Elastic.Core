using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Results
{
    public class Bounds
    {
        [JsonProperty("top_left")]
        public Point TopLeft { get; set; }

        [JsonProperty("bottom_right")]
        public Point BottomRight { get; set; }
    }
}
