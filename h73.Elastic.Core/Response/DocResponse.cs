using Newtonsoft.Json;

namespace h73.Elastic.Core.Response
{

    /// <summary>
    /// DocResponse
    /// </summary>
    public class DocResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DocResponse"/> is found.
        /// </summary>
        /// <value>
        ///   <c>true</c> if found; otherwise, <c>false</c>.
        /// </value>
        public bool Found { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("_index")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("_type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [JsonProperty("_version")]
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the shards.
        /// </summary>
        /// <value>
        /// The shards.
        /// </value>
        [JsonProperty("_shards")]
        public Shards Shards { get; set; }
    }
}
