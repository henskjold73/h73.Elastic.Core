using Elasticsearch.Net;

namespace h73.Elastic.Core.Response
{
    /// <summary>
    /// ElasticResponse
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    /// <seealso cref="Elasticsearch.Net.ElasticsearchResponse{T}" />
    public class ElasticResponse<T> : ElasticsearchResponse<T>
    {
    }
}
