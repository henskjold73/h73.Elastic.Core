using System;
using System.Linq.Expressions;
using h73.Elastic.Core.Helpers;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// The term query finds documents that contain the exact term specified in the inverted index.
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    [Serializable]
    public class TermQuery<T> : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TermQuery{T}"/> class.
        /// </summary>
        public TermQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TermQuery{T}"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public TermQuery(string field, string value)
        {
            Term = new TermQueryValue { [field] = value };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TermQuery{T}"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fieldExpression">The field expression.</param>
        public TermQuery(string text, Expression<Func<T, object>> fieldExpression)
        {
            var fieldName = ExpressionHelper.GetPropertyName(fieldExpression);
            Term = new TermQueryValue { [fieldName] = $"{text}" };
        }

        /// <summary>
        /// Gets or sets the term.
        /// </summary>
        /// <value>
        /// The term.
        /// </value>
        [JsonProperty("term")]
        public TermQueryValue Term { get; set; }
    }
}
