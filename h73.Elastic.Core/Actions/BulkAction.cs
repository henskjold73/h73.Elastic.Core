using System;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Actions
{
    /// <inheritdoc />
    /// <summary>
    /// Action for bulk indexing
    /// </summary>
    /// <seealso cref="T:h73.Elastic.Core.Search.Interfaces.IAction" />
    public class BulkAction : IAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BulkAction"/> class.
        /// </summary>
        public BulkAction()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkAction"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <param name="type">The type.</param>
        public BulkAction(string id, string index, string type)
        {
            Index = new IndexAction
            {
                Index = index,
                Id = id,
                Type = type
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkAction"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <param name="type">The type.</param>
        public BulkAction(string id, string index, Type type)
        {
            Index = new IndexAction
            {
                Index = index,
                Id = id,
                Type = type.FullName
            };
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public IndexAction Index { get; set; }
    }
}
