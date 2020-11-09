using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// The query_string query parses the input and splits text around operators. Each textual part is analyzed independently of each other.
    /// </summary>
    public class QueryString
    {
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}
