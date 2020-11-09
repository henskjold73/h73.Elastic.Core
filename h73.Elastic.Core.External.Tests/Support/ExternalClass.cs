using h73.Elastic.TypeMapping;
using h73.Elastic.TypeMapping.Attributes;

namespace h73.Elastic.Core.External.Tests.Support
{
    public class ExternalClass : IExternalClass
    {
        public string Name { get; set; }

        [ElasticTypeMapping(FieldTypes.Keyword)]
        public string Country { get; set; }
        
        public int Age { get; set; }
        public double Chance { get; set; }

        [ElasticTypeMapping(FieldTypes.GeoPoint)]
        public GeoClass Geo { get; set; }
    }
}