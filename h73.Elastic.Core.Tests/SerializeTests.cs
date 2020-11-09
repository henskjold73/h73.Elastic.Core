using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using h73.Elastic.Core.Search;
using h73.Elastic.Core.Search.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace h73.Elastic.Core.Tests
{
    [TestClass]
    public class SerializeTests
    {
        [TestMethod]
        public void Serialize_Deserialize_Type()
        {
            var source = ObjectToByteArray(new TypeQuery());
            var source1 = ObjectToByteArray(new TypeQueryValue());
            var sObj = ObjectToByteArray(source);
            var sObj1 = ObjectToByteArray(source1);
            var qObj = ByteArrayToObject(sObj);
        }

        [TestMethod]
        public void Serialize_Deserialize_Terms()
        {
            var source = ObjectToByteArray(new TermsQuery());
            var source1 = ObjectToByteArray(new TermsQueryValue());
            var sObj = ObjectToByteArray(source);
            var sObj1 = ObjectToByteArray(source1);
            var qObj = ByteArrayToObject(sObj);
        }

        [TestMethod]
        public void Serialize_Deserialize_Source()
        {
            var source = ObjectToByteArray(new Source());
            var sObj = ObjectToByteArray(source);
            var qObj = ByteArrayToObject(sObj);
        }

        [TestMethod]
        public void Serialize_Deserialize_Exists()
        {
            var source = ObjectToByteArray(new ExistsQuery());
            var sObj = ObjectToByteArray(source);
            var qObj = ByteArrayToObject(sObj);
        }

        [TestMethod]
        public void Serialize_Deserialize_Match()
        {
            var source = ObjectToByteArray(new MatchQuery<object>());
            var sObj = ObjectToByteArray(source);
            var qObj = ByteArrayToObject(sObj);
        }

        [TestMethod]
        public void Serialize_Deserialize_BooleanQuery()
        {
            var source = ObjectToByteArray(new BooleanQuery());
            var sObj = ObjectToByteArray(source);
            var qObj = ByteArrayToObject(sObj);
        }

        // Convert an object to a byte array
        private byte[] ObjectToByteArray(Object obj)
        {
            if(obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object) binForm.Deserialize(memStream);

            return obj;
        }
    }
}