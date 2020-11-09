using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using h73.Elastic.Core.Server;
using h73.Elastic.Core.Tests.Support;
using h73.Elastic.TypeMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class CreateIndexTests
    {
        [TestMethod]
        public void CreateIndex_TypeMapping()
        {
            var shardSettings = new ShardsSettings(1, 1);

            var typeMapping = new TypeMapping<IndexedClass>();
            var mappings = new KeyValuePair<Expression<Func<IndexedClass, object>>, FieldTypes>(ic => ic.ListObjects, FieldTypes.Nested);
            typeMapping.AddMapping(mappings);

            var json = JsonConvert.SerializeObject(new {settings = shardSettings.Settings, mappings = typeMapping});

            Assert.AreEqual("{\"settings\":{\"index\":{\"number_of_shards\":1,\"number_of_replicas\":1}},\"mappings\":{\"h73.Elastic.Core.Tests.Support.IndexedClass\":{\"properties\":{\"ListObjects\":{\"type\":\"nested\"}}}}}",
                json);
        }
    }
}