using System.Collections.Generic;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    public class BooleanQueryRoot : Dictionary<string, IQuery>, IQuery, IFilter
    {
        [JsonIgnore]
        public string _Name { get; set; }
    }
}
