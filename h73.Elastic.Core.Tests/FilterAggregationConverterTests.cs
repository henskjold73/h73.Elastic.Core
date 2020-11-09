using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search.Aggregations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class FilterAggregationConverterTests
    {
        [TestMethod]
        public void FilterAggCon_WriteJson()
        {
            var filterAggCon = new FilteredAggregationConverter();
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            filterAggCon.WriteJson(new JsonTextWriter(sw), null, new JsonSerializer());
        }

        [TestMethod]
        public void FilterAggCon_ReadJson()
        {
            var filterAggCon = new FilteredAggregationConverter();
            var reader = new JsonTextReader(new StringReader("{null}"));
            var read = filterAggCon.ReadJson(reader, typeof(FilterAggregation), null, new JsonSerializer());
            Assert.AreEqual(null, read);
        }
    }
}