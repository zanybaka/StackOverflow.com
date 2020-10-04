using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = " text\r\n\r\n\r\n  spaces    ok  \r\n  yeap.  \r\n";
            string expected = "text\r\nspaces ok\r\nyeap.";

            string result = string.Join("\r\n", input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(x => Regex.Replace(x.Trim(), @"\s+", " ")));
            // string result = Regex.Replace(input, @"[\r\n]+", "\n");
            // result = Regex.Replace(result, @"(?:(?![\r\n])\s)+", " ");
            Console.WriteLine($"|{expected}|");
            Console.WriteLine($"|{result}|");
            Console.WriteLine();
            Console.WriteLine(result == expected);
        }
    }
}
