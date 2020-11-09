using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class ServerHelpersTests
    {
        [TestMethod]
        public void ServerHelpers_TenantPrefix()
        {
            var n = ServerHelpers.CreateIndexName<IndexedClass>("tenant");
            Assert.AreEqual("tenant_h73_elastic_core_tests_support_indexedclass", n);
        }
    }
}