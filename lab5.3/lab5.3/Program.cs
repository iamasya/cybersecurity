using System;
using System.Security.Cryptography;
using System.Text;

namespace lab5._3
{
    public class SaltedHash
    {
        public static byte[] GenerateSalt()
        {
            const int saltLength = 32;
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(toBeHashed, salt));
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            const string password = "V3ryC0mpl3xP455w0rd";
            byte[] salt = SaltedHash.GenerateSalt();
            Console.WriteLine("Password : " + password);
            Console.WriteLine("Salt = " +
                   Convert.ToBase64String(salt));
            Console.WriteLine();
            var hashedPassword1 = SaltedHash.HashPasswordWithSalt(
                Encoding.UTF8.GetBytes(password),
                salt);
            Console.WriteLine("Hashed Password = " +
                       Convert.ToBase64String(hashedPassword1));
            Console.ReadLine();
        }
    }
}
