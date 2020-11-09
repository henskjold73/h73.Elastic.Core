using System;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// The most simple query, which matches all documents, giving them all a _score of 1.0
    /// </summary>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    [Serializable]
    public class MatchAllQuery : QueryInit
    {

        /// <summary>
        /// Gets the match all.
        /// </summary>
        /// <value>
        /// The match all.
        /// </value>
        [JsonProperty("match_all")]
        public object MatchAll => new { };
    }
}
