using System.Collections.Generic;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class After : Dictionary<string, object>
    {
        public After(string field, object value)
        {
            this[field] = value;
        }

    }
}