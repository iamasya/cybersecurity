using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab8._1
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
                Flags = CspProviderFlags.UseMachineKeyStore, //Рівень пристрою 
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };

            using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            }
        }

        public static byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] cypherBytes;

            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                //rsa.ImportParameters(_publicKey); 
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                cypherBytes = rsa.Encrypt(dataToEncrypt, true);
            }

            return cypherBytes;
        }

        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,

            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
            }
            return plainBytes;
        }

        static void Main(string[] args)
        {
            AssignNewKey("RadyvonenkoAsya.xml");
            string message = "Hi! My name is Asya!";
            string keyname = "usersmessage.xml";
            string umessage = "messagetoyou.dat"; //файл, який буде розшифровано
            //Console.WriteLine(File.ReadAllBytes(umessage));

            Console.WriteLine("Choose your command: ");
            Console.WriteLine("d - decryption of a message");
            Console.WriteLine("e - encryption of a message");
            Console.WriteLine("");
            Console.WriteLine("Enter your command -> ");
            string c = Convert.ToString(Console.ReadLine());

            if (c == "e")
            {
                //decryption of a message
                var encrypted = EncryptData(keyname, Encoding.Unicode.GetBytes(message));
                File.WriteAllBytes("mynewmessage.dat", encrypted);
                Console.WriteLine("The message " + message + " was encrypted. Check your folder! ");
                Console.WriteLine();
            }
            else
            {
                //encryption of a message
                var encryptedmessage = File.ReadAllBytes(umessage);
                var decrypted = DecryptData(encryptedmessage);
                Console.WriteLine("Decrypted message = " + Encoding.UTF8.GetString(decrypted));
            }

            Console.ReadLine();
        }


    }

}

