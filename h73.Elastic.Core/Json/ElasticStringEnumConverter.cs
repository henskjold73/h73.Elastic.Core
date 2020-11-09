using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace h73.Elastic.Core.Json
{
    public class ElasticStringEnumConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var @enum = (Enum) value;
                var enumValue = @enum.ToString();
                    writer.WriteValue(enumValue);
            }
        }
    }
}