using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class ExpressionHelperTests
    {
        [TestMethod]
        public void GetPropertyName_Child()
        {
            Expression<Func<IndexedClass, object>> fieldExpression = ic => ic.Child;
            var result = ExpressionHelper.GetPropertyName(fieldExpression);

            Assert.AreEqual("Child", result);
        }

        [TestMethod]
        public void GetPropertyName_Child_SomeNumber()
        {
            Expression<Func<IndexedClass, object>> fieldExpression = ic => ic.Child.SomeNumber;
            var result = ExpressionHelper.GetPropertyName(fieldExpression);

            Assert.AreEqual("Child.SomeNumber", result);
        }

        [TestMethod]
        public void PropertyName_Test1()
        {
            var obj = new List<IndexedClass>();
            var q = ExpressionHelper.GetPropertyName<IndexedClass>(x => x.Children.PropertyName(c => c.AString));
            Assert.AreEqual("Children.AString", q);
        }
    }
}