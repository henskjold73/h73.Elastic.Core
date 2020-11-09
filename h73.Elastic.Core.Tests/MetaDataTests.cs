using System.Collections.Generic;
using h73.Elastic.Core.Data;
using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class MetaDataTests
    {
        [TestMethod]
        public void MetaData_Strings()
        {
            var icMeta = new IndexedClass
            {
                AString = "AString.Value"
            };

            var ic = new IndexedClass
            {
                MetaData = icMeta
            };

            var json = JsonConvert.SerializeObject(ic, JsonHelpers.CreateSerializerSettings());
        }

        [TestMethod]
        public void MetaData_Convert()
        {
            var icMeta = new IndexedClass
            {
                AString = "AString.Value"
            };

            var ic = new IndexedClass
            {
                MetaData = icMeta
            };

            IndexedClass ic2 = ic.MetaData;
            MetaData<IndexedClass> icm = ic;
        }

        [TestMethod]
        public void MetaData_Expressions()
        {
                var obj = new IndexedClass();
                var q = ExpressionHelper.GetPropertyName<IndexedClass>(
                    x => x.MetaData2.MetaPropertyName(md => md.IndexedClass21S)
                    );
                Assert.AreEqual("MetaData2.IndexedClass21S", q);
        }

    }
}