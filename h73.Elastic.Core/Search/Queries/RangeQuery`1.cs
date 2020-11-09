using System;
using h73.Elastic.Core.Enums;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Matches documents with fields that have terms within a certain range.
    /// The type of the Lucene query depends on the field type, for string fields, the TermRangeQuery, while for number/date fields, the query is a NumericRangeQuery.
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    [Serializable]
    public class RangeQuery<T> : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeQuery{T}"/> class.
        /// </summary>
        public RangeQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeQuery{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="field">The field.</param>
        /// <param name="type">The type.</param>
        /// <param name="boost">Boost</param>
        public RangeQuery(T value, string field, RangeQueryType type, double? boost = null)
        {
            switch (type)
            {
                case RangeQueryType.GreaterThan:
                    Range = new RangeQueryItem<T>
                    {
                        [field] = new RangeQueryValue<T> { GreaterThan = value, Boost = boost }
                    };
                    break;
                case RangeQueryType.LesserThan:
                    Range = new RangeQueryItem<T>
                    {
                        [field] = new RangeQueryValue<T> { LesserThan = value, Boost = boost  }
                    };
                    break;
                case RangeQueryType.GreaterThanEqual:
                    Range = new RangeQueryItem<T>
                    {
                        [field] = new RangeQueryValue<T> { GreaterThanOrEqual = value, Boost = boost  }
                    };
                    break;
                case RangeQueryType.LesserThanEqual:
                    Range = new RangeQueryItem<T>
                    {
                        [field] = new RangeQueryValue<T> { LesserThanOrEqual = value, Boost = boost }
                    };
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeQuery{T}"/> class.
        /// </summary>
        /// <param name="greaterThan">The greater than.</param>
        /// <param name="lesserThan">The lesser than.</param>
        /// <param name="field">The field.</param>
        /// <param name="type">The type.</param>
        public RangeQuery(T greaterThan, T lesserThan, string field, RangeQueryType type, double? boost = null)
        {
            switch (type)
            {
                case RangeQueryType.GreaterLesserThan:
                    Range = new RangeQueryItem<T>
                    {
                        [field] = new RangeQueryValue<T> { GreaterThan = greaterThan, LesserThan = lesserThan, Boost = boost }
                    };
                    break;
                case RangeQueryType.GreaterLesserThanEqual:
                    Range = new RangeQueryItem<T>
                    {
                        [field] = new RangeQueryValue<T> { GreaterThanOrEqual = greaterThan, LesserThanOrEqual = lesserThan, Boost = boost }
                    };
                    break;
            }
        }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        [JsonProperty("range")]
        public RangeQueryItem<T> Range { get; set; }
    }
}
