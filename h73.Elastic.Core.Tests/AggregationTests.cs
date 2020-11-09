using System;
using System.Collections.Generic;
using h73.Elastic.Core.Enums;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search;
using h73.Elastic.Core.Search.Aggregations;
using h73.Elastic.Core.Search.Interfaces;
using h73.Elastic.Core.Search.Queries;
using h73.Elastic.Core.Search.Results;
using h73.Elastic.Core.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class AggregationTests
    {
        private JsonSerializerSettings _json;

        [TestInitialize]
        public void Setup()
        {
            _json = JsonHelpers.CreateSerializerSettings();
        }

        [TestMethod]
        public void Terms_Field_Json()
        {
            var terms = new TermsAggregation("FieldName");
            var json = JsonConvert.SerializeObject(terms, _json);

            Assert.AreEqual("{\"terms\":{\"field\":\"FieldName\"}}", json);
        }

        [TestMethod]
        public void Terms_FieldSize_Json()
        {
            var terms = new TermsAggregation("FieldName", 1000);
            var json = JsonConvert.SerializeObject(terms, _json);

            Assert.AreEqual("{\"terms\":{\"field\":\"FieldName\",\"size\":1000}}", json);
        }

        [TestMethod]
        public void GeoCentroid_Field_Json()
        {
            var geo = new GeoCentroidAggregation("FieldName");
            var json = JsonConvert.SerializeObject(geo, _json);

            Assert.AreEqual("{\"geo_centroid\":{\"field\":\"FieldName\"}}", json);
        }

        [TestMethod]
        public void GeoHashGrid_Field_Json()
        {
            var geo = new GeoHashGridAggregation("FieldName", 5);
            var json = JsonConvert.SerializeObject(geo, _json);

            Assert.AreEqual("{\"geohash_grid\":{\"precision\":5,\"field\":\"FieldName\"}}", json);
        }

        [TestMethod]
        public void Script_lat_lng()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["center_lat"] = new ScriptAggregation {Avg = new ScriptAggr {Script = "doc['Point'].lat"}},
                ["center_lng"] = new ScriptAggregation {Avg = new ScriptAggr {Script = "doc['Point'].lon"}}
            };
            var json = JsonConvert.SerializeObject(aggs, _json);

            Assert.AreEqual(
                "{\"center_lat\":{\"avg\":{\"script\":\"doc[\'Point\'].lat\"}},\"center_lng\":{\"avg\":{\"script\":\"doc[\'Point\'].lon\"}}}",
                json);
        }


        [TestMethod]
        public void Terms_GeoCentroid_Nested_Json()
        {
            var terms = new TermsAggregation("Address.PostalCode", 1000);
            var geo = new GeoCentroidAggregation("Point");
            terms.Aggregations = new Dictionary<string, IAggregation>
            {
                ["centroid"] = geo
            };
            var json = JsonConvert.SerializeObject(terms, _json);

            Assert.AreEqual(
                "{\"terms\":{\"field\":\"Address.PostalCode\",\"size\":1000},\"aggs\":{\"centroid\":{\"geo_centroid\":{\"field\":\"Point\"}}}}",
                json);
        }

        [TestMethod]
        public void GeoBoundingBox()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["CurrentView"] = new AggregationBase
                {
                    Filter = new GeoBoundingBoxFilter("Point", "60.2822, 9.7400", "58.0863, 11.2055"),
                    Aggregations = new Dictionary<string, IAggregation>
                    {
                        ["GeoGrid"] = new GeoHashGridAggregation("Point", 6)
                        {
                            Aggregations = new Dictionary<string, IAggregation>
                            {
                                ["center_lat"] =
                                    new ScriptAggregation {Avg = new ScriptAggr {Script = "doc['Point'].lat"}},
                                ["center_lng"] =
                                    new ScriptAggregation {Avg = new ScriptAggr {Script = "doc['Point'].lon"}}
                            }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(aggs, _json);

            Assert.AreEqual(
                "{\"CurrentView\":{\"aggs\":{\"GeoGrid\":{\"geohash_grid\":{\"precision\":6,\"field\":\"Point\"},\"aggs\":{\"center_lat\":{\"avg\":{\"script\":\"doc[\'Point\'].lat\"}},\"center_lng\":{\"avg\":{\"script\":\"doc[\'Point\'].lon\"}}}}},\"filter\":{\"geo_bounding_box\":{\"Point\":{\"top_left\":\"60.2822, 9.7400\",\"bottom_right\":\"58.0863, 11.2055\"}}}}}",
                json);
        }

        [TestMethod]
        public void TermAggregation_Order()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["Unordered"] = new TermsAggregation("Values"),
                ["Ordered"] = new TermsAggregation("Values",
                    order: new AggsOrder(AggsOrderBy.Count, AggsOrderDirection.Asc))
            };
            var json = JsonConvert.SerializeObject(aggs, _json);
            Assert.AreEqual(
                "{\"Unordered\":{\"terms\":{\"field\":\"Values\"}},\"Ordered\":{\"terms\":{\"field\":\"Values\",\"order\":{\"_count\":\"asc\"}}}}",
                json);
        }

        [TestMethod]
        public void TermAggregation_Order2()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["Unordered"] = new TermsAggregation("Values"),
                ["Ordered"] = new TermsAggregation("Values", AggsOrderBy.Count, AggsOrderDirection.Asc)
            };
            var json = JsonConvert.SerializeObject(aggs, _json);
            Assert.AreEqual(
                "{\"Unordered\":{\"terms\":{\"field\":\"Values\"}},\"Ordered\":{\"terms\":{\"field\":\"Values\",\"order\":{\"_count\":\"asc\"}}}}",
                json);
        }

        [TestMethod]
        public void SumAggregation_Json()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["Sum"] = new SumAggregation("Field")
            };
            var json = JsonConvert.SerializeObject(aggs, _json);
            Assert.AreEqual("{\"Sum\":{\"sum\":{\"field\":\"Field\"}}}", json);
        }

        [TestMethod]
        public void HistogramAggregation_Json()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["prices"] = new HistogramAggregation<int>("Name", 50)
            };
            var json = JsonConvert.SerializeObject(aggs, _json);
            Assert.AreEqual("{\"prices\":{\"histogram\":{\"interval\":50,\"field\":\"Name\"}}}", json);
        }

        [TestMethod]
        public void CardinalityAggregation_Json()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["c"] = new CardinalityAggregation("Name")
            };
            var json = JsonConvert.SerializeObject(aggs, _json);
            Assert.AreEqual("{\"c\":{\"cardinality\":{\"field\":\"Name\"}}}", json);
        }

        [TestMethod]
        public void MaxAggregation_BucketSort_Json()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["terms_Guid"] = new TermsAggregation("Guid", int.MaxValue)
                {
                    Aggregations = new Dictionary<string, IAggregation>
                    {
                        ["max"] = new MaxAggregation("PeakLoadPercentage"),
                        ["BucketSort_Max"] = new BucketSortAggregation
                        {
                            BucketSort = new BucketSort
                            {
                                Sorts = new[]
                                {
                                    new Sort
                                    {
                                        ["max"] = "desc"
                                    }
                                },
                                Size = 100,
                                From = 500
                            }
                        }
                    }
                }
            };
            var json = JsonConvert.SerializeObject(aggs, _json);
            Assert.AreEqual(
                "{\"terms_Guid\":{\"terms\":{\"field\":\"Guid\",\"size\":2147483647},\"aggs\":" +
                "{\"max\":{\"max\":{\"field\":\"PeakLoadPercentage\"}},\"BucketSort_Max\":" +
                "{\"bucket_sort\":{\"sort\":[{\"max\":\"desc\"}],\"size\":100,\"from\":500}}}}}",
                json);
        }

        [TestMethod]
        public void BooleanQuery_AggsFilter_Json()
        {
            var qFilter = new BooleanQueryRoot
            {
                ["bool"] = new BooleanQuery
                {
                    Must = new List<IQuery> {new TermQuery<IndexedClass>("TEST", ic => ic.AString)}
                }
            };

            var nestedAgg = new TermsAggregation("TESTFIELD");

            var aggs = new Dictionary<string, IAggregation>
            {
                ["aggs_filter"] = new FilterAggregation
                {
                    Filter = qFilter,
                    Aggregations = new Dictionary<string, IAggregation>
                    {
                        ["nested"] = nestedAgg
                    }
                }
            };

            var json = JsonConvert.SerializeObject(aggs, JsonHelpers.CreateSerializerSettings());
            Assert.AreEqual("{\"aggs_filter\":{\"aggs\":{\"nested\":{\"terms\":{\"field\":\"TESTFIELD\"}}}," +
                            "\"filter\":{\"bool\":{\"must\":[{\"term\":{\"AString\":\"TEST\"}}]}}}}", json);
        }

        [TestMethod]
        public void Nested_Avg_Result_Json()
        {
            var json =
                "{\"took\":24,\"timed_out\":false,\"_shards\":{\"total\":5,\"successful\":5,\"skipped\":0,\"failed\":0},\"hits\":" +
                "{\"total\":3000,\"max_score\":0,\"hits\":[]},\"aggregations\":{\"nested_Products_0\":" +
                "{\"doc_count\":299728,\"Avg\":{\"value\":499.081400097394}}}}";

            var obj = JsonConvert.DeserializeObject<SearchResult<object>>(json, _json);

            Assert.AreEqual(obj.Hits.Total, 3000);
            Assert.AreEqual(obj.Aggregations["nested_Products_0"].DocCount, 299728);
            Assert.AreEqual(obj.Aggregations["nested_Products_0"].Avg.Value, 499.081400097394);

        }

        [TestMethod]
        public void Nested_Avg_As_Nested_Result_Json()
        {
            var json =
                "{\"took\":29,\"timed_out\":false,\"_shards\":{\"total\":5,\"successful\":5,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":5000,\"max_score\":0,\"hits\":[]}," +
                "\"aggregations\":{\"nested_Products_0\":{\"doc_count\":501477,\"nested\":{\"value\":499.45137828364056}}}}";

            var obj = JsonConvert.DeserializeObject<SearchResult<object>>(json, _json);

            Assert.AreEqual(obj.Hits.Total, 5000);
            Assert.AreEqual(obj.Aggregations["nested_Products_0"].DocCount, 501477);
            Assert.AreEqual(obj.Aggregations["nested_Products_0"].Nested.Value, 499.45137828364056);

        }

        [TestMethod]
        public void Nested_Buckets_Json()
        {
            var json =
                "{\"took\":51,\"timed_out\":false,\"_shards\":{\"total\":3,\"successful\":3,\"skipped\":0,\"failed\":0},\"hits\":" +
                "{\"total\":1000,\"max_score\":0,\"hits\":[]},\"aggregations\":{\"agg_DateHistogram\":{\"buckets\":[" +
                "{\"key_as_string\":\"2018-11-05T00:00:00.000Z\",\"key\":1541376000000,\"doc_count\":2,\"nested\":" +
                "{\"doc_count_error_upper_bound\":0,\"sum_other_doc_count\":0,\"buckets\":[]}},{\"key_as_string\":\"2018-10-29T00:00:00.000Z\"," +
                "\"key\":1540771200000,\"doc_count\":19,\"nested\":{\"doc_count_error_upper_bound\":0,\"sum_other_doc_count\":9,\"buckets\":[" +
                "{\"key\":\"9213ea30-ffe4-4488-9ad9-321311f80e36\",\"doc_count\":1,\"sum\":{\"value\":184}},{\"key\":\"e1d040fd-fa93-40d5-84fe-980646ab2b40\"," +
                "\"doc_count\":1,\"sum\":{\"value\":181}}]}}]}}}";

            var obj = JsonConvert.DeserializeObject<SearchResult<object>>(json, _json);

            Assert.AreEqual(obj.Aggregations["agg_DateHistogram"].Buckets[1].Nested.Buckets[0].Sum.Value, 184);
            Assert.AreEqual(obj.Aggregations["agg_DateHistogram"].Buckets[1].Nested.Buckets[1].Sum.Value, 181);
        }

        [TestMethod]
        public void Aggr_After()
        {
            var aggr = new Aggr {After = new After("field", "value")};
            var json = JsonConvert.SerializeObject(aggr, _json);
            Assert.AreEqual("{\"after\":{\"field\":\"value\"}}", json);
        }

        [TestMethod]
        public void Aggr_Aggs()
        {
            var aggr = new Aggr
            {
                Aggs = new Dictionary<string, IAggregation>
                {
                    ["key"] = new AvgAggregation("fieldName")
                }
            };
            var json = JsonConvert.SerializeObject(aggr, _json);
            Assert.AreEqual("{\"aggs\":{\"key\":{\"avg\":{\"field\":\"fieldName\"}}}}", json);
        }

        [TestMethod]
        public void RangeAggr_To()
        {
            var aggr = new RangeAggrValue<int> {To = 4};
            var json = JsonConvert.SerializeObject(aggr, _json);
            Assert.AreEqual("{\"from\":0,\"to\":4}", json);
        }

        [TestMethod]
        public void IntervalAggr_MinDocCount()
        {
            var aggr = new IntervalAggr<int>() {MinDocCount = 4};
            var json = JsonConvert.SerializeObject(aggr, _json);
            Assert.AreEqual("{\"interval\":0,\"min_doc_count\":4}", json);
        }

        [TestMethod]
        public void GeoBoundingBoxFilter_ctor()
        {
            var filter = new GeoBoundingBoxFilter
            {
                GeoBoundingBox = new GeoBoundingBoxFilterItem {["key"] = new GeoBox("A", "B")}
            };
            var json = JsonConvert.SerializeObject(filter, _json);
            Assert.AreEqual("{\"geo_bounding_box\":{\"key\":{\"top_left\":\"A\",\"bottom_right\":\"B\"}}}", json);
        }

        [TestMethod]
        public void RangeAggregation_ctor()
        {
            var aggr = new RangeAggregation<int>("field");
            var json = JsonConvert.SerializeObject(aggr, _json);
            Assert.AreEqual("{\"range\":{\"field\":\"field\"}}", json);
        }

        [TestMethod]
        public void DateHistogramAggregation_ctor()
        {
            var aggr = new DateHistogramAggregation("field", "week", AggsOrderBy.Count, AggsOrderDirection.Asc);
            var json = JsonConvert.SerializeObject(aggr, _json);
            Assert.AreEqual(
                "{\"date_histogram\":{\"interval\":\"week\",\"field\":\"field\",\"order\":{\"_count\":\"asc\"}}}",
                json);
        }

        [TestMethod]
        public void MinAggregation_ctor()
        {
            var aggr = new MinAggregation("field");
            var json = JsonConvert.SerializeObject(aggr, _json);
            Assert.AreEqual("{\"min\":{\"field\":\"field\"}}", json);
        }

        [TestMethod]
        public void CompositeAggregation_ctor()
        {
            var aggr = new CompositeAggregation
            {
                Composite = new CompositeAggr
                {
                    Sources = new List<IAggregation>
                    {
                        new AvgAggregation("field")
                    }
                }
            };
            var json = JsonConvert.SerializeObject(aggr, _json);
            Assert.AreEqual("{\"composite\":{\"sources\":[{\"avg\":{\"field\":\"field\"}}]}}", json);
        }
    }
}