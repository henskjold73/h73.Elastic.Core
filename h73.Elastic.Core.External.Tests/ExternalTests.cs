using h73.Elastic.Core.External.Tests.Support;
using h73.Elastic.Core.Tests;
using h73.Elastic.TypeMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.External.Tests
{
    [TestClass]
    public class ExternalTests
    {
        [TestMethod]
        public void InheritedTypeExternal()
        {
            var type = typeof(ExternalClass);
            var assembly = typeof(ExternalClass).Assembly;
            var result = ExternalTestsHelper.GetInheritedTypes(type, assembly);

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void ExternalTypeMapping()
        {
            var type = typeof(ExternalClass);
            var tm = type.GetTypeMapping<ExternalClass>();
            var json = JsonConvert.SerializeObject(new { mappings = tm });
            Assert.AreEqual("{\"mappings\":{\"h73.Elastic.Core.External.Tests.Support.ExternalClass\":{\"properties\":{\"Country\":{\"type\":\"keyword\"},\"Geo\":{\"type\":\"geo_point\"}}}}}", json);
        }
    }
}