namespace h73.Elastic.Core.External.Tests.Support
{
    public interface IExternalClass
    {
        string Name { get; set; }
        string Country { get; set; }
        int Age { get; set; }
        double Chance { get; set; }
        GeoClass Geo { get; set; }
    }
}