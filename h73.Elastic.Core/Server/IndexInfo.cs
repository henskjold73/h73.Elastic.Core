using Newtonsoft.Json;

namespace h73.Elastic.Core.Server
{
    public class IndexInfo
    {
        [JsonProperty("health")]
        public string Health { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("uuid")]
        public string UuId { get; set; }

        [JsonProperty("pri")]
        public int Pri { get; set; }

        [JsonProperty("rep")]
        public int Rep { get; set; }

        [JsonProperty("doc.count")]
        public int DocsCount { get; set; }

        [JsonProperty("doc.deleted")]
        public int DocsDeleted { get; set; }

        [JsonProperty("store.size")]
        public string StoreSize { get; set; }

        [JsonProperty("pr.store.size")]
        public string PriStoreSize { get; set; }
    }
}
