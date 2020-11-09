using System.Collections.Generic;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// The term query finds documents that contain the exact term specified in the inverted index.
    /// </summary>
    /// <seealso cref="string" />
    public class TermQueryValue : Dictionary<string, string>
    {
    }
}
