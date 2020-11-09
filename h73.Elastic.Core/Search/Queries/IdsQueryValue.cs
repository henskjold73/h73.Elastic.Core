using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Filters documents that only have the provided ids. Note, this query uses the _uid field.
    /// </summary>
    public class IdsQueryValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdsQueryValue"/> class.
        /// </summary>
        /// <param name="ids">The ids.</param>
        public IdsQueryValue(string[] ids)
        {
            Value = ids;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("values")]
        public string[] Value { get; set; }
    }
}
