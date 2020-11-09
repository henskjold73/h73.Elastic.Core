using System;
using System.Linq;
using h73.Elastic.Core.Response;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search.Results
{    

    public class SearchResult<T>
    {
        [JsonIgnore]
        public string _Name { get; set; }

        [JsonProperty("took")]
        public int Took { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        [JsonProperty("_shards")]
        public Shards Shards { get; set; }

        [JsonProperty("hits")]
        public Hits<T> Hits { get; set; }

        [JsonProperty("aggregations")]
        public Aggregations Aggregations { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("_scroll_id")]
        public string ScrollId { get; set; }
        
        public SearchResult<T3> SelectResult<T3>(Func<T, T3> func)
            where T3 : class
        {
            var output = new SearchResult<T3>
            {
                Aggregations = this.Aggregations,
                Shards = this.Shards,
                TimedOut = this.TimedOut,
                Count = this.Count,
                Took = this.Took,
                Hits = new Hits<T3>
                {
                    Total = this.Hits.Total,
                    MaxScore = this.Hits.MaxScore
                }
            };

            var hits = this.Hits.HitsList.Select(x => new Hit<T3>
            {
                Type = x.Type,
                Id = x.Id,
                Index = x.Index,
                Score = x.Score,
                Source = func.Invoke(x.Source)
            });

            output.Hits.HitsList = hits.ToArray();

            return output;
        }
    }
}
