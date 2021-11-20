using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp3_3
{
    class Program
    {
        static byte[] ComputeHashSHA256(byte[] dataForHash)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(dataForHash);
            }
        }

        public static byte[] ComputeHmacsha256(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }
        static void Main(string[] args)
        {
            string message = "Fundamentals of information security";
            string key = "64pA6cJle1";

            var hashedKey = ComputeHashSHA256(Encoding.Unicode.GetBytes(key));

            var hashedMessage = ComputeHmacsha256(Encoding.Unicode.GetBytes(message), hashedKey);

            Console.WriteLine($"My message: {message}");
            Console.WriteLine($"Hashed message: {Convert.ToBase64String(hashedMessage)}");

            Console.WriteLine("Loading...");

            string key1 = "64pA6cJle1";

            var hashedKey1 = ComputeHashSHA256(Encoding.Unicode.GetBytes(key1));

            var hashedMessage1 = ComputeHmacsha256(Encoding.Unicode.GetBytes(message), hashedKey1);

            if (Convert.ToBase64String(hashedMessage) == Convert.ToBase64String(hashedMessage1))
            {
                Console.WriteLine("Message is true!");
            }
            else
            {
                Console.WriteLine("Message is false!");
            }

        }
    }
}