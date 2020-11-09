using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search;
using h73.Elastic.Core.Search.Aggregations;
using h73.Elastic.Core.Search.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class TopHitsTests
    {
        [TestMethod]
        public void TopHits_Json()
        {
            var aggs = new Dictionary<string, IAggregation>
            {
                ["top_tags"] = new TermsAggregation("type", 3)
                {
                    Aggregations = new Dictionary<string, IAggregation>
                    {
                        ["top_hits"] = new TopHitsAggregation
                        {
                            TopHits = new TopHits
                            {
                                Size = 1,
                                Sorts = new [] {new Sort {["date"] = "desc"}},
                                SourceObject = new Source {Includes = new[]{"date","price"}}
                            }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(aggs, JsonHelpers.CreateSerializerSettings());
            Assert.AreEqual("{\"top_tags\":{\"terms\":{\"field\":\"type\",\"size\":3},\"aggs\":" +
                            "{\"top_hits\":{\"top_hits\":{\"_source\":{\"includes\":[\"date\",\"price\"]}" +
                            ",\"sort\":[{\"date\":\"desc\"}],\"size\":1}}}}}", json);
        }

        [TestMethod]
        public void TopHits_WriteJson()
        {
            var topHitsConverter = new TopHitsConverter(Assembly.GetAssembly(GetType()));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            topHitsConverter.WriteJson(new JsonTextWriter(sw), null, new JsonSerializer());
        }

        [TestMethod]
        public void TopHits_ReadJson()
        {
            var topHitsConverter = new TopHitsConverter();
            var reader = new JsonTextReader(new StringReader("{null}"));
            var read = topHitsConverter.ReadJson(reader, typeof(FilterAggregation), null, new JsonSerializer());
            Assert.AreEqual(null, read);
        }
    }
}