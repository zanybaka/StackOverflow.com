using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex  pattern = new Regex(@"^[0-9]{1,6}\.txt$", RegexOptions.IgnoreCase);
            string path    = @"c:\Temp\1\";

            string[] files = Directory.GetFiles(path, "*.txt")
                .Where(path => pattern.IsMatch(Path.GetFileName(path))).ToArray();

            foreach (var myFile in files)
            {
                try
                {
                    Console.WriteLine("Filename:" + myFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }
    }
}