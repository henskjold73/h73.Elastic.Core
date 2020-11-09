using System.IO;
using System.Reflection;
using System.Text;
using h73.Elastic.Core.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class ElasticStringEnumConverterTests
    {
        [TestMethod]
        public void ElasticStringEnumConverter_WriteJson()
        {
            var elasticStringEnumConverter = new ElasticStringEnumConverter();
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            elasticStringEnumConverter.WriteJson(new JsonTextWriter(sw), null, new JsonSerializer());
        }
    }
}