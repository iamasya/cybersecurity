using System;
using System.Security.Cryptography;

namespace pz1_1
{
    class Program
    {
        public static byte[] GenerateRandomNumber(int length)
        {
            using (var rndNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                rndNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                string randomNumber = Convert.ToBase64String(GenerateRandomNumber(32));                
                Console.WriteLine(randomNumber);
                
            }
            
            Console.ReadLine();
        }
    }
}
