using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace ConsoleApp1
{
    public class EntityFromJson<TEntity>
    {
        private readonly string _input;

        public EntityFromJson(string input)
        {
            _input = input ?? "";
        }

        public static implicit operator TEntity(EntityFromJson<TEntity> obj)
        {
            return obj.GetValue();
        }

        public TEntity GetValue()
        {
            return Create(_input);
        }

        private static TEntity Create(string json)
        {
            // opt1:
            using (var memoryStream = new MemoryStream())
            {
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                memoryStream.Write(jsonBytes, 0, jsonBytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(
                    memoryStream,
                    Encoding.UTF8,
                    XmlDictionaryReaderQuotas.Max,
                    null))
                {
                    var     serializer = new DataContractJsonSerializer(typeof(TEntity));
                    TEntity entity     = (TEntity)serializer.ReadObject(jsonReader);
                    return entity;
                }
            }

            // opt2: JavaScriptSerializer
            // opt3:
            // var jsonReader = JsonReaderWriterFactory.CreateJsonReader(
            //    Encoding.UTF8.GetBytes(sb.ToString()),
            //    new System.Xml.XmlDictionaryReaderQuotas());
            // XElement root = XElement.Load(jsonReader);
            // opt4: System.Web.Helpers
            // dynamic json = System.Web.Helpers.Json.Decode(@"{ ""Name"": ""Jon Smith"", ""Address"": { ""City"": ""New York"", ""State"": ""NY"" }, ""Age"": 42 }");
            // var entity = Activator.CreateInstance<TEntity>();
            // opt5: JsonConvert (JSON.NET or Newtonsoft.Json)
        }
    }
}