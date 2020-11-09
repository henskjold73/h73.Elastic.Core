using Newtonsoft.Json;

namespace h73.Elastic.Core.Response
{
    /// <summary>
    /// Shards
    /// </summary>
    public class Shards
    {
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the successful.
        /// </summary>
        /// <value>
        /// The successful.
        /// </value>
        [JsonProperty("successful")]
        public int Successful { get; set; }

        /// <summary>
        /// Gets or sets the failed.
        /// </summary>
        /// <value>
        /// The failed.
        /// </value>
        [JsonProperty("failed")]
        public int Failed { get; set; }

        /// <summary>
        /// Gets or sets the skipped.
        /// </summary>
        /// <value>
        /// The skipped.
        /// </value>
        [JsonProperty("skipped")]
        public int Skipped { get; set; }
    }
}
