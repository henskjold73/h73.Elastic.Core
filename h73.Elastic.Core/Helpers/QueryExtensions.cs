using System;
using System.Linq;
using System.Reflection;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Search.Interfaces;
using h73.Elastic.Core.Search.Queries;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Helpers
{
    /// <summary>
    /// Extensions for the Query class
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Json string of the IQuery</returns>
        public static string ToJson(this IQuery query, JsonSerializerSettings serializerSettings = null)
        {
            if (serializerSettings == null) serializerSettings = JsonHelpers.CreateSerializerSettings();
            return JsonConvert.SerializeObject(query, serializerSettings);
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Json string of the QueryInit</returns>
        public static string ToJson(this QueryInit query, JsonSerializerSettings serializerSettings = null)
        {
            if (serializerSettings == null) serializerSettings = JsonHelpers.CreateSerializerSettings();
            return JsonConvert.SerializeObject(query, serializerSettings);
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="aggr">The aggr.</param>
        /// <returns>Json string of the IAggregation</returns>
        public static string ToJson(this IAggregation aggr, JsonSerializerSettings serializerSettings = null)
        {
            if (serializerSettings == null) serializerSettings = JsonHelpers.CreateSerializerSettings();
            return JsonConvert.SerializeObject(aggr, serializerSettings);
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Json string of the IFilter</returns>
        public static string ToJson(this IFilter filter, JsonSerializerSettings serializerSettings = null)
        {
            if (serializerSettings == null) serializerSettings = JsonHelpers.CreateSerializerSettings();
            return JsonConvert.SerializeObject(filter, serializerSettings);
        }

        /// <summary>
        /// Determines whether this instance is null.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   <c>true</c> if the specified query is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(this object query)
        {
            return query == null;
        }

        /// <summary>
        /// Determines whether this instance is null.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   <c>true</c> if the specified query is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(this string query)
        {
            return string.IsNullOrEmpty(query);
        }

        /// <summary>
        /// Determines whether [is not null].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if [is not null] [the specified object]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNull(this object obj)
        {
            if (obj != null && obj is DateTime time)
            {
                return time.IsNotNull();
            }

            return obj != null;
        }

        /// <summary>
        /// Determines whether [is not null].
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>
        ///   <c>true</c> if [is not null] [the specified dt]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNull(this DateTime dt)
        {
            return dt > DateTime.MinValue;
        }

        /// <summary>
        /// Inherited types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="assembly">The assembly.</param>
        /// <returns>InheritedTypes full name as array</returns>
        public static string[] InheritedTypes(this Type type, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = type.Assembly;
            }

            var assemblyTypes = assembly.GetTypes();
            return assemblyTypes.Where(type.IsAssignableFrom).Select(x => x.FullName).ToArray();
        }

        /// <summary>
        /// Names the specified query.
        /// </summary>
        /// <typeparam name="T">Type of T</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>MatchQuery</returns>
        public static MatchQuery<T> Name<T>(this MatchQuery<T> query)
            where T : class
        {
            query._Name = $"{typeof(T).FullName}${query.Match.First().Key}";
            return query;
        }

        /// <summary>
        /// Names the specified query.
        /// </summary>
        /// <typeparam name="T">Type of T</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>Name</returns>
        public static CommonQuery<T> Name<T>(this CommonQuery<T> query)
            where T : class
        {
            query._Name = $"{typeof(T).FullName}${query.Match.First().Key}";
            return query;
        }

        /// <summary>
        /// Names the specified query.
        /// </summary>
        /// <typeparam name="T">Type of T</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>Name</returns>
        public static TermQuery<T> Name<T>(this TermQuery<T> query)
            where T : class
        {
            query._Name = $"{typeof(T).FullName}${query.Term.First().Key}";
            return query;
        }

        /// <summary>
        /// Names the specified name.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="name">The name.</param>
        /// <returns>Name</returns>
        public static IQuery Name(this IQuery query, string name)
        {
            query._Name = name;
            return query;
        }

        /// <summary>
        /// Names the specified name.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="name">The name.</param>
        /// <returns>Name</returns>
        public static QueryInit Name(this QueryInit query, string name)
        {
            query._Name = name;
            return query;
        }
    }
}