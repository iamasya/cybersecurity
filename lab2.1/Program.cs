using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace lab2._1
{
    class Program
    {
        static void Main()
        {
            
            string text;
            var fileStream = new FileStream(@"D:\university\2\1 semester\cybersecurity\lab2\test.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            Console.WriteLine(text);

            
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] key = new byte[data.Length];
            var rnd = new RNGCryptoServiceProvider();
            rnd.GetBytes(key);
            byte[] encryptedMessage = new byte[data.Length];
            Console.Write("Key: ");
            Console.WriteLine(Encoding.UTF8.GetString(key));
            Console.WriteLine("Encrypted message:");
            for (byte i = 0; i < data.Length; i++ )
            {
                encryptedMessage[i] = (byte)(data[i] ^ key[i]);
            }
            Console.Write(Encoding.UTF8.GetString(encryptedMessage));
            Console.WriteLine();
            byte[] decryptedMessage = new byte[data.Length];
            Console.WriteLine("Decrypted message:");
            for (byte i = 0; i < data.Length; i++)
            {
                decryptedMessage[i] = (byte)(encryptedMessage[i] ^ key[i]);
                
                //Console.Write(decryptedMessage[i]);

            }
            Console.Write(Encoding.UTF8.GetString(decryptedMessage));

            string fileName = @"D:\university\2\1 semester\cybersecurity\lab2\text.dat";
            // Check if file already exists. If yes, delete it.     
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            using (FileStream fs = File.Create(fileName))
            {
                // Add some text to file   

                fs.Write(encryptedMessage, 0 , encryptedMessage.Length);
            }
        }
    }
}
