using h73.Elastic.Core.Enums;
using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Search.Aggregations;
using h73.Elastic.Core.Search.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class FilterTests
    {
        [TestMethod]
        public void Filter_AddFilter_Aggr()
        {
            var aggr = new AggregationBase();
            aggr.Filter = new RangeQuery<double?>(24 * 60, "Events.Duration", RangeQueryType.GreaterThan);
            var json = aggr.ToJson();
            Assert.AreEqual("{\"filter\":{\"range\":{\"Events.Duration\":{\"gt\":1440.0}}}}", json);
        }
    }
}