using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Results
{
    public class Hits<T>
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("max_score")]
        public float? MaxScore { get; set; }

        [JsonProperty("hits")]
        public Hit<T>[] HitsList { get; set; }
    }
}
