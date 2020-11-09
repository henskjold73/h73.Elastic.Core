using System;
using h73.Elastic.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class ExceptionTests
    {
        private const string MsgPrefix = "What do you mean?! Query does not make any sense!";

        [TestMethod]
        public void AmbiguousException_Empty_Base()
        {
            var ex = new AmbiguousQueryException();
            var msg = ex.Message;

            Assert.AreEqual(MsgPrefix, msg);
        }

        [TestMethod]
        public void AmbiguousException_Test_Base()
        {
            var ctorMsg = "Test";
            var ex = new AmbiguousQueryException(ctorMsg);
            var msg = ex.Message;

            Assert.AreEqual($"{MsgPrefix} {ctorMsg}", msg);
        }
    }
}