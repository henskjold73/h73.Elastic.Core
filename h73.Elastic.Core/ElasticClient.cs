using System;
using System.Linq;
using Elasticsearch.Net;
using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Response;
using h73.Elastic.Core.Server;
using h73.Elastic.TypeMapping;
using Newtonsoft.Json;

namespace h73.Elastic.Core
{
    /// <summary>
    /// Client for communication with elasticsearch
    /// </summary>
    public class ElasticClient : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticClient"/> class.
        /// </summary>
        /// <param name="protocol">http/https</param>
        /// <param name="node">elasticsearch node</param>
        /// <param name="port">port</param>
        /// <param name="debug">Json indentation and responses returned</param>
        /// <param name="username">username</param>
        /// <param name="psw">password</param>
        /// <param name="tenantId">tenant id</param>
        public ElasticClient(
            string protocol = "http",
            string node = "localhost",
            int port = 9200,
            bool debug = false,
            string username = "",
            string psw = "",
            string tenantId = "")
        {
            var authenticationString = !string.IsNullOrEmpty(username)
                ? $"{protocol}://{username}:{psw}@{node}:{port}"
                : $"{protocol}://{node}:{port}";
            var uri = new Uri(authenticationString);
            var settings = new ConnectionConfiguration(uri);
            TenantId = tenantId;
            if (debug)
            {
                settings.EnableDebugMode();
                settings.DisableDirectStreaming();
                settings.PrettyJson();
            }

            Client = new ElasticLowLevelClient(settings);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticClient"/> class.
        /// </summary>
        /// <param name="settings">Created from a <see cref="ConnectionConfiguration"/> class.</param>
        public ElasticClient(ConnectionConfiguration settings)
        {
            Client = new ElasticLowLevelClient(settings);
        }

        /// <summary>
        /// Gets low level client from elasticsearch.net
        /// </summary>
        public ElasticLowLevelClient Client { get; private set; }

        /// <summary>
        /// Gets or sets JsonSerializer to be used by the client
        /// </summary>
        public JsonSerializer Serializer { get; set; }

        /// <summary>
        /// Gets or sets TenantId
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Returns server info
        /// </summary>
        /// <returns>elasticsearch server info</returns>
        public ServerInfo Info()
        {
            var response = Client.DoRequest<StringResponse>(HttpMethod.GET, "/");
            var info = new ServerInfo();
            if (response.Success)
            {
                info = JsonConvert.DeserializeObject<ServerInfo>(response.Body);
            }

            return info;
        }

        /// <summary>
        /// Returns Indices information
        /// </summary>
        /// <returns>Indices information List&lt;IndexInfo&gt;</returns>
        public Indices Indices()
        {
            var response = Client.DoRequest<StringResponse>(HttpMethod.GET, "/_cat/indices");
            var indecies = new Indices();
            if (response.Success)
            {
                indecies = JsonConvert.DeserializeObject<Indices>(response.Body);
            }

            return indecies;
        }

        /// <summary>
        /// Does the index exist
        /// </summary>
        /// <param name="indexName">index name</param>
        /// <returns>True/False</returns>
        public bool IndexExists(string indexName)
        {
            return Indices().Select(x => x.Index).Contains(indexName);
        }

        public void Dispose()
        {
            Client?.Settings?.Dispose();
        }
    }
}
