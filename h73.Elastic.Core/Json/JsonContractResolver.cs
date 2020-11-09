using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace h73.Elastic.Core
{
    /// <summary>
    /// JsonContractResolver
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Serialization.DefaultContractResolver" />
    public class JsonContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Resolves the contract for a given type.
        /// </summary>
        /// <param name="type">The type to resolve a contract for.</param>
        /// <returns>
        /// The contract for a given type.
        /// </returns>
        public override JsonContract ResolveContract(Type type)
        {
            var contract = base.ResolveContract(type);
            contract.IsReference = false;
            return contract;
        }

        /// <summary>
        /// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
        /// <returns>
        /// A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            var isDefaultValueIgnored = ((property.DefaultValueHandling ??
                                          DefaultValueHandling.Ignore) & DefaultValueHandling.Ignore) != 0;
            if (!isDefaultValueIgnored
                || typeof(string).IsAssignableFrom(property.PropertyType)
                || !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                return property;
            }

            bool NewShouldSerialize(object obj)
            {
                var collection = property.ValueProvider.GetValue(obj) as ICollection;
                return collection == null || collection.Count != 0;
            }

            var oldShouldSerialize = property.ShouldSerialize;
            property.ShouldSerialize = oldShouldSerialize != null
                ? o => oldShouldSerialize(o) && NewShouldSerialize(o)
                : (Predicate<object>)NewShouldSerialize;

            return property;
        }
    }
}
