using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Actions
{
    /// <summary>
    /// Indexing action
    /// </summary>
    /// <seealso cref="h73.Elastic.Core.Search.Interfaces.IAction" />
    public class IndexAction : IAction
    {
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
    }
}
