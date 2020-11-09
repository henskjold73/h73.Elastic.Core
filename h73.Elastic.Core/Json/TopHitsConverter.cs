using System;
using System.Linq;
using System.Reflection;
using h73.Elastic.Core.Search.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace h73.Elastic.Core.Json
{
    public class TopHitsConverter : JsonConverter
    {
        private readonly Assembly _topHitsAssembly;
        private Type _topHitsType;

        public TopHitsConverter()
        {
        }

        public TopHitsConverter(Type topHitsType)
        {
            _topHitsType = topHitsType;
        }

        public TopHitsConverter(Assembly topHitsAssembly)
        {
            _topHitsAssembly = topHitsAssembly;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.None) return null;
            var jsonObject = JObject.Load(reader);
            try
            {
                var jType = jsonObject.First.First["hits"].First["_type"].Value<string>();
                if (_topHitsType == null)
                {
                    var asms = _topHitsAssembly == null ? AppDomain.CurrentDomain.GetAssemblies() : new[]{_topHitsAssembly} ;
                    foreach (var rA in asms)
                    {
                        var rType = rA.GetTypes().FirstOrDefault(t => t.FullName == jType);
                        if (rType == null) continue;
                        _topHitsType = rType;
                        break;
                    }
                }

                var jsonString = jsonObject.First.ToString();
                var template = typeof(TopHitsResult<>);
                if (_topHitsType == null) _topHitsType = typeof(object);
                var genericType = template.MakeGenericType(_topHitsType);

                return JsonConvert.DeserializeObject($"{{{jsonString}}}", genericType);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.FullName != null && objectType.FullName.StartsWith("h73.Elastic.Core.Search.Results.TopHitsResult");
        }
    }
}