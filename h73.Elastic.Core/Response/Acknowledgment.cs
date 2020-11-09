using Newtonsoft.Json;

namespace h73.Elastic.Core.Response
{
    /// <summary>
    /// Acknowledgment, answer from elasticsearch
    /// </summary>
    public class Acknowledgment
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Acknowledgment"/> is acknowledged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if acknowledged; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("acknowledged")]
        public bool Acknowledged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [shards acknowledged].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [shards acknowledged]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("shards_acknowledged")]
        public bool ShardsAcknowledged { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public string Index { get; set; }
    }
}
