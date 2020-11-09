using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class BucketTests
    {
        private JsonSerializerSettings _j;

        [TestInitialize]
        public void Setup()
        {
            _j = JsonHelpers.CreateSerializerSettings();
        }

        [TestMethod]
        public void Bucket_Min()
        {
            var bucket = new Bucket
            {
                Min = new AggValue {Value = 7.3},
                KeyAsString = "7.3",
                To = "7.3",
                From = "7.3"
            };
            Assert.AreEqual(7.3, bucket.Min.Value);
            Assert.AreEqual("7.3", bucket.KeyAsString);
            Assert.AreEqual("7.3", bucket.To);
            Assert.AreEqual("7.3", bucket.From);
        }
    }
}