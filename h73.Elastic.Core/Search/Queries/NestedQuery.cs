using System;
using System.Collections.Generic;
using h73.Elastic.Core.Enums;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    [Serializable]
    public class NestedQuery : NestedQuery<object>
    {
    }

    public class NestedQuery<T> : QueryInit where T : class
    {
        [JsonProperty(PropertyName = "nested", NullValueHandling = NullValueHandling.Ignore)]
        public NestedItem<T> Nested { get; set; }
    }

    public class NestedItem : NestedItem<object>
    {
    }

    public class NestedItem<T> where T : class
    {
        [JsonProperty(PropertyName = "path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "score_mode", NullValueHandling = NullValueHandling.Ignore)]
        public ScoreMode? ScoreMode { get; set; }

        [JsonProperty(PropertyName = "query", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, IQuery> Query { get; set; }

        [JsonProperty(PropertyName = "inner_hits", NullValueHandling = NullValueHandling.Ignore)]
        public InnerHits InnerHits { get; set; }

    }
}