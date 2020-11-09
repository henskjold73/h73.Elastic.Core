using System;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Base class for queries
    /// </summary>
    [Serializable]
    public abstract class QueryInit : IQuery, IFilter
    {
        /// <summary>
        /// Gets or sets query name for matching queries after they are run
        /// </summary>
        [JsonIgnore]
        public string _Name { get; set; }
    }
}
