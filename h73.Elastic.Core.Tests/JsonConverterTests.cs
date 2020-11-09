using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using h73.Core;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search.Results;
using h73.Elastic.Core.Tests.Support;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class JsonConverterTests
    {
        [TestMethod]
        public void Serialize_type_exists()
        {
            var ic = Builder<IndexedClass>.CreateNew().Build();
            var serializerSettings = new ElasticIndexer<IndexedClass>(new ElasticClient()).SerializerSettings;
            serializerSettings.TypeNameHandling = TypeNameHandling.Objects;
            var body = JsonConvert.SerializeObject(ic, serializerSettings);
            Assert.IsTrue(body.Contains("$type"));
        }



        [TestMethod]
        public void Serialize_empty_collection_removed()
        {
            var ic = Builder<IndexedClass>.CreateNew().Build();
            ic.List = new List<string>();
            var serializerSettings = new ElasticIndexer<IndexedClass>(new ElasticClient()).SerializerSettings;
            var body = JsonConvert.SerializeObject(ic, serializerSettings);
            Assert.IsFalse(body.Contains("List"));
        }

        [TestMethod]
        public void LargeResult_Json()
        {
            string json;
            using (StreamReader r = new StreamReader("Support/result1.json"))
            {
                json = r.ReadToEnd();
            }

            var js = JsonHelpers.CreateSerializerSettings<object>();
            var obj1 = JsonConvert.DeserializeObject<SearchResult<object>>(json, js);
            var jsNull = JsonHelpers.CreateSerializerSettings();
            var obj2 = JsonConvert.DeserializeObject<SearchResult<object>>(json, jsNull);
        }
    }
}