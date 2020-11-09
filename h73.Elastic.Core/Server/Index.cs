using Newtonsoft.Json;

namespace h73.Elastic.Core.Server
{
    public class Index
    {
        public Index(int shards = 3, int replicas = 2)
        {
            NumberOfShards = shards;
            NumberOfReplicas = replicas;
        }

        [JsonProperty("number_of_shards")]
        public int NumberOfShards { get; set; }

        [JsonProperty("number_of_replicas")]
        public int NumberOfReplicas { get; set; }
    }
}
