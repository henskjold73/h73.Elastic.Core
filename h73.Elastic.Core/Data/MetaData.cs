using System.Collections.Generic;

namespace h73.Elastic.Core.Data
{
    public class MetaData<T> : Dictionary<string, object> where T : new()
    {
        public static implicit operator MetaData<T>(T obj)
        {
            var output = new MetaData<T>();
            foreach (var piInfo in obj.GetType().GetProperties())
            {
                output[piInfo.Name] = piInfo.GetValue(obj);
            }

            return output;
        }

        public static implicit operator T(MetaData<T> obj)
        {
            var output = new T();
            foreach (var piInfo in output.GetType().GetProperties())
            {
                piInfo.SetValue(output, obj[piInfo.Name]);
            }

            return output;
        }
    }
}