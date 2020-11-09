using h73.Elastic.Core.Search.Interfaces;
using h73.Elastic.Core.Search.Queries;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Aggregations
{
    /// <summary>
    /// The geo_bounding_box filter has been replaced by the Geo Bounding Box Query. It behaves as a query in “query context” and as a filter in “filter context” (see Query DSL).
    /// </summary>
    /// <seealso cref="h73.Elastic.Core.Search.Queries.QueryInit" />
    /// <seealso cref="h73.Elastic.Core.Search.Interfaces.IFilter" />
    public class GeoBoundingBoxFilter : QueryInit, IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoBoundingBoxFilter"/> class.
        /// </summary>
        public GeoBoundingBoxFilter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoBoundingBoxFilter"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="topLeft">The top left.</param>
        /// <param name="bottomRight">The bottom right.</param>
        public GeoBoundingBoxFilter(string fieldName, string topLeft, string bottomRight)
        {
            GeoBoundingBox = new GeoBoundingBoxFilterItem
            {
                [fieldName] = new GeoBox(topLeft, bottomRight)
            };
        }

        /// <summary>
        /// Gets or sets the geo bounding box.
        /// </summary>
        /// <value>
        /// The geo bounding box.
        /// </value>
        [JsonProperty("geo_bounding_box")]
        public GeoBoundingBoxFilterItem GeoBoundingBox { get; set; }
    }
}
