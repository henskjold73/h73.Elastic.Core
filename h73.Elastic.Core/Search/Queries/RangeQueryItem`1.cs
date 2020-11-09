using System.Collections.Generic;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// RangeQueryItem to support RangeQuery
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    /// <seealso cref="RangeQueryValue{T}" />
    public class RangeQueryItem<T> : Dictionary<string, RangeQueryValue<T>>
    {
    }
}
