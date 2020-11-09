using System;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Returns documents that have at least one non-null value in the original field
    /// </summary>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    [Serializable]
    public class ExistsQuery : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExistsQuery"/> class.
        /// </summary>
        public ExistsQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExistsQuery"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        public ExistsQuery(string fieldName)
        {
            Exists = new ExistsQueryValue(fieldName);
        }

        /// <summary>
        /// Gets or sets the exists.
        /// </summary>
        /// <value>
        /// The exists.
        /// </value>
        [JsonProperty("exists")]
        public ExistsQueryValue Exists { get; set; }
    }
}
