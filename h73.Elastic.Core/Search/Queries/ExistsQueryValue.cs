using System;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Returns documents that have at least one non-null value in the original field
    /// </summary>
    [Serializable]
    public class ExistsQueryValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExistsQueryValue"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        public ExistsQueryValue(string fieldName)
        {
            Field = fieldName;
        }

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>
        /// The field.
        /// </value>
        [JsonProperty("field")]
        public string Field { get; set; }
    }
}
