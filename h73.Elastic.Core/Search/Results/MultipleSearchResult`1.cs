using System.Collections.Generic;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Results
{
    public class MultipleSearchResult<T>
        where T : class
    {
        [JsonProperty("responses")]
        public List<SearchResult<T>> Resonses { get; set; }
    }
}
