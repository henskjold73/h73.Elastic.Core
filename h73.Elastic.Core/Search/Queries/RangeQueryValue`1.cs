using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// RangeQueryValue to support RangeQueryItem
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    public class RangeQueryValue<T> : IBoostable
    {
        /// <summary>
        /// Gets or sets the greater than.
        /// </summary>
        /// <value>
        /// The greater than.
        /// </value>
        [JsonProperty("gt")]
        public T GreaterThan { get; set; }

        /// <summary>
        /// Gets or sets the greater than or equal.
        /// </summary>
        /// <value>
        /// The greater than or equal.
        /// </value>
        [JsonProperty("gte")]
        public T GreaterThanOrEqual { get; set; }

        /// <summary>
        /// Gets or sets the lesser than.
        /// </summary>
        /// <value>
        /// The lesser than.
        /// </value>
        [JsonProperty("lt")]
        public T LesserThan { get; set; }

        /// <summary>
        /// Gets or sets the lesser than or equal.
        /// </summary>
        /// <value>
        /// The lesser than or equal.
        /// </value>
        [JsonProperty("lte")]
        public T LesserThanOrEqual { get; set; }

        /// <summary>
        /// Shoulds serialize greater than.
        /// </summary>
        /// <returns>bool</returns>
        public bool ShouldSerializeGreaterThan()
        {
            return GreaterThan.IsNotNull();
        }

        /// <summary>
        /// Shoulds serialize greater than or equal.
        /// </summary>
        /// <returns>bool</returns>
        public bool ShouldSerializeGreaterThanOrEqual()
        {
            return GreaterThanOrEqual.IsNotNull();
        }

        /// <summary>
        /// Shoulds serialize lesser than.
        /// </summary>
        /// <returns>bool</returns>
        public bool ShouldSerializeLesserThan()
        {
            return LesserThan.IsNotNull();
        }

        /// <summary>
        /// Shoulds serialize lesser than or equal.
        /// </summary>
        /// <returns>bool</returns>
        public bool ShouldSerializeLesserThanOrEqual()
        {
            return LesserThanOrEqual.IsNotNull();
        }

        [JsonProperty("boost", NullValueHandling = NullValueHandling.Ignore)]
        public double? Boost { get; set; }
    }
}
