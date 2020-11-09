using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search.Results;
using h73.Elastic.Core.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class AggregationResultTests
    {
        private JsonSerializerSettings _j;

        [TestInitialize]
        public void Setup()
        {
            _j = JsonHelpers.CreateSerializerSettings();
        }

        [TestMethod]
        public void NestedAggregationResult1_JsonResult_ToObject()
        {
            var jsonResult =
                "{\"_shards\":{\"failed\":1,\"skipped\":2,\"successful\":5,\"total\":8},\"aggregations\":{\"nested_TimeSeriesValues\":{\"doc_count\":4291700,\"nested\":{\"buckets\":[{\"doc_count\":42917,\"key\":1535925600000,\"key_as_string\":\"2018-09-02T22:00:00.000Z\",\"sum\":{\"value\":72661},\"nested\":{\"buckets\":[{\"doc_count\":10070,\"key\":\"Missing\",\"to\":1},{\"doc_count\":32847,\"from\":1,\"key\":\"Some\",\"to\":24},{\"doc_count\":0,\"from\":24,\"key\":\"All\"}]}},{\"doc_count\":42917,\"key\":1535839200000,\"key_as_string\":\"2018-09-01T22:00:00.000Z\",\"nested\":{\"buckets\":[{\"doc_count\":2886,\"key\":\"Missing\",\"to\":1},{\"doc_count\":7186,\"from\":1,\"key\":\"Some\",\"to\":24},{\"doc_count\":32845,\"from\":24,\"key\":\"All\"}]}},{\"doc_count\":42917,\"key\":1535752800000,\"key_as_string\":\"2018-08-31T22:00:00.000Z\",\"nested\":{\"buckets\":[{\"doc_count\":2021,\"key\":\"Missing\",\"to\":1},{\"doc_count\":866,\"from\":1,\"key\":\"Some\",\"to\":24},{\"doc_count\":40030,\"from\":24,\"key\":\"All\"}]}},{\"doc_count\":42917,\"key\":1535666400000,\"key_as_string\":\"2018-08-30T22:00:00.000Z\",\"nested\":{\"buckets\":[{\"doc_count\":1801,\"key\":\"Missing\",\"to\":1},{\"doc_count\":228,\"from\":1,\"key\":\"Some\",\"to\":24},{\"doc_count\":40888,\"from\":24,\"key\":\"All\"}]}},{\"doc_count\":42917,\"key\":1535580000000,\"key_as_string\":\"2018-08-29T22:00:00.000Z\",\"nested\":{\"buckets\":[{\"doc_count\":1766,\"key\":\"Missing\",\"to\":1},{\"doc_count\":346,\"from\":1,\"key\":\"Some\",\"to\":24},{\"doc_count\":40805,\"from\":24,\"key\":\"All\"}]}},{\"doc_count\":42917,\"key\":1535493600000,\"key_as_string\":\"2018-08-28T22:00:00.000Z\",\"nested\":{\"buckets\":[{\"doc_count\":1512,\"key\":\"Missing\",\"to\":1},{\"doc_count\":571,\"from\":1,\"key\":\"Some\",\"to\":24},{\"doc_count\":40834,\"from\":24,\"key\":\"All\"}]}},{\"doc_count\":42917,\"key\":1535407200000,\"key_as_string\":\"2018-08-27T22:00:00.000Z\",\"nested\":{\"buckets\":[{\"doc_count\":1502,\"key\":\"Missing\",\"to\":1},{\"doc_count\":276,\"from\":1,\"key\":\"Some\",\"to\":24},{\"doc_count\":41139,\"from\":24,\"key\":\"All\"}]}}],\"doc_count_error_upper_bound\":0,\"sum_other_doc_count\":3991281}}},\"hits\":{\"hits\":[],\"max_score\":0,\"total\":42917},\"timed_out\":false,\"took\":0}";

            var obj = JsonConvert.DeserializeObject<SearchResult<dynamic>>(jsonResult);


            Assert.AreEqual(1, obj.Shards.Failed);
            Assert.AreEqual(2, obj.Shards.Skipped);
            Assert.AreEqual(5, obj.Shards.Successful);
            Assert.AreEqual(8, obj.Shards.Total);

            Assert.AreEqual(4291700, obj.Aggregations["nested_TimeSeriesValues"].DocCount);
            Assert.AreEqual(7, obj.Aggregations["nested_TimeSeriesValues"].Nested.Buckets.Length);
            Assert.AreEqual(42917, obj.Aggregations["nested_TimeSeriesValues"].Nested.Buckets[0].DocCount);
            Assert.AreEqual(72661, obj.Aggregations["nested_TimeSeriesValues"].Nested.Buckets[0].Sum.Value);
        }

        [TestMethod]
        public void NestedAggregationResult2_JsonResult_ToObject()
        {
            var jsonData =
                "{\"took\":32,\"timed_out\":false,\"_shards\":{\"total\":3,\"successful\":3,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":841,\"max_score\":0,\"hits\":[]}" +
                ",\"aggregations\":{\"nested_Children_0\":{\"doc_count\":33549,\"nested\":{\"buckets\":[{\"key_as_string\":\"2018-10-22T00:00:00.000Z\",\"key\":1540166400000," +
                "\"doc_count\":38,\"nested\":{\"doc_count_error_upper_bound\":0,\"sum_other_doc_count\":26,\"buckets\":[]}},{\"key_as_string\":\"2018-10-15T00:00:00.000Z\"," +
                "\"key\":1539561600000,\"doc_count\":149,\"nested\":{\"doc_count_error_upper_bound\":-1,\"sum_other_doc_count\":129,\"buckets\":[{\"key\":\"505ad21c-9cb7-4c17-99a8-a193f248986c\"," +
                "\"doc_count\":2,\"sum\":{\"value\":3798}},{\"key\":\"cc3dfb17-b653-4627-b8c1-98afa752ae92\",\"doc_count\":2,\"sum\":{\"value\":3044}},{\"key\":\"63475d9a-3b48-4196-a287-1d1788b311ad\"," +
                "\"doc_count\":2,\"sum\":{\"value\":2735}},{\"key\":\"35bea27a-f44c-4c43-86f4-ce1a97104d1d\",\"doc_count\":2,\"sum\":{\"value\":2676}}]}}]}}}}";

            var searchResult = JsonConvert.DeserializeObject<SearchResult<IndexedClass>>(jsonData, JsonHelpers.CreateSerializerSettings());
        }

        [TestMethod]
        public void FilteredNestedAggregationResult_JsonResult_ToObject()
        {
            var json = "{\"took\":1,\"timed_out\":false,\"_shards\":{\"total\":3,\"successful\":3,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":841,\"max_score\":0,\"hits\":[]},\"aggregations\":" +
                       "{\"agg_filter\":{\"doc_count\":3414,\"nested_Events_0\":{\"doc_count\":42,\"nested\":{\"buckets\":[{\"key_as_string\":\"2018-08-27T00:00:00.000Z\",\"key\":1535328000000,\"doc_count\":1," +
                       "\"nested\":{\"doc_count_error_upper_bound\":0,\"sum_other_doc_count\":0,\"buckets\":[]}},{\"key_as_string\":\"2018-08-20T00:00:00.000Z\",\"key\":1534723200000,\"doc_count\":0,\"nested\":{" +
                       "\"doc_count_error_upper_bound\":0,\"sum_other_doc_count\":0,\"buckets\":[]}}]}}}}}";

            var searchResult = JsonConvert.DeserializeObject<SearchResult<IndexedClass>>(json, JsonHelpers.CreateSerializerSettings());
            Assert.AreEqual(42, searchResult.Aggregations["nested_Events_0"].DocCount);
        }

        [TestMethod]
        public void Aggregation_DocCountErrorUpperBound()
        {
            var aggr = new Aggregation{DocCountErrorUpperBound = 5, SumOtherDocCount = 5};
            
            Assert.AreEqual(5, aggr.DocCountErrorUpperBound);
            Assert.AreEqual(5, aggr.SumOtherDocCount);
        }

    }
}