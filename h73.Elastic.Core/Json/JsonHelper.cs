using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace h73.Elastic.Core.Json
{
    public static class JsonHelpers
    {
        /// <summary>
        /// Creates the serializer settings.
        /// </summary>
        /// <returns>Default serializer settings</returns>
        public static JsonSerializerSettings CreateSerializerSettings(
            PreserveReferencesHandling preserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling referenceLoopHandling = ReferenceLoopHandling.Ignore,
            DefaultContractResolver contractResolver = null,
            TypeNameHandling typeNameHandling = TypeNameHandling.None,
            NullValueHandling nullValueHandling = NullValueHandling.Ignore)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = preserveReferencesHandling,
                ReferenceLoopHandling = referenceLoopHandling,
                ContractResolver = contractResolver ?? new JsonContractResolver(),
                TypeNameHandling = typeNameHandling,
                NullValueHandling = nullValueHandling
            };
            serializerSettings.Converters.Add(new ElasticStringEnumConverter());
            serializerSettings.Converters.Add(new TopHitsConverter());
            serializerSettings.Converters.Add(new FilteredAggregationConverter());
            return serializerSettings;
        }

        public static JsonSerializerSettings CreateSerializerSettings<T>(
            PreserveReferencesHandling preserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling referenceLoopHandling = ReferenceLoopHandling.Ignore,
            DefaultContractResolver contractResolver = null,
            TypeNameHandling typeNameHandling = TypeNameHandling.None,
            NullValueHandling nullValueHandling = NullValueHandling.Ignore)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = preserveReferencesHandling,
                ReferenceLoopHandling = referenceLoopHandling,
                ContractResolver = contractResolver ?? new JsonContractResolver(),
                TypeNameHandling = typeNameHandling,
                NullValueHandling = nullValueHandling
            };
            serializerSettings.Converters.Add(new ElasticStringEnumConverter());
            serializerSettings.Converters.Add(new TopHitsConverter(typeof(T)));
            serializerSettings.Converters.Add(new FilteredAggregationConverter());
            return serializerSettings;
        }
    }
}