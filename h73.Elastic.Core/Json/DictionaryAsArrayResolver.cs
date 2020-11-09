using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace h73.Elastic.Core.Json
{
    /// <inheritdoc />
    /// <summary>
    /// DictionaryAsArrayResolver
    /// </summary>
    /// <seealso cref="T:Newtonsoft.Json.Serialization.DefaultContractResolver" />
    public class DictionaryAsArrayResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            return objectType.GetInterfaces().Any(i =>
                i == typeof(IDictionary) ||
                (i.IsGenericType &&
                 i.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
                ? CreateArrayContract(objectType)
                : base.CreateContract(objectType);
        }
    }
}