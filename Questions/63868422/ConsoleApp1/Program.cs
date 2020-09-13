using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, a, b;
            n = 5; // !
            int   mi  = 2147483647, ma = 0;
            int[] num = new int[n]; // !
            for (int i = 0; i < n; i++)
            {
                num[i] = new Random((int)DateTime.Now.Ticks).Next(1, 9);
                if (num[i] > ma)
                    ma = num[i];
                if (num[i] < mi)
                    mi = num[i];
            }

            for (int i = 0; i < num.Length; i++)
            {
                Console.Write($"{num[i]} ");
            }

            Console.WriteLine();
            Console.WriteLine($"min={mi}; max={ma}");

            /* original wrong algo
            int n, a, b;
            n = 5; // !
            int ma = 0, mi = 0;
            int[] num = new int[n]; // !
            num[0] = new Random((int) DateTime.Now.Ticks).Next(1, 9);
            for (int i = 1; i < n; i++)
            {
                num[i] = new Random((int)DateTime.Now.Ticks).Next(1, 9);
                a = num[i];
                b = num[i - 1];
                if (b > a)
                    ma = b;
                if (b < a)
                    mi = b;
            }

            for (int i = 0; i < num.Length; i++)
            {
                Console.Write($"{num[i]} ");
            }

            Console.WriteLine();
            Console.WriteLine($"min={mi}; max={ma}");*/
        }
    }
}
