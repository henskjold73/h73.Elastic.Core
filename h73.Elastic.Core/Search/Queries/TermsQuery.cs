using System;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Filters documents that have fields that match any of the provided terms (not analyzed).
    /// </summary>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    [Serializable]
    public class TermsQuery : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TermsQuery"/> class.
        /// </summary>
        public TermsQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TermsQuery"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public TermsQuery(string field, string[] value)
        {
            Terms = new TermsQueryValue { [field] = value };
        }

        /// <summary>
        /// Gets or sets the terms.
        /// </summary>
        /// <value>
        /// The terms.
        /// </value>
        [JsonProperty("terms")]
        public TermsQueryValue Terms { get; set; }
    }
}
