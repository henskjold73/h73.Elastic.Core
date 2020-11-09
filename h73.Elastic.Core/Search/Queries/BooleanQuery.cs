using System;
using System.Collections.Generic;
using System.Linq;
using h73.Elastic.Core.Enums;
using h73.Elastic.Core.Search.Interfaces;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Queries
{
    [Serializable]
    public class BooleanQuery : QueryInit
    {
        public BooleanQuery()
        {
            Must = new List<IQuery>();
            MustNot = new List<IQuery>();
            Should = new List<IQuery>();
            MinimumShouldMatch = 1;
        }

        [JsonProperty("must")]
        public List<IQuery> Must { get; set; }

        [JsonProperty("must_not")]
        public List<IQuery> MustNot { get; set; }

        [JsonProperty("should")]
        public List<IQuery> Should { get; set; }

        [JsonProperty("minimum_should_match")]
        public int MinimumShouldMatch { get; set; }

        [JsonProperty("filter", NullValueHandling = NullValueHandling.Ignore)]
        public List<IQuery> Filter { get; set; }

        public void Add(IQuery q, BooleanQueryType type)
        {
            switch (type)
            {
                case BooleanQueryType.Must:
                    Must.Add(q);
                    break;
                case BooleanQueryType.MustNot:
                    MustNot.Add(q);
                    break;
                case BooleanQueryType.Should:
                    Should.Add(q);
                    break;
            }
        }

        public void AddRange(IEnumerable<IQuery> q, BooleanQueryType type)
        {
            switch (type)
            {
                case BooleanQueryType.Must:
                    Must.AddRange(q);
                    break;
                case BooleanQueryType.MustNot:
                    MustNot.AddRange(q);
                    break;
                case BooleanQueryType.Should:
                    Should.AddRange(q);
                    break;
            }
        }

        public bool ShouldSerializeMust()
        {
            return Must != null && Must.Any();
        }

        public bool ShouldSerializeMustNot()
        {
            return MustNot != null && MustNot.Any();
        }

        public bool ShouldSerializeShould()
        {
            return Should != null && Should.Any();
        }

        public bool ShouldSerializeMinimumShouldMatch()
        {
            return Should != null && Should.Any();
        }
    }
}
