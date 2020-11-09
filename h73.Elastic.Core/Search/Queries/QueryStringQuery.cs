using System;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// A query that uses a query parser in order to parse its content.
    /// </summary>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    [Serializable]
    public class QueryStringQuery : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringQuery"/> class.
        /// </summary>
        public QueryStringQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringQuery"/> class.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        public QueryStringQuery(string queryString)
        {
            QueryString = new QueryString { Query = queryString };
        }

        /// <summary>
        /// Gets or sets the query string.
        /// </summary>
        /// <value>
        /// The query string.
        /// </value>
        [JsonProperty("query_string")]
        public QueryString QueryString { get; set; }
    }
}
