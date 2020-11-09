using System.Collections.Generic;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// match queries accept text/numerics/dates, analyzes them, and constructs a query.
    /// </summary>
    /// <seealso cref="object" />
    public class MatchQueryValue : Dictionary<string, object>
    {
    }
}
