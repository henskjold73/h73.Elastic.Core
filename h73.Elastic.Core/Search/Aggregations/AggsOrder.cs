using System.Collections.Generic;
using h73.Elastic.Core.Enums;

namespace h73.Elastic.Core.Search.Aggregations
{
    public class AggsOrder : Dictionary<string,string>
    {
        public AggsOrder(AggsOrderBy aggsOrderBy, AggsOrderDirection aggsOrderDirection)
        {
            this[$"_{aggsOrderBy.ToString().ToLower()}"] = aggsOrderDirection.ToString().ToLower();
        }
        public AggsOrder(string aggsOrderBy, AggsOrderDirection aggsOrderDirection)
        {
            this[aggsOrderBy] = aggsOrderDirection.ToString().ToLower();
        }
    }
}