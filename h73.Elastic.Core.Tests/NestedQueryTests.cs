using System.Collections.Generic;
using System.Linq;
using h73.Elastic.Core.Enums;
using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Search.Interfaces;
using h73.Elastic.Core.Search.Queries;
using h73.Elastic.Core.Search.Results;
using h73.Elastic.Core.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class NestedQueryTests
    {
        [TestMethod]
        public void NestedQuery_Simple()
        {
            var nq = new NestedQuery {Nested = new NestedItem {Path = "PropertyName"}};
            var jsonNq = nq.ToJson();

            var b = new BooleanQuery();
            b.Must.Add(QueryHelper.MatchAll());
            var nqb = new NestedQuery
                {Nested = new NestedItem {Path = "PropertyName", Query = new Dictionary<string, IQuery> {{"bool", b}}}};
            var jsonNqb = nqb.ToJson();

            Assert.AreEqual("{\"nested\":{\"path\":\"PropertyName\"}}", jsonNq);
            Assert.AreEqual(
                "{\"nested\":{\"path\":\"PropertyName\",\"query\":{\"bool\":{\"must\":[{\"match_all\":{}}]}}}}",
                jsonNqb);
        }

        [TestMethod]
        public void NestedQuery_ScoreMode_Sum()
        {
            var b = new BooleanQuery();
            b.Must.Add(new RangeQuery<object>
            {
                Range = new RangeQueryItem<object>
                {
                    {
                        "Property", new RangeQueryValue<object>
                        {
                            Boost = 0,
                            GreaterThan = 5
                        }
                    }

                }
            });
            var nqb = new NestedQuery
            {
                Nested = new NestedItem
                {
                    Path = "PropertyName", 
                    ScoreMode = ScoreMode.Sum,
                    Query = new Dictionary<string, IQuery>
                    {
                        {"bool", b}
                    }
                }
            };
            var jsonNqb = nqb.ToJson();

            Assert.AreEqual(
                "{\"nested\":{\"path\":\"PropertyName\",\"score_mode\":\"Sum\",\"query\":{\"bool\":{\"must\":[{\"range\":{\"Property\":{\"gt\":5,\"boost\":0.0}}}]}}}}",
                jsonNqb);
        }

        [TestMethod]
        public void InnerHitsTests_Deserialize_EmptyInnerHits()
        {
            var result =
                "{\"took\":1,\"timed_out\":false,\"_shards\":{\"total\":3,\"successful\":3,\"skipped\":0,\"failed\":0}," +
                "\"hits\":{\"total\":895,\"max_score\":1,\"hits\":[{\"_index\":\"h73dev_h73_groundfault_component\"," +
                "\"_type\":\"h73.GroundFault.Component\",\"_id\":\"9740e6f9-07fe-4624-9736-fff265751a8f\",\"_score\":1," +
                "\"_source\":{},\"inner_hits\":{}}]}}";

            var jsonObject = JsonConvert.DeserializeObject<SearchResult<IndexedClass>>(result);
            Assert.AreEqual(0, jsonObject.Hits.HitsList.First().InnerHits.Count);
        }

        [TestMethod]
        public void InnerHitsTests_Deserialize_OnlyInnerHits()
        {
            var result =
                "{\"Events\":{\"hits\":{\"total\":2,\"max_score\":1,\"hits\":[{\"_index\":\"h73dev_h73_groundfault_component\"," +
                "\"_type\":\"h73.GroundFault.Component\",\"_id\":\"9740e6f9-07fe-4624-9736-fff265751a8f\",\"_nested\":" +
                "{\"field\":\"Events\",\"offset\":7},\"_score\":1,\"_source\":{\"Type\":\"GroundFault\",\"Start\":" +
                "\"2017-10-08T09:29:56.9326293+02:00\",\"End\":\"2017-10-09T16:37:56.9326293+02:00\",\"CaseCreated\":false," +
                "\"PowerCurrent\":79.34553667360244,\"Voltage\":86.8306860112728,\"Duration\":1868}},{\"_index\":" +
                "\"h73dev_h73_groundfault_component\",\"_type\":\"h73.GroundFault.Component\",\"_id\":" +
                "\"9740e6f9-07fe-4624-9736-fff265751a8f\",\"_nested\":{\"field\":\"Events\",\"offset\":0},\"_score\":1," +
                "\"_source\":{\"Type\":\"GroundFault\",\"Start\":\"2018-08-05T09:29:56.9322674+02:00\",\"End\":" +
                "\"2018-08-06T17:52:56.9322674+02:00\",\"CaseCreated\":false,\"PowerCurrent\":52.268288879314575,\"Voltage\":" +
                "111.51511493302654,\"Duration\":1943}}]}}}";

            var jsonObject = JsonConvert.DeserializeObject<InnerHitsResult>(result);
            Assert.AreEqual(2, jsonObject.Single().Value.Hit.Total);
        }

        [TestMethod]
        public void InnerHitsTests_Deserialize_FullResult()
        {
            var result =
                "{\"took\":1,\"timed_out\":false,\"_shards\":{\"total\":3,\"successful\":3,\"skipped\":0,\"failed\":0},\"hits\":" +
                "{\"total\":895,\"max_score\":1,\"hits\":[{\"_index\":\"h73dev_h73_groundfault_component\",\"_type\":" +
                "\"h73.GroundFault.Component\",\"_id\":\"9740e6f9-07fe-4624-9736-fff265751a8f\",\"_score\":1,\"_source\":" +
                "{\"ParticipantId\":4,\"Description\":\"Substation H570-T1L\",\"ValidTo\":\"2100-12-31T00:00:00\",\"StSrid\":4326," +
                "\"ValidFrom\":\"2000-01-01T00:00:00\",\"Id\":17498,\"AssetGuid\":\"9740e6f9-07fe-4624-9736-fff265751a8f\",\"Name\":" +
                "\"570HT1\"},\"inner_hits\":{\"Events\":{\"hits\":{\"total\":2,\"max_score\":1,\"hits\":[{\"_index\":" +
                "\"h73dev_h73_groundfault_component\",\"_type\":\"h73.GroundFault.Component\",\"_id\":" +
                "\"9740e6f9-07fe-4624-9736-fff265751a8f\",\"_nested\":{\"field\":\"Events\",\"offset\":7},\"_score\":1," +
                "\"_source\":{\"Type\":\"GroundFault\",\"Start\":\"2017-10-08T09:29:56.9326293+02:00\",\"End\":" +
                "\"2017-10-09T16:37:56.9326293+02:00\",\"CaseCreated\":false,\"PowerCurrent\":79.34553667360244,\"Voltage" +
                "\":86.8306860112728,\"Duration\":1868}},{\"_index\":\"h73dev_h73_groundfault_component\",\"_type\":" +
                "\"h73.GroundFault.Component\",\"_id\":\"9740e6f9-07fe-4624-9736-fff265751a8f\",\"_nested\":{\"field\":\"Events\"," +
                "\"offset\":0},\"_score\":1,\"_source\":{\"Type\":\"GroundFault\",\"Start\":\"2018-08-05T09:29:56.9322674+02:00\",\"End" +
                "\":\"2018-08-06T17:52:56.9322674+02:00\",\"CaseCreated\":false,\"PowerCurrent\":52.268288879314575,\"Voltage\":" +
                "111.51511493302654,\"Duration\":1943}}]}}}}]}}";

            var jsonObject = JsonConvert.DeserializeObject<SearchResult<dynamic>>(result);
            var inner = jsonObject.Hits.HitsList.Single().InnerHits.Values.First().Hit.Total;
            Assert.AreEqual(2, inner);
        }
    }
}