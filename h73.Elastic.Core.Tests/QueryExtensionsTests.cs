using System.Collections.Generic;
using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Search.Queries;
using h73.Elastic.Core.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class QueryExtensionsTests
    {
        [TestMethod]
        public void Naming_Match_TypeKey()
        {
            var query = new MatchQuery<IndexedClass>("not important",ic => ic.AString).Name();
            var expected = "h73.Elastic.Core.Tests.Support.IndexedClass$AString";
            Assert.AreEqual(expected, query._Name);
        }

        [TestMethod]
        public void Naming_Match_TypeKey_Nested()
        {
            var query = new MatchQuery<IndexedClass>("not important", ic => ic.Child.Child.AString).Name();
            var expected = "h73.Elastic.Core.Tests.Support.IndexedClass$Child.Child.AString";
            Assert.AreEqual(expected, query._Name);
        }

        [TestMethod]
        public void InheritedTypes_IC_All4() //Might need to change on IndexedClass change
        {
            var ic = typeof(IndexedClass);
            var types = ic.InheritedTypes();
            Assert.AreEqual(4, types.Length);
        }

        [TestMethod]
        public void InheritedTypes_IC2_IC2IC21() //Might need to change on IndexedClass2 change
        {
            var ic = typeof(IndexedClass2);
            var types = ic.InheritedTypes();
            Assert.AreEqual(2, types.Length);
        }

        [TestMethod]
        public void InheritedTypes_IC3_IC3() //Might need to change on IndexedClass3 change
        {
            var ic = typeof(IndexedClass3);
            var types = ic.InheritedTypes();
            Assert.AreEqual(1, types.Length);
        }
    }
}