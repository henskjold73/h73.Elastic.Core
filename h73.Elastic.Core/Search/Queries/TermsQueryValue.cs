using System;
using System.Collections.Generic;

namespace h73.Elastic.Core.Search.Queries
{
    /// <summary>
    /// Filters documents that have fields that match any of the provided terms (not analyzed).
    /// </summary>
    /// <seealso cref="string" />
    [Serializable]
    public class TermsQueryValue : Dictionary<string, string[]>
    {
    }
}
