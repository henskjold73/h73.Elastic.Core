using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Results
{
    public class AggValue
    {
        [JsonProperty(PropertyName = "value")]
        public double Value { get; set; }
    }


}