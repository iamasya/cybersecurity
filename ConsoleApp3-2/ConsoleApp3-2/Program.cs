using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp3_2
{
    class Program
    {
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }

        static void Main(string[] args)
        {
            Guid Guid1 = Guid.Parse("564c8da6-0440-88ec-d453-0bbad57c6036");
            string Hash = "po1MVkAE7IjUUwu61XxgNg==";
            string CatchHash;
            Guid CatchGuid;
            string password;
            

            for (int i1 = 0; i1 <= 9; i1++)
            {
                for (int i2 = 0; i2 <= 9; i2++)
                {
                    for (int i3 = 0; i3 <= 9; i3++)
                    {
                        for (int i4 = 0; i4 <= 9; i4++)
                        {
                            for (int i5 = 0; i5 <= 9; i5++)
                            {
                                for (int i6 = 0; i6 <= 9; i6++)
                                {
                                    for (int i7 = 0; i7 <= 9; i7++)
                                    {
                                        for (int i8 = 0; i8 <= 9; i8++)
                                        {
                                            password = i1.ToString()+ i2.ToString()+ i3.ToString()+ i4.ToString()+ i5.ToString()+ i6.ToString()+ i7.ToString()+ i8.ToString();
                                            //Console.WriteLine(password);
                                            //password = "20192020";
                                            CatchHash = Convert.ToBase64String(ComputeHashMd5(Encoding.Unicode.GetBytes(password)));
                                            //temp = Convert.ToBase64String(ComputeHashMD5(Encoding.Unicode.GetBytes(password)));
                                            
                                            CatchGuid = new Guid(ComputeHashMd5(Encoding.Unicode.GetBytes(password)));
                                            //string guid = System.Guid.ToString();
                                            if ((CatchHash == Hash) && (CatchGuid == Guid1))
                                            {
                                                Console.WriteLine($"Password found: {password}");

                                                goto fff;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
        fff:
            Console.WriteLine("yohoo");
            Console.ReadLine();
        }
        
    }
}
