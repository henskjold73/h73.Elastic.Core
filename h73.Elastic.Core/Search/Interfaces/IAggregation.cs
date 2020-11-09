using System.Collections.Generic;

namespace h73.Elastic.Core.Search.Interfaces
{
    public interface IAggregation
    {
        Dictionary<string, IAggregation> Aggregations { get; set; }

        IFilter Filter { get; set; }
    }

    public interface IStatAggregation : IAggregation
    {
      
    }
}
