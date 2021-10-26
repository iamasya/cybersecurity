using System;
using System.IO;
using System.Text;

namespace lab2._2
{
    class Program
    {
        byte key = 0;
        static void Main(string[] args)
        {

            string text;
            var fileStream = new FileStream(@"D:\university\2\1 semester\cybersecurity\lab2\encfile5.dat", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            Console.WriteLine(text);

            byte[] encryptedMessage = Encoding.UTF8.GetBytes(text);
            // byte[] encryptedMessage = new byte[data.Length];
            Console.WriteLine("Encrypted message:");
            byte[] decryptedMessage = new byte[encryptedMessage.Length];
            bool breakLoops = false;
            int p = 256;
            for (int key1 = 0; key1 < p; key1++)
            {

                decryptedMessage[0] = (byte)(encryptedMessage[0] ^ key1);

                for (int key2 = 0; key2 < p; key2++)
                {
                    decryptedMessage[1] = (byte)(encryptedMessage[1] ^ key2);

                    for (int key3 = 0; key3 < p; key3++)
                    {
                        decryptedMessage[2] = (byte)(encryptedMessage[2] ^ key3);

                        for (int key4 = 0; key4 < p; key4++)
                        {
                            decryptedMessage[3] = (byte)(encryptedMessage[3] ^ key4);

                            for (int key5 = 0; key5 < p; key5++)
                            {
                                decryptedMessage[4] = (byte)(encryptedMessage[4] ^ key5);

                                for (int key6 = 0; key6 < p; key6++)
                                {
                                    decryptedMessage[5] = (byte)(encryptedMessage[5] ^ key6);

                                    for (int key7 = 0; key7 < p; key7++)
                                    {
                                        decryptedMessage[6] = (byte)(encryptedMessage[6] ^ key7);
                                        for (int key8 = 0; key8 < p; key8++)
                                        {
                                            decryptedMessage[7] = (byte)(encryptedMessage[7] ^ key8);
                                            //Console.WriteLine(Encoding.UTF8.GetString(decryptedMessage));

                                            if (Encoding.UTF8.GetString(decryptedMessage).Contains("Mit21") == true)
                                            {
                                                Console.WriteLine("Found!");

                                                string fileName = @"D:\university\2\1 semester\cybersecurity\lab2\found.dat";
                                                // Check if file already exists. If yes, delete it.     
                                                if (File.Exists(fileName))
                                                {
                                                    File.Delete(fileName);
                                                }

                                                // Create a new file     
                                                using (FileStream fs = File.Create(fileName))
                                                {
                                                    // Add some text to file   

                                                    fs.Write(decryptedMessage, 0, decryptedMessage.Length);
                                                }

                                                breakLoops = true;
                                                break;
                                            }
                                            else
                                            {
                                                //Console.WriteLine("Not found!");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (breakLoops)
                    {
                        break;
                    }
                }

                Console.WriteLine("\nFinished with calculations.");
            }
        }
        
            
    }
}
