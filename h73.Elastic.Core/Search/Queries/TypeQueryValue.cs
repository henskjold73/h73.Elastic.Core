using System;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Filters documents matching the provided document / mapping type
    /// </summary>
    [Serializable]
    public class TypeQueryValue
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
