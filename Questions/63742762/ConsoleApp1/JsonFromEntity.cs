using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ConsoleApp1
{
    public class JsonFromEntity
    {
        private readonly object _input;

        public JsonFromEntity(object input)
        {
            _input = input;
        }

        public static implicit operator string(JsonFromEntity obj)
        {
            return obj.GetValue();
        }

        public string GetValue()
        {
            return Create(_input);
        }

        private static string Create(object entity)
        {
            var serializer = new DataContractJsonSerializer(entity.GetType());

            using (var stream = new MemoryStream())
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8))
                {
                    serializer.WriteObject(writer, entity);
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}