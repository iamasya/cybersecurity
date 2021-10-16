using System;

namespace pz1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(1235);
            random.Next(1, 10);
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(random.Next(1, 10));
            }
        }
    }
}
