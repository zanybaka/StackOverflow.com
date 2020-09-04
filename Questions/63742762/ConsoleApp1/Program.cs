using System;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var entity =
                new EntityFromJson<JsonRootObject>(File.ReadAllText("Entity.json"))
                    .GetValue();
            Console.WriteLine($"Length = {entity.Array.Length}");
            Console.WriteLine($"transaction.Version = {entity.Array.First().transaction.Version}");

            var json =
                new JsonFromEntity(entity)
                    .GetValue();
            Console.WriteLine($"Json = {json}");
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }
    }
}
