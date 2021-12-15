using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;  

namespace lab7._2
{
    class Program
    {
        private readonly static string CspContainerName = "RsaContainer";

        //  Cryptographic Service Provider 
        public static void AssignNewKey(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                //Flags = CspProviderFlags.UseMachineKeyStore, //Рівень пристрою 
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };

            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }

        public static byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] cypherBytes;

            /*var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            */

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                //rsa.ImportParameters(_publicKey); 
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                cypherBytes = rsa.Encrypt(dataToEncrypt, false);
            }

            return cypherBytes;
        }

        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainTextBytes;

            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                //Flags = CspProviderFlags.UseMachineKeyStore
            };


            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true; 

                plainTextBytes = rsa.Decrypt(dataToDecrypt, false);
            }

            return plainTextBytes;
        }

        static void Main(string[] args)
        {
            string original = "Very very complex text to encrypt";
            AssignNewKey("publickeys.xml");
           
            var encrypted = EncryptData("publickeys.xml", Encoding.Unicode.GetBytes(original));
            var decrypted = DecryptData(encrypted);
           
            Console.WriteLine();
            Console.WriteLine("In Memory Key");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Text = " + Encoding.Unicode.GetString(decrypted));
            Console.ReadLine();
        }
    }
}
