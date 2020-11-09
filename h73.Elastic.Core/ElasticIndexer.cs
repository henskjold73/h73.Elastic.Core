using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using h73.Elastic.Core.Actions;
using h73.Elastic.Core.Helpers;
using h73.Elastic.Core.Json;
using h73.Elastic.Core.Response;
using h73.Elastic.Core.Server;
using h73.Elastic.TypeMapping;
using Newtonsoft.Json;

namespace h73.Elastic.Core
{
    /// <summary>
    /// Class used for creating and deleting index, and indexing objects
    /// </summary>
    public class ElasticIndexer<T> where T : class, new() 
    {
        private readonly ElasticClient _client;
        
        public ElasticIndexer(){}

        public ElasticIndexer(ElasticClient client, JsonSerializerSettings serializerSettings = null)
        {
            _client = client;
            SerializerSettings = serializerSettings ?? JsonHelpers.CreateSerializerSettings();
        }

        /// <summary>
        /// Create index from tenantId and Type
        /// </summary>
        /// <param name="tenantId">TenantId</param>
        /// <param name="shards">number of index shards</param>
        /// <param name="replicas">number of replicas</param>
        /// <param name="typeMapping">typeMapping</param>
        /// <typeparam name="T">Type of T</typeparam>
        /// <returns>Acknowledgment</returns>
        public Acknowledgment CreateIndex(TypeMapping<T> typeMapping, int shards = 3, int replicas = 2)
        {
            var indexName = ServerHelpers.CreateIndexName<T>(_client.TenantId);
            return CreateIndex(indexName, typeMapping, shards, replicas);
        }

        /// <summary>
        /// Create index with the given name
        /// </summary>
        /// <param name="name">name of the index</param>
        /// <param name="shards">number of index shards</param>
        /// <param name="replicas">number of replicas</param>
        /// <param name="typeMapping">typeMapping</param>
        /// <typeparam name="T">Type of T</typeparam>
        /// <returns>Acknowledgment</returns>
        public Acknowledgment CreateIndex(string name, TypeMapping<T> typeMapping, int shards = 3, int replicas = 2)
        {
            name = name.ToLower();
            var shardSettings = new ShardsSettings(shards, replicas);

            var post = typeMapping != null
                ? JsonConvert.SerializeObject(new { settings = shardSettings.Settings, mappings = typeMapping })
                : JsonConvert.SerializeObject(shardSettings);

            var response = _client.Client.DoRequest<StringResponse>(HttpMethod.PUT, name, post);

            var acknowledgment = new Acknowledgment();
            if (response.Success)
            {
                acknowledgment = JsonConvert.DeserializeObject<Acknowledgment>(response.Body);
            }

            return acknowledgment;
        }

        /// <summary>
        /// Create index with the given name
        /// </summary>
        /// <param name="name">name of the index</param>
        /// <param name="shards">number of index shards</param>
        /// <param name="replicas">number of replicas</param>
        /// <returns>Acknowledgment</returns>
        public Acknowledgment CreateIndex(string name, int shards = 3, int replicas = 2)
        {
            var shardSettings = new ShardsSettings(shards, replicas);
            var post = JsonConvert.SerializeObject(shardSettings);

            var response = _client.Client.DoRequest<StringResponse>(HttpMethod.PUT, name, post);

            var acknowledgment = new Acknowledgment();
            if (response.Success)
            {
                acknowledgment = JsonConvert.DeserializeObject<Acknowledgment>(response.Body);
            }

            return acknowledgment;
        }

        /// <summary>
        /// Delete an index with the given name
        /// </summary>
        /// <param name="name">index name</param>
        /// <returns>Acknowledgment</returns>
        public Acknowledgment DeleteIndex(string name)
        {
            var response = _client.Client.DoRequest<StringResponse>(HttpMethod.DELETE, name);
            var acknowledgment = new Acknowledgment();
            if (response.Success)
            {
                acknowledgment = JsonConvert.DeserializeObject<Acknowledgment>(response.Body);
            }

            return acknowledgment;
        }

        /// <summary>
        /// Gets or sets the serializer settings.
        /// </summary>
        /// <value>
        /// The serializer settings.
        /// </value>
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <summary>
        /// Indexes the specified object.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>StringResponse</returns>
        public StringResponse Index(T obj, Guid? id = null, string index = null)
        {
            return Index<T>(obj, id, index);
        }

        /// <summary>
        /// Indexes the specified object.
        /// </summary>
        /// <typeparam name="T2">Type</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>StringResponse</returns>
        public StringResponse Index<T2>(T2 obj, Guid? id = null, string index = null) where T2 : class
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            var body = JsonConvert.SerializeObject(obj, SerializerSettings);
            var type = obj.GetType().FullName; // TODO : Remove if test passes
            return _client.Client.Index<StringResponse>(ServerHelpers.CreateIndexName<T2>(_client.TenantId), id.ToString(), body);
        }

