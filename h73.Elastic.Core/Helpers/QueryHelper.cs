using h73.Elastic.Core.Search.Queries;

namespace h73.Elastic.Core.Helpers
{
    /// <summary>
    /// Helpers for the Query class
    /// </summary>
    public static class QueryHelper
    {
        /// <summary>
        /// The most simple query, which matches all documents, giving them all a _score of 1.0.
        /// </summary>
        /// <returns>MatchAllQuery</returns>
        public static MatchAllQuery MatchAll()
        {
            return new MatchAllQuery();
        }
    }
}
