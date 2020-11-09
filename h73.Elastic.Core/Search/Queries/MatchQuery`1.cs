using System;
using System.Linq.Expressions;
using h73.Elastic.Core.Helpers;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// match queries accept text/numerics/dates, analyzes them, and constructs a query.
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    [Serializable]
    public class MatchQuery<T> : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchQuery{T}"/> class.
        /// </summary>
        public MatchQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchQuery{T}"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public MatchQuery(string field, object value)
        {
            Match = new MatchQueryValue { [field] = value };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchQuery{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fieldExpression">The field expression.</param>
        public MatchQuery(object value, Expression<Func<T, object>> fieldExpression)
        {
            var fieldName = ExpressionHelper.GetPropertyName(fieldExpression);
            Match = new MatchQueryValue { [fieldName] = value };
        }

        /// <summary>
        /// Gets or sets the match.
        /// </summary>
        /// <value>
        /// The match.
        /// </value>
        [JsonProperty("match")]
        public MatchQueryValue Match { get; set; }
    }
}
