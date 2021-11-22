using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab6._1
{
    //DES
    class DEScipher
    {
        public static byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }

    //Triple-DES
    class TripleDEScipher
    {
        public static byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }

    //AES
    class AEScipher
    {
        public static byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string original = "Performing Symmetric Encryption";

            //DES
            var keydes = AEScipher.GenerateRandomNumber(8);
            var ivdes = AEScipher.GenerateRandomNumber(8);
            var encrypteddes = DEScipher.Encrypt(Encoding.UTF8.GetBytes(original), keydes, ivdes);
            var decrypteddes = DEScipher.Decrypt(encrypteddes, keydes, ivdes);
            var decryptedMessagedes = Encoding.UTF8.GetString(decrypteddes);
            Console.WriteLine("-------------------------");
            Console.WriteLine("DES Demonstration");
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypteddes));
            Console.WriteLine("Decrypted Text = " + decryptedMessagedes);
            Console.ReadLine();
            
            //Triple-DES
            var keytrdes = AEScipher.GenerateRandomNumber(16);
            var ivtrdes = AEScipher.GenerateRandomNumber(8);
            var encryptedtrdes = TripleDEScipher.Encrypt(Encoding.UTF8.GetBytes(original), keytrdes, ivtrdes);
            var decryptedtrdes = TripleDEScipher.Decrypt(encryptedtrdes, keytrdes, ivtrdes);
            var decryptedMessagetrdes = Encoding.UTF8.GetString(decryptedtrdes);
            Console.WriteLine("-------------------------");
            Console.WriteLine("Triple-DES Demonstration");
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " +
            Convert.ToBase64String(encryptedtrdes));
            Console.WriteLine("Decrypted Text = " + decryptedMessagetrdes);
            Console.ReadLine();

            //AES
            var keyaes = AEScipher.GenerateRandomNumber(32);
            var ivaes = AEScipher.GenerateRandomNumber(16);
            var encryptedaes = AEScipher.Encrypt(Encoding.UTF8.GetBytes(original), keyaes, ivaes);
            var decryptedaes = AEScipher.Decrypt(encryptedaes, keyaes, ivaes);
            var decryptedMessageaes = Encoding.UTF8.GetString(decryptedaes);
            Console.WriteLine("-------------------------");
            Console.WriteLine("AES Demonstration");
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " +
            Convert.ToBase64String(encryptedaes));
            Console.WriteLine("Decrypted Text = " + decryptedMessageaes);
            Console.ReadLine();
        }
    }
}
