using Newtonsoft.Json;

namespace h73.Elastic.Core.Server
{
    public class ServerInfo
    {
        public ServerInfo()
        {
        }

        public string Name { get; set; }

        [JsonProperty("cluster_name")]
        public string ClusterName { get; set; }

        [JsonProperty("cluster_uuid")]
        public string ClusterUuid { get; set; }

        public Version Version { get; set; }

        public string Tagline { get; set; }

        public override string ToString()
        {
            return $"{ClusterName} {Name} {Version}";
        }
    }
}
