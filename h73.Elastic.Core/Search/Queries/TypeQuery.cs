using System;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Filters documents matching the provided document / mapping type
    /// </summary>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    [Serializable]
    public class TypeQuery : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeQuery"/> class.
        /// </summary>
        public TypeQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeQuery"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public TypeQuery(string type)
        {
            Type = new TypeQueryValue { Value = type };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeQuery"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public TypeQuery(Type type)
        {
            Type = new TypeQueryValue { Value = type.FullName };
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        public TypeQueryValue Type { get; set; }
    }
}
