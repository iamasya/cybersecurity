using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace lab5._4
{
    class Program
    {
        static void Main(string[] args)
        {
            const string passwordToHash = "mit-21";
            HashPassword(passwordToHash, 220000); //перше значення = номер варіанта * 10'000; мій номер у списку: 22
            HashPassword(passwordToHash, 270000); //крок = 50'000
            HashPassword(passwordToHash, 320000);
            HashPassword(passwordToHash, 370000);
            HashPassword(passwordToHash, 420000);
            HashPassword(passwordToHash, 470000);
            HashPassword(passwordToHash, 520000);
            HashPassword(passwordToHash, 570000);
            HashPassword(passwordToHash, 620000);
            HashPassword(passwordToHash, 670000);
            Console.ReadLine();
        }

        private static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();

            var hashedPasswordmd5 = PBKDF2.HashPasswordMD5(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            var hashedPasswordsha1 = PBKDF2.HashPasswordSHA1(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            var hashedPasswordsha256 = PBKDF2.HashPasswordSHA256(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            var hashedPasswordsha384 = PBKDF2.HashPasswordSHA384(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            var hashedPasswordsha512 = PBKDF2.HashPasswordSHA512(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);

            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("MD5 Hashed Password : " + Convert.ToBase64String(hashedPasswordmd5));
            Console.WriteLine("SHA1 Hashed Password : " + Convert.ToBase64String(hashedPasswordsha1));
            Console.WriteLine("SHA256 Hashed Password : " + Convert.ToBase64String(hashedPasswordsha256));
            Console.WriteLine("SHA384 Hashed Password : " + Convert.ToBase64String(hashedPasswordsha384));
            Console.WriteLine("SHA512 Hashed Password : " + Convert.ToBase64String(hashedPasswordsha512));
            Console.WriteLine("Iterations <" + numberOfRounds + ">  Elapsed Time: " + sw.ElapsedMilliseconds + "ms");

        }
    }

    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static byte[] HashPasswordMD5(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.MD5))
            {
                return rfc2898.GetBytes(16);//Коли GetBytes викликається для вилучення
                                            //хеш, ми отримуємо лише 16 байт; це тому, що базова функція хешування
                                            //всередині Rfc2898DeriveBytes використовує алгоритм MD-5 для створення хешу. MD - 5
                                            //створює 128 - бітний або 16 - байтний хеш - код.
            }
        }

        public static byte[] HashPasswordSHA1(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA1))
            {
                return rfc2898.GetBytes(20); //SHA - 1 створює 160 - бітний або 20 - байтний хеш - код.
            }
        }

        public static byte[] HashPasswordSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);//SHA - 256 створює 256 - бітний або 32 - байтний хеш - код.
            }
        }

        public static byte[] HashPasswordSHA384(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA384))
            {
                return rfc2898.GetBytes(48);//SHA - 384 створює 384 - бітний або 48 - байтний хеш - код.
            }
        }

        public static byte[] HashPasswordSHA512(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA512))
            {
                return rfc2898.GetBytes(64);//SHA - 512 створює 512 - бітний або 64 - байтний хеш - код.
            }
        }
    }
}
