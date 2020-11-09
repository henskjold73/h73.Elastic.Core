using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Filters documents that only have the provided ids. Note, this query uses the _uid field.
    /// </summary>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    /// [Serializable]
    public class IdsQuery : QueryInit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdsQuery"/> class.
        /// </summary>
        public IdsQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdsQuery"/> class.
        /// </summary>
        /// <param name="ids">The ids.</param>
        public IdsQuery(string[] ids)
        {
            Ids = new IdsQueryValue(ids);
        }

        /// <summary>
        /// Gets or sets the ids.
        /// </summary>
        /// <value>
        /// The ids.
        /// </value>
        [JsonProperty("ids")]
        public IdsQueryValue Ids { get; set; }
    }
}
