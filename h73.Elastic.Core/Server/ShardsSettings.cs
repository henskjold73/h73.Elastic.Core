using Newtonsoft.Json;

namespace h73.Elastic.Core.Server
{
    public class ShardsSettings
    {
        public ShardsSettings(int shards = 3, int replicas = 2)
        {
            Settings = new Settings(shards, replicas);
        }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }
    }
}
