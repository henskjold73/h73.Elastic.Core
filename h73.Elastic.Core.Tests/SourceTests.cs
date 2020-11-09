using h73.Elastic.Core.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class SourceTests
    {
        [TestMethod]
        public void Source_Output_Invalid_null()
        {
            var src = new Source();
            Assert.IsNull(src.Output);
        }

        [TestMethod]
        public void Source_Output_Valid_null()
        {
            var src = new Source {Includes = new string[] { }};
            Assert.IsNull(src.Output);
        }
    }
}