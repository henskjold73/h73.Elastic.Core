using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace h73.Elastic.Core.Search
{
    [Serializable]
    public class Source
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, TypeNameHandling = TypeNameHandling.None)]
        public object Output
        {
            get
            {
                if (!IsValid())
                {
                    return null;
                }

                if (Values != null)
                {
                    return Values;
                }

                if (Includes != null && Excludes != null && Includes.Any() && Excludes.Any())
                {
                    return new { includes = Includes, excludes = Excludes };
                }

                if (Includes != null && Includes.Any())
                {
                    return new { includes = Includes };
                }

                if (Excludes != null && Excludes.Any())
                {
                    return new { excludes = Excludes };
                }

                return null;
            }
        }

        public string[] Values { get; set; }

        public string[] Includes { get; set; }

        public string[] Excludes { get; set; }

        public bool IsValid()
        {
            var t = GetType();
            var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int count = 0;

            foreach (var prop in properties.Where(x => x.Name != nameof(Output)))
            {
                var value = prop.GetValue(this, null);

                if (value == null)
                {
                    continue;
                }

                count++;
            }

            return count == 1 || (Includes != null && Excludes != null && count == 2);
        }
    }
}