        /// <summary>
        /// Indexes the asynchronous.
        /// </summary>
        /// <typeparam name="T2">Type</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>StringResponse</returns>
        public async Task<StringResponse> IndexAsync<T2>(T2 obj, string id, bool debug = false, TypeMapping<T> tmap = null) where T2 : class
        {
            var type = obj.GetType().FullName;
            var body = JsonConvert.SerializeObject(obj, SerializerSettings);
            var typeFullName = obj.GetType().FullName;
            if (typeFullName == null)
            {
                return new StringResponse();
            }

            var indexName = ServerHelpers.CreateIndexName<T2>(_client.TenantId);
            if (_client.IndexExists(indexName))
            {
                return await _client.Client.IndexAsync<StringResponse>(indexName, id, body);
            }

            Logging.WriteDebug($"IndexBulk - Creating {indexName}", debug);
            CreateIndex(indexName, tmap);
            return await _client.Client.IndexAsync<StringResponse>(indexName, id, body);
        }

        /// <summary>
        /// Indexes the asynchronous.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>StringResponse</returns>
        public async Task<StringResponse> IndexAsync(T obj, Guid? id = null, bool debug = false, TypeMapping<T> tmap = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }
            return await IndexAsync(obj, id.ToString(), debug, tmap);
        }

        /// <summary>
        /// Indexes the json asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>StringResponse</returns>
        public async Task<StringResponse> IndexJsonAsync(string obj, string typeFullName, Guid? id = null,
            bool debug = false)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            if (typeFullName == null)
            {
                return new StringResponse();
            }

            var indexName = ServerHelpers.CreateIndexName(_client.TenantId, typeFullName);
            if (_client.IndexExists(indexName))
            {
                return await _client.Client.IndexAsync<StringResponse>(indexName, id.ToString(), obj);
            }

            Logging.WriteDebug($"IndexBulk - Creating {indexName}", debug);
            CreateIndex(indexName);
            return await _client.Client.IndexAsync<StringResponse>(indexName, id.ToString(), obj);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>DocResponse</returns>
        public async Task<DocResponse> DeleteAsync(Guid id, string index = null)
        {
            return await DeleteAsync<T>(id, index);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T2">Type</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>DocResponse</returns>
        public async Task<DocResponse> DeleteAsync<T2>(Guid id, string index = null)
            where T2 : class
        {
            var type = typeof(T).FullName;
            if (type == null)
            {
                return new DocResponse {Result = "Type was not found."};
            }

            if (string.IsNullOrEmpty(index))
            {
                index = ServerHelpers.CreateIndexName<T2>(_client.TenantId);
            }

            var response = await _client.Client.DeleteAsync<StringResponse>(index, id.ToString());

            return JsonConvert.DeserializeObject<DocResponse>(response.Body);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>DocResponse</returns>
        public DocResponse Delete(Guid id, string index = null)
        {
            return Delete<T>(id, index);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <typeparam name="T2">Type</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>DocResponse</returns>
        public DocResponse Delete<T2>(Guid id, string index = null)
            where T2 : class
        {
            var type = typeof(T).FullName;
            if (type == null)
            {
                return new DocResponse {Result = "Type was not found."};
            }

            if (string.IsNullOrEmpty(index))
            {
                index = ServerHelpers.CreateIndexName<T2>(_client.TenantId);
            }

            var response = _client.Client.Delete<StringResponse>(index, id.ToString());

            return JsonConvert.DeserializeObject<DocResponse>(response.Body);
        }

        /// <summary>
        /// Deletes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>DocResponse</returns>
        public DocResponse Delete(Type type, Guid id, string index = null)
        {
            if (type == null || type.FullName == null)
            {
                return new DocResponse {Result = "Type was not found."};
            }

            if (string.IsNullOrEmpty(index))
            {
                index = ServerHelpers.CreateIndexName(_client.TenantId, type.FullName);
            }

            var t = type.FullName;
            var response = _client.Client.Delete<StringResponse>(index, id.ToString());
            return JsonConvert.DeserializeObject<DocResponse>(response.Body);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>DocResponse</returns>
        public async Task<DocResponse> DeleteAsync(Type type, Guid id, string index = null)
        {
            if (type == null || type.FullName == null)
            {
                return new DocResponse {Result = "Type was not found."};
            }

            if (string.IsNullOrEmpty(index))
            {
                index = ServerHelpers.CreateIndexName(_client.TenantId, type.FullName);
            }

            var t = type.FullName;
            var response = await _client.Client.DeleteAsync<StringResponse>(index, id.ToString());
            return JsonConvert.DeserializeObject<DocResponse>(response.Body);
        }

        /// <summary>
        /// Indexes a collection of objects.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="idExpression">The identifier expression.</param>
        /// <param name="bulkSize">Size of the bulk.</param>
        /// <param name="typeExpression">The type expression.</param>
        /// <param name="continueOnError">if set to <c>true</c> [continue on error].</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>List&lt;BulkResponse&gt;</returns>
        public List<BulkResponse> IndexBulk(
            IEnumerable<T> objects,
            Expression<Func<T, string>> idExpression,
            int bulkSize,
            Expression<Func<T, Type>> typeExpression = null,
            bool continueOnError = false,
            bool debug = false)
        {
            return IndexBulk<T>(objects, idExpression,bulkSize,typeExpression,continueOnError,debug);
        }

        /// <summary>
        /// Indexes a collection of objects.
        /// </summary>
        /// <typeparam name="T2">Type</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="idExpression">The identifier expression.</param>
        /// <param name="bulkSize">Size of the bulk.</param>
        /// <param name="typeExpression">The type expression.</param>
        /// <param name="continueOnError">if set to <c>true</c> [continue on error].</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>List&lt;BulkResponse&gt;</returns>
        public List<BulkResponse> IndexBulk<T2>(
            IEnumerable<T2> objects,
            Expression<Func<T2, string>> idExpression,
            int bulkSize,
            Expression<Func<T2, Type>> typeExpression = null,
            bool continueOnError = false,
            bool debug = false)
            where T2 : class
        {
            var output = new List<BulkResponse>();
            var typeGroups = objects.GroupBy(typeExpression?.Compile() ?? (x => x.GetType()));
            foreach (var typeGroup in typeGroups)
            {
                var typeFullName = typeGroup.Key.FullName;
                var enumerable = typeGroup.ToArray();
                if (typeFullName == null)
                {
                    continue;
                }

                var indexName = ServerHelpers.CreateIndexName<T2>(_client.TenantId);
                Logging.WriteDebug($"IndexBulk - {indexName}", debug);
                if (!_client.IndexExists(indexName))
                {
                    Logging.WriteDebug($"IndexBulk - Creating {indexName}", debug);
                    CreateIndex(indexName);
                }

                var objectsCount = enumerable.Length;
                var bulkCount = (objectsCount / bulkSize) + (objectsCount % bulkSize != 0 ? 1 : 0);
                Logging.WriteDebug($"IndexBulk - {typeFullName.ToLower()} - BulkCount {bulkCount}", debug);
                var func = idExpression.Compile();

                for (var bulkNo = 0; bulkNo < bulkCount; bulkNo++)
                {
                    output.Add(IndexBulk(indexName, func, enumerable.Skip(bulkNo * bulkSize).Take(bulkSize),
                        typeExpression, continueOnError));
                    Logging.WriteDebug($"IndexBulk - {typeFullName.ToLower()} - Batch:{bulkNo}/{bulkCount}", debug);
                }
            }

            return output;
        }

        /// <summary>
        /// Indexes a collection of objects async.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="idExpression">The identifier expression.</param>
        /// <param name="bulkSize">Size of the bulk.</param>
        /// <param name="typeExpression">The type expression.</param>
        /// <param name="continueOnError">if set to <c>true</c> [continue on error].</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>List&lt;BulkResponse&gt;</returns>
        public async Task<List<BulkResponse>> IndexBulkAsync(
            IEnumerable<T> objects,
            Expression<Func<T, string>> idExpression,
            int bulkSize,
            Expression<Func<T, Type>> typeExpression = null,
            bool continueOnError = false,
            bool debug = false)
        {
            return await IndexBulkAsync<T>(objects, idExpression,bulkSize,typeExpression,continueOnError,debug);
        }

        /// <summary>
        /// Indexes a collection of objects async.
        /// </summary>
        /// <typeparam name="T2">Type</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="idExpression">The identifier expression.</param>
        /// <param name="bulkSize">Size of the bulk.</param>
        /// <param name="typeExpression">The type expression.</param>
        /// <param name="continueOnError">if set to <c>true</c> [continue on error].</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>List&lt;BulkResponse&gt;</returns>
        public async Task<List<BulkResponse>> IndexBulkAsync<T2>(
            IEnumerable<T2> objects,
            Expression<Func<T2, string>> idExpression,
            int bulkSize,
            Expression<Func<T2, Type>> typeExpression = null,
            bool continueOnError = false,
            bool debug = false)
            where T2 : class
        {
            var output = new List<BulkResponse>();
            var typeGroups = objects.GroupBy(typeExpression?.Compile() ?? (x => x.GetType()));
            foreach (var typeGroup in typeGroups)
            {
                var typeFullName = typeGroup.Key.FullName;
                var enumerable = typeGroup.ToArray();
                if (typeFullName == null)
                {
                    continue;
                }

                var indexName = ServerHelpers.CreateIndexName<T2>(_client.TenantId);
                Logging.WriteDebug($"IndexBulk - {indexName}", debug);
                if (!_client.IndexExists(indexName))
                {
                    Logging.WriteDebug($"IndexBulk - Creating {indexName}", debug);
                    CreateIndex(indexName);
                }

                var objectsCount = enumerable.Length;
                var bulkCount = (objectsCount / bulkSize) + (objectsCount % bulkSize != 0 ? 1 : 0);
                Logging.WriteDebug($"IndexBulk - {typeFullName.ToLower()} - BulkCount {bulkCount}", debug);
                var func = idExpression.Compile();

                for (var bulkNo = 0; bulkNo < bulkCount; bulkNo++)
                {
                    output.Add(await IndexBulkAsync(indexName, func, enumerable.Skip(bulkNo * bulkSize).Take(bulkSize),
                        typeExpression, continueOnError));
                    Logging.WriteDebug($"IndexBulk - {typeFullName.ToLower()} - Batch:{bulkNo}/{bulkCount}", debug);
                }
            }

            return output;
        }

        private static BulkResponse HandleResponse(ElasticsearchResponse<string> response, bool continueOnError = false)
        {
            if (response.Success)
            {
                return JsonConvert.DeserializeObject<BulkResponse>(response.Body);
            }

            if (continueOnError)
            {
                return new BulkResponse
                {
                    Errors = true,
                    OriginalException = response.OriginalException.Message,
                    Body = response.Body
                };
            }

            throw response.OriginalException;
        }

        private async Task<BulkResponse> IndexBulkAsync<T2>(string useIndex, Func<T2, string> func, IEnumerable<T2> bulk,
            Expression<Func<T2, Type>> typeExpression, bool continueOnError = false)
            where T2 : class
        {
            var bulkArray = bulk as T2[] ?? bulk.ToArray();
            var s = CreateBulkFrom(useIndex, func, bulkArray, typeExpression);
            var response =
                await _client.Client.DoRequestAsync<StringResponse>(HttpMethod.POST, "_bulk", CancellationToken.None,
                    s);
            if (response.HttpStatusCode != 413 || !continueOnError)
            {
                return HandleResponse(response, continueOnError);
            }

            var enumerable2 = bulk as T2[] ?? bulkArray.ToArray();
            var bulkSize = enumerable2.Length / 10;
            for (var bulkNo = 0; bulkNo < 11; bulkNo++)
            {
                await IndexBulkAsync(useIndex, func, enumerable2.Skip(bulkNo * bulkSize).Take(bulkSize), typeExpression,
                    true);
            }

            return HandleResponse(response, true);
        }

        private BulkResponse IndexBulk<T2>(string useIndex, Func<T2, string> func, IEnumerable<T2> bulk,
            Expression<Func<T2, Type>> typeExpression, bool continueOnError = false)
            where T2 : class
        {
            var bulkArray = bulk as T2[] ?? bulk.ToArray();
            var s = CreateBulkFrom(useIndex, func, bulkArray, typeExpression);
            var response = _client.Client.DoRequest<StringResponse>(HttpMethod.POST, "_bulk", s);
            if (response.HttpStatusCode != 413 || !continueOnError)
            {
                return HandleResponse(response, continueOnError);
            }

            var enumerable2 = bulk as T2[] ?? bulkArray.ToArray();
            var bulkSize = enumerable2.Length / 10;
            for (var bulkNo = 0; bulkNo < 11; bulkNo++)
            {
                IndexBulk(useIndex, func, enumerable2.Skip(bulkNo * bulkSize).Take(bulkSize), typeExpression, true);
            }

            return HandleResponse(response, true);
        }

        private string CreateBulkFrom<T2>(string useIndex, Func<T2, string> func, IEnumerable<T2> bulk,
            Expression<Func<T2, Type>> typeExpression)
            where T2 : class
        {
            var currentBulk = GetBulk(useIndex, func, bulk, typeExpression);
            var s = $"{string.Join("\n", currentBulk)}\n";
            return s;
        }

        private IEnumerable<string> GetBulk<T2>(string useIndex, Func<T2, string> func, IEnumerable<T2> bulk,
            Expression<Func<T2, Type>> typeExpression)
            where T2 : class
        {
            if (typeExpression == null)
            {
                typeExpression = arg => arg.GetType();
            }

            return bulk.Select(doc => new BulkOperation<T2>(
                    new BulkAction(func(doc), useIndex, typeExpression.Compile().Invoke(doc).FullName), doc))
                .Select(o => JsonConvert.SerializeObject(o.Item1) + "\n" +
                             JsonConvert.SerializeObject(o.Item2, SerializerSettings));
        }
    }
}