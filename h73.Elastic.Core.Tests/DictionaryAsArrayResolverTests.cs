using System.Collections.Generic;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class DictionaryAsArrayResolverTests
    {
        [TestMethod]
        public void DictionaryAsArrayResolver_CreateContract()
        {
            var dictionaryAsArrayResolver = new DictionaryAsArrayResolver();
            dictionaryAsArrayResolver.ResolveContract(typeof(Dictionary<string, AggValue>));
        }
    }
}