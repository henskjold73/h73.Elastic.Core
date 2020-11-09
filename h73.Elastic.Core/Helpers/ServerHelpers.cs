using System;
using System.Text.RegularExpressions;

namespace h73.Elastic.Core.Helpers
{
    /// <summary>
    /// Helpers associated with the elasticsearch server
    /// </summary>
    public static class ServerHelpers
    {
        /// <summary>
        /// Create and index name from the tenantId and Type of T
        /// </summary>
        /// <param name="tenantId">TenantId</param>
        /// <typeparam name="T">Type of T</typeparam>
        /// <returns>index name &lt;tenantId&gt;_&lt;namespace_type&gt;</returns>
        /// <exception cref="Exception">Thrown if the type of T's full name can not be found</exception>
        public static string CreateIndexName<T>(string tenantId)
            where T : class
        {
            var fullName = typeof(T).FullName;
            if (fullName == null)
            {
                throw new Exception("Unable to find type full name");
            }

            return CreateIndexName(tenantId, fullName);
        }

        /// <summary>
        /// Create and index name from the tenantId and type
        /// </summary>
        /// <param name="tenantId">TenantId</param>
        /// <param name="typeFullName">Type full name</param>
        /// <returns>index name &lt;tenantId&gt;_&lt;namespace_type&gt;</returns>
        public static string CreateIndexName(string tenantId, string typeFullName)
        {
            var typeName = Regex.Replace(typeFullName.ToLower().Replace(".", "_"), "/[^a-z0-9]+/i", string.Empty);
            var indexName = $"{tenantId}_{typeName}";
            return indexName;
        }
    }
}
