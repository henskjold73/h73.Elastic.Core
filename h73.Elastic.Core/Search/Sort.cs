using System.Collections.Generic;
using h73.Elastic.Core.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace h73.Elastic.Core.Search
{

    public class Sort : Dictionary<string, string>
    {
        public void AddSorting(string fieldName, string direction)
        {
            this[fieldName] = direction;
        }
    }
}
