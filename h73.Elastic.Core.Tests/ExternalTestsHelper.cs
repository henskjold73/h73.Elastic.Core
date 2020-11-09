using System;
using System.Reflection;
using h73.Elastic.Core.Helpers;

namespace h73.Elastic.Core.Tests
{
    public static class ExternalTestsHelper
    {
        public static string[] GetInheritedTypes(Type type, Assembly assembly)
        {
            return type.InheritedTypes(assembly);
        }
    }
}