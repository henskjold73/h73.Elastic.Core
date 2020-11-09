using System;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Server
{
    public class Version
    {
        public string Number { get; set; }

        [JsonProperty("build_hash")]
        public string BuildHash { get; set; }

        [JsonProperty("build_date")]
        public DateTime BuildDate { get; set; }

        [JsonProperty("build_snapshot")]
        public bool BuildSnapshot { get; set; }

        [JsonProperty("lucene_version")]
        public string LuceneVersion { get; set; }

        public override string ToString()
        {
            return $"{Number}";
        }
    }
}
