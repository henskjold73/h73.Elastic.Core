using System;
using System.Linq;
using System.Reflection;
using h73.Elastic.Core.Search.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace h73.Elastic.Core.Json
{
    public class FilteredAggregationConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.None) return null;
            var converters = serializer.Converters.Where(x => x.GetType() != typeof(FilteredAggregationConverter));
            var jsonObject = JObject.Load(reader);
            var jsonConverters = converters.ToList();
            var obj = JsonConvert.DeserializeObject<Aggregations>(jsonObject.ToString(), jsonConverters.ToArray());
            if (!obj.Keys.Any(key=>key.StartsWith("agg_filter"))) return obj;
            var output = new Aggregations();
            foreach (var child in jsonObject.Children().ToList())
            {
                var name = ((JProperty) child).Name;
                if (name.StartsWith("agg_filter"))
                {
                    var propNames = typeof(Aggregation).GetProperties().Select(x => x.GetCustomAttribute<JsonPropertyAttribute>().PropertyName).ToList();
                    foreach (var jProp in child.Children().First().Children())
                    {
                        var jPropName = ((JProperty)jProp).Name;
                        if (!propNames.Contains(jPropName))
                        {
                            output[jPropName] = JsonConvert.DeserializeObject<Aggregation>(jProp.Children().First().ToString(), jsonConverters.ToArray());
                        }
                    }
                }
                else
                {
                    output[name] = JsonConvert.DeserializeObject<Aggregation>(child.First().ToString(), jsonConverters.ToArray());
                }
            }

            return output;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Aggregations);
        }
    }
}