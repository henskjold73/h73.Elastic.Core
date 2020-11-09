using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using h73.Core;
using h73.Elastic.Core.Enums;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search;
using h73.Elastic.Core.Search.Aggregations;
using h73.Elastic.Core.Search.Interfaces;
using h73.Elastic.Core.Search.Queries;
using h73.Elastic.Core.Search.Results;
using h73.Elastic.Core.Tests.Support;
using h73.Elastic.TypeMapping;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Fields = h73.Elastic.Core.Search.Fields;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class JsonTests
    {
        [TestMethod]
        public void Serialize_source_is_okay()
        {
            var source1 = new Source {Values = new[] {"*"}};
            var source2 = new Source {Excludes = new[] {"*"}};
            var source3 = new Source {Includes = new[] {"*"}};
            var source4 = new Source {Excludes = new[] {"*"}, Includes = new[] {"*"}};

            Assert.IsTrue(source1.IsValid());
            Assert.IsTrue(source2.IsValid());
            Assert.IsTrue(source3.IsValid());
            Assert.IsTrue(source4.IsValid());
        }

        [TestMethod]
        public void Serialize_source_is_notokay()
        {
            var source1 = new Source {Values = new[] {"*"}, Includes = new[] {"*"}};
            var source2 = new Source {Values = new[] {"*"}, Excludes = new[] {"*"}};
            var source3 = new Source {Values = new[] {"*"}, Excludes = new[] {"*"}, Includes = new[] {"*"}};
            var source4 = new Source();

            Assert.IsFalse(source1.IsValid());
            Assert.IsFalse(source2.IsValid());
            Assert.IsFalse(source3.IsValid());
            Assert.IsFalse(source4.IsValid());
        }

        [TestMethod]
        public void Serialize_source_toJson()
        {
            var source1 = new Source {Values = new[] {"1", "2"}};
            var source2 = new Source {Excludes = new[] {"1"}};
            var source3 = new Source {Includes = new[] {"1"}};
            var source4 = new Source {Excludes = new[] {"1", "2", "3"}, Includes = new[] {"1", "2", "3", "4"}};

            Assert.AreEqual("[\"1\",\"2\"]", JsonConvert.SerializeObject(source1.Output));
            Assert.AreEqual("{\"excludes\":[\"1\"]}", JsonConvert.SerializeObject(source2.Output));
            Assert.AreEqual("{\"includes\":[\"1\"]}", JsonConvert.SerializeObject(source3.Output));
            Assert.AreEqual("{\"includes\":[\"1\",\"2\",\"3\",\"4\"],\"excludes\":[\"1\",\"2\",\"3\"]}",
                JsonConvert.SerializeObject(source4.Output));
        }

        [TestMethod]
        public void Serialize_highlighting()
        {
            var highlighting1 = new Highlight
            {
                Fields = new Fields {["A"] = new HighlightingValue()}
            };
            var json = JsonConvert.SerializeObject(highlighting1);
            Assert.AreEqual("{\"fields\":{\"A\":{\"type\":\"plain\"}}}", json);
        }

        [TestMethod]
        public void Serialize_sorting()
        {
            var sorting = new Sort();
            sorting.AddSorting("Id", "asc");

            var obj = new {sort = sorting};

            var json = JsonConvert.SerializeObject(obj);
            Assert.AreEqual("{\"sort\":{\"Id\":\"asc\"}}", json);
        }

        [TestMethod]
        public void TypeMapping()
        {
            var obj = PredefinedTypeMapping.GetPredefinedTypeMapping<object>(typeof(IndexedClass));
            var json = JsonConvert.SerializeObject(obj);
            Assert.AreNotEqual(null, json);
        }

        [TestMethod]
        public void ViewportAggregation()
        {
            const string expected = "{\"geo_bounds\":{\"field\":\"Location.Point\",\"wrap_longitude\":true}}";
            var aggs = new GeoBoundingBoxAggregation("Location.Point");
            var json = JsonConvert.SerializeObject(aggs);
            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void ViewportAggregation2()
        {
            const string jsonIn =
                "{\"Viewport\": {\"bounds\": {\"top_left\": {\"lat\": 59.24208730459213,\"lon\": 9.669787622988224},\"bottom_right\": {\"lat\": 58.37131870910525,\"lon\": 11.351130176335573}}}}";
            var result = JsonConvert.DeserializeObject<Aggregations>(jsonIn);
            Assert.AreEqual(59.24208730459213, result["Viewport"].Bounds.TopLeft.Lat);
            Assert.AreEqual(11.351130176335573, result["Viewport"].Bounds.BottomRight.Lon);
        }

        [TestMethod]
        public void ViewportAggregation3()
        {
            const string expected =
                "{\"filter\":[{\"geo_bounding_box\":{\"Location.Point\":{\"top_left\":\"59.2420873045921, 9.66978762298822\",\"bottom_right\":\"58.3713187091053, 11.3511301763356\"}}}]}";
            const double latMin = 58.37131870910525;
            const double latMax = 59.24208730459213;
            const double lngMin = 9.669787622988224;
            const double lngMax = 11.351130176335573;

            var query = new BooleanQuery
            {
                Filter = new List<IQuery>
                {
                    new GeoBoundingBoxFilter("Location.Point", $"{latMax.ToString(CultureInfo.InvariantCulture)}, {lngMin.ToString(CultureInfo.InvariantCulture)}", $"{latMin.ToString(CultureInfo.InvariantCulture)}, {lngMax.ToString(CultureInfo.InvariantCulture)}")
                }
            };
            var json = JsonConvert.SerializeObject(query);

            Assert.AreEqual(expected, json);
        }


        [TestMethod]
        public void Range_Serial_Deserial()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new JsonContractResolver(),
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore
            }; 

            var rng = new RangeQuery<DateTime>(new DateTime(2020, 8, 20), "DATEFIELD", RangeQueryType.GreaterThanEqual);
            var json1 = JsonConvert.SerializeObject(rng, jsonSerializerSettings);
            var rng2 = JsonConvert.DeserializeObject<RangeQuery<DateTime>>(json1, jsonSerializerSettings);
            Assert.AreEqual(rng.Range["DATEFIELD"].GreaterThanOrEqual, rng2.Range["DATEFIELD"].GreaterThanOrEqual);
        }

        [TestMethod]
        public void Match_Serial_Deserial()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new JsonContractResolver(),
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore
            };

            var match1 = new MatchQuery<DateTime>{Match = new MatchQueryValue
            {
                ["KEY"] = "VALUE"
            }};
            var json1 = JsonConvert.SerializeObject(match1, jsonSerializerSettings);
            var match2 = JsonConvert.DeserializeObject<MatchQuery<DateTime>>(json1, jsonSerializerSettings);
            Assert.AreEqual(match1.Match["KEY"], match2.Match["KEY"]);
        }

        [TestMethod]
        public void Serialize_typeInArray_FlatNType()
        {
            var ic = new IndexedClass(){ListDates = new List<DateTime>()};

            for (int i = 0; i < 5; i++) ic.ListDates.Add(DateTime.Now.AddDays(i));

            var serializerSettings = JsonHelpers.CreateSerializerSettings();
            var serializerSettings2 = JsonHelpers.CreateSerializerSettings(typeNameHandling: TypeNameHandling.All);
            var body = JsonConvert.SerializeObject(ic, serializerSettings);
            var body2 = JsonConvert.SerializeObject(ic, serializerSettings2);
            Assert.IsFalse(body.Contains("$value"));
            Assert.IsTrue(body2.Contains("$value"));
        }

        [TestMethod]
        public void Bucket_Deserial_Key()
        {
            var json =
                "[{\"key\":\"u4xb\",\"doc_count\":4200,\"lon\":{\"value\":10.947712290965553},\"lat\":{\"value\":59.11423803927443}},{\"key\":\"u4rz\",\"doc_count\":2990,\"lon\":{\"value\":11.030104694427006},\"lat\":{\"value\":59.03246395254857}},{\"key\":\"u4x8\",\"doc_count\":1341,\"lon\":{\"value\":10.868397945938195},\"lat\":{\"value\":59.09932285812902}},{\"key\":\"u4rs\",\"doc_count\":4,\"lon\":{\"value\":10.61625486239791},\"lat\":{\"value\":58.37131870910525}},{\"key\":\"u4rr\",\"doc_count\":3,\"lon\":{\"value\":10.270185330882668},\"lat\":{\"value\":58.983558104373515}},{\"key\":\"u4rv\",\"doc_count\":2,\"lon\":{\"value\":11.030601589009166},\"lat\":{\"value\":58.57190775219351}},{\"key\":\"u680\",\"doc_count\":1,\"lon\":{\"value\":11.351130176335573},\"lat\":{\"value\":59.12867751438171}},{\"key\":\"u4xc\",\"doc_count\":1,\"lon\":{\"value\":11.000912133604288},\"lat\":{\"value\":59.24208730459213}},{\"key\":\"u4rp\",\"doc_count\":1,\"lon\":{\"value\":10.13147952966392},\"lat\":{\"value\":58.891057977452874}},{\"key\":\"u4rn\",\"doc_count\":1,\"lon\":{\"value\":9.964919462800026},\"lat\":{\"value\":58.88531880453229}},{\"key\":\"u4qy\",\"doc_count\":1,\"lon\":{\"value\":9.669787622988224},\"lat\":{\"value\":58.80914134439081}}]";

            var obj = JsonConvert.DeserializeObject<List<Bucket>>(json);

            Assert.AreEqual(11,obj.Count);
            Assert.AreEqual("u4xb", obj.First().Key);
        }

        [TestMethod]
        public void RangeAggs()
        {
            var rng = new RangeAggregation<int?>("Field", new RangeAggrValue<int?>
            {
                Key = "test", From = 0
            });
            var json = JsonConvert.SerializeObject(rng, JsonHelpers.CreateSerializerSettings());
        }
        
        [TestMethod]
        public void Result_To_Json()
        {
           var json = "{\"took\":4,\"timed_out\":false,\"_shards\":{\"total\":3,\"successful\":3,\"skipped\":0,\"failed\":0},\"hits\":" +
                      "{\"total\":1330,\"max_score\":0,\"hits\":[]},\"aggregations\":{\"terms_AssetGuid\":{\"doc_count_error_upper_bound\":3," +
                      "\"sum_other_doc_count\":1203,\"buckets\":[{\"key\":\"9af4387b-cecb-42e6-9e4b-a8bbca4cc662\",\"doc_count\":5,\"nested\":" +
                      "{\"buckets\":[{\"key\":\"Above120\",\"from\":120,\"doc_count\":1,\"tophits\":{\"hits\":{\"total\":1,\"max_score\":null," +
                      "\"hits\":[{\"_index\":\"h73dev_h73_core_event\",\"_type\":\"h73.Core.Event\",\"_id\":" +
                      "\"9af4387b-cecb-42e6-9e4b-a8bbca4cc662_EarthFault_636630941494510178\",\"_score\":null,\"_source\":" +
                      "{\"Description\":\"Samfunnshuset 2\",\"Start\":\"2018-05-28T08:49:09.4510178\",\"Duration\":2790.37785744,\"AssetGuid\":" +
                      "\"9af4387b-cecb-42e6-9e4b-a8bbca4cc662\",\"Name\":\"C038\",\"ParticipantId\":1,\"Type\":\"EarthFault\",\"ValidTo\":" +
                      "\"2073-05-31T23:00:00\",\"Long\":\"10.25648\",\"EventIdGuid\":\"9af4387b-cecb-42e6-9e4b-a8bbca4cc662_EarthFault_636630941494510178\"," +
                      "\"ValidFrom\":\"1979-12-31T23:00:00\",\"End\":\"2018-05-30T07:19:32.1224642\",\"Id\":45037,\"GeoLocation\":{\"lon\":10.25648,\"lat\":60.16737}," +
                      "\"Lat\":\"60.16737\"},\"sort\":[1527497349451]}]}},\"cardinality\":{\"value\":1}}]}}]}}}";

            var result = JsonConvert.DeserializeObject<SearchResult<object>>(json, JsonHelpers.CreateSerializerSettings());
        }

        [TestMethod]
        public void TopHitsResult_Json()
        {
            string json;
            using (StreamReader r = new StreamReader("Support/tophitsresult.json"))
            {
                json = r.ReadToEnd();
            }

            var obj = JsonConvert.DeserializeObject<SearchResult<Event>>(json, JsonHelpers.CreateSerializerSettings());
        }
    }
}