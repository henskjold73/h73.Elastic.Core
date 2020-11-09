using System;
using System.Linq.Expressions;
using h73.Elastic.Core.Helpers;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    [Serializable]
    public class CommonQuery<T> : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonQuery{T}"/> class.
        /// </summary>
        public CommonQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonQuery{T}"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public CommonQuery(string field, string value)
        {
            Match = new CommonQueryValue { [field] = $"{value}" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonQuery{T}"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fieldExpression">The field expression.</param>
        public CommonQuery(string text, Expression<Func<T, object>> fieldExpression)
        {
            var fieldName = ExpressionHelper.GetPropertyName(fieldExpression);
            Match = new CommonQueryValue { [fieldName] = $"{text}" };
        }

        /// <summary>
        /// Gets or sets the match.
        /// </summary>
        /// <value>
        /// The match.
        /// </value>
        [JsonProperty("wildcard")]
        public CommonQueryValue Match { get; set; }
    }
}
