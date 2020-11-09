using System.Linq;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search.Results;
using h73.Elastic.Core.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class SearchResultTests
    {
        private JsonSerializerSettings _j;

        [TestInitialize]
        public void Setup()
        {
            _j = JsonHelpers.CreateSerializerSettings();
        }

        [TestMethod]
        public void SearchResult_Json1()
        {
            var json =
                "{\"took\":135,\"timed_out\":false,\"_shards\":" +
                "{\"total\":3,\"successful\":3,\"skipped\":0,\"failed\":0},\"hits\":" +
                "{\"total\":36452,\"max_score\":0,\"hits\":[]},\"aggregations\":" +
                "{\"terms_Guid\":{\"doc_count_error_upper_bound\":0,\"sum_other_doc_count\":0,\"buckets\":" +
                "[{\"key\":\"6da8ae20-9ade-4cf4-812a-5d60a7e2c903\",\"doc_count\":31,\"Avg\":{\"value\":0.3353280112628014},\"TopHits\":" +
                "{\"hits\":{\"total\":31,\"max_score\":null,\"hits\":[{\"_index\":\"h73dev_h73_transformermetrics_transformerstatistics\"," +
                "\"_type\":\"h73.Elastic.Core.Tests.Support.IndexedClass\",\"_id\":\"112366\",\"_score\":null,\"_source\":{\"AString\":" +
                "\"Aggregated\",\"SomeNumber\":112366,\"ADate\":\"2018-09-20T22:00:00\",\"BString\":\"V023\"},\"sort\":[1537480800000]}]}},\"Max\":" +
                "{\"value\":2.2597575187683105}}]}}}";

            var result =
                JsonConvert.DeserializeObject<SearchResult<IndexedClass>>(json, JsonHelpers.CreateSerializerSettings());


            Assert.AreEqual(1, result.Aggregations.Count);
            Assert.AreEqual(1, result.Aggregations.First().Value.Buckets.Count());
            Assert.AreEqual(31,
                ((TopHitsResult<IndexedClass>) result.Aggregations.First().Value.Buckets[0].TopHits).Hits.Total);
            Assert.AreEqual("V023",
                ((TopHitsResult<IndexedClass>) result.Aggregations.First().Value.Buckets[0].TopHits).Hits.HitsList
                .First().Source.BString);
        }

        [TestMethod]
        public void SearchResult_TopHitsResult()
        {
            var topHitsResult = new TopHitsResult
            {
                Hits = new Hits<object>
                {
                    Total = 2545,
                    HitsList = new[]
                    {
                        new Hit<object> {Source = new {F = "f"}, Id = "f"},
                        new Hit<object> {Source = new {F = "f"}, Id = "f"},
                        new Hit<object> {Source = new {F = "f"}, Id = "f"},
                    }
                }
            };
            var r = JsonConvert.SerializeObject(topHitsResult.Hits, _j);

            Assert.AreEqual(r, "{\"total\":2545,\"hits\":[{\"_id\":\"f\",\"_source\":{\"F\":\"f\"}},{\"_id\":\"f\",\"_source\":{\"F\":\"f\"}},{\"_id\":\"f\",\"_source\":{\"F\":\"f\"}}]}");
        }
    }
}