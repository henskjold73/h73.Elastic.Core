using Newtonsoft.Json;

namespace h73.Elastic.Core.Server
{
    public class Settings
    {
        public Settings(int shards = 3, int replicas = 2)
        {
            Index = new Index(shards, replicas);
        }

        [JsonProperty("index")]
        public Index Index { get; set; }
    }
}
