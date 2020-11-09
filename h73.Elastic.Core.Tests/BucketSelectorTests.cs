using System.Collections.Generic;
using h73.Elastic.Core.Enums;
using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search.Aggregations;
using h73.Elastic.Core.Search.Interfaces;
using h73.Elastic.Core.Search.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class BucketSelectorTests
    {
        [TestMethod]
        public void BucketSelector_DateHistogram()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["nested"] = new NestedAggregation("Events")
                {
                    Aggregations =  new Dictionary<string, IAggregation>
                    {
                        ["nested"] = new DateHistogramAggregation("Events.Start", "week", order: new AggsOrder(AggsOrderBy.Key, AggsOrderDirection.Desc))
                        {
                            Aggregations =  new Dictionary<string, IAggregation>
                            {
                                ["nested"] = new TermsAggregation("_id", order: new AggsOrder("sum", AggsOrderDirection.Desc))
                                {
                                    Aggregations = new Dictionary<string, IAggregation>
                                    {
                                        ["sum"] = new SumAggregation("Events.Duration"),
                                        ["above25percent_filter"] = new BucketSelectorAggregation
                                        {
                                            BucketSelector = new BucketSelector
                                            {
                                                BucketsPath = new Dictionary<string, string>{["sum"] = "sum"},
                                                Script = "params.sum > 2520"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };



            var sSettings = JsonHelpers.CreateSerializerSettings();
            var json = JsonConvert.SerializeObject(new{size = 0, aggs}, sSettings);
            Assert.AreEqual("{\"size\":0,\"aggs\":{\"nested\":{\"nested\":{\"path\":\"Events\"},\"aggs\":{\"nested\":{\"date_histogram\":" +
                            "{\"interval\":\"week\",\"field\":\"Events.Start\",\"order\":{\"_key\":\"desc\"}},\"aggs\":{\"nested\":{\"terms\":" +
                            "{\"field\":\"_id\",\"order\":{\"sum\":\"desc\"}},\"aggs\":{\"sum\":{\"sum\":{\"field\":\"Events.Duration\"}}," +
                            "\"above25percent_filter\":{\"bucket_selector\":{\"buckets_path\":{\"sum\":\"sum\"},\"script\":\"params.sum > 2520\"}}}}}}}}}}", json);
        }

        [TestMethod]
        public void BucketSelector_DateHistogram_MinCount()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["nested"] = new NestedAggregation("Events")
                {
                    Aggregations =  new Dictionary<string, IAggregation>
                    {
                        ["nested"] = new DateHistogramAggregation("Events.Start", "week", 1, order: new AggsOrder(AggsOrderBy.Key, AggsOrderDirection.Desc))
                        {
                            Aggregations =  new Dictionary<string, IAggregation>
                            {
                                ["nested"] = new TermsAggregation("_id", order: new AggsOrder("sum", AggsOrderDirection.Desc))
                                {
                                    Aggregations = new Dictionary<string, IAggregation>
                                    {
                                        ["sum"] = new SumAggregation("Events.Duration"),
                                        ["above25percent_filter"] = new BucketSelectorAggregation
                                        {
                                            BucketSelector = new BucketSelector
                                            {
                                                BucketsPath = new Dictionary<string, string>{["sum"] = "sum"},
                                                Script = "params.sum > 2520"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            var sSettings = JsonHelpers.CreateSerializerSettings();
            var json = JsonConvert.SerializeObject(new{size = 0, aggs}, sSettings);
            Assert.AreEqual("{\"size\":0,\"aggs\":{\"nested\":{\"nested\":{\"path\":\"Events\"},\"aggs\":{\"nested\":{\"date_histogram\":" +
                            "{\"interval\":\"week\",\"min_doc_count\":1,\"field\":\"Events.Start\",\"order\":{\"_key\":\"desc\"}},\"aggs\":{\"nested\":{\"terms\":" +
                            "{\"field\":\"_id\",\"order\":{\"sum\":\"desc\"}},\"aggs\":{\"sum\":{\"sum\":{\"field\":\"Events.Duration\"}}," +
                            "\"above25percent_filter\":{\"bucket_selector\":{\"buckets_path\":{\"sum\":\"sum\"},\"script\":\"params.sum > 2520\"}}}}}}}}}}", json);
        }

        [TestMethod]
        public void BucketSelector_DateHistogram_Result()
        {
            var json = "{\"took\":11,\"timed_out\":false,\"_shards\":{\"total\":3,\"successful\":3,\"skipped\":0,\"failed\":0},\"hits\":" +
                       "{\"total\":4253,\"max_score\":0,\"hits\":[]},\"aggregations\":{\"nested\":{\"doc_count\":11351,\"nested\":" +
                       "{\"buckets\":[{\"key_as_string\":\"2018-10-01T00:00:00.000Z\",\"key\":1538352000000,\"doc_count\":87,\"nested\":" +
                       "{\"doc_count_error_upper_bound\":-1,\"sum_other_doc_count\":72,\"buckets\":[{\"key\":\"c7e9b694-e775-45e7-bb34-187bb2f267f1\"," +
                       "\"doc_count\":2,\"sum\":{\"value\":3154}}]}}]}}}}";

            var result = JsonConvert.DeserializeObject<SearchResult<object>>(json);
            Assert.AreEqual(3154, result.Aggregations["nested"].Nested.Buckets[0].Nested.Buckets[0].Sum.Value);
        }
    }
}