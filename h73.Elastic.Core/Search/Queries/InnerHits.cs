using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    public class InnerHits
    {
        [JsonProperty(PropertyName = "size", NullValueHandling = NullValueHandling.Ignore)]
        public int? Size { get; set; }
    }
}