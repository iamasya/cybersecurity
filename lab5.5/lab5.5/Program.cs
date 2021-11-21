using System;
using System.Security.Cryptography;
using System.Text;

namespace lab5._5
{
    class Program
    {
        static byte[] ComputeHashSHA256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }

        static void Main(string[] args)
        {
            byte[] Salt = PBKDF2.GenerateSalt();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Register to our website by creating your login & password!");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Create your login name ->");
            string login = Console.ReadLine();
            Console.WriteLine("Create your password ->");
            string password = Console.ReadLine();

            Console.WriteLine("Congrats! Your personal account has been created! ");

            var sha256Forstrl = Convert.ToBase64String(ComputeHashSHA256(Encoding.Unicode.GetBytes(login))); 
            var sha256Forstrp = Convert.ToBase64String(PBKDF2.HashPassword(Encoding.Unicode.GetBytes(Convert.ToString(password)), Salt, 220000));//число ітерацій = номер варіанта * 10'000.
                                                                                                                                                 //мій варіант (номер у списку): 22

            int tt = 0; //кількість спроб
            string loginHash, passwordHash;
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Enter to our website by using your login & password! ");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("WARNING! You have only 3 attempts to log into your account!");
            Console.WriteLine("");

            do
            {
                Console.WriteLine("Please enter your login -> ");
                login = Console.ReadLine();
                loginHash = Convert.ToBase64String(ComputeHashSHA256(Encoding.Unicode.GetBytes(login)));
                Console.WriteLine("Please enter your password -> ");
                password = Console.ReadLine();
                passwordHash = Convert.ToBase64String(PBKDF2.HashPassword(Encoding.Unicode.GetBytes(Convert.ToString(password)), Salt, 220000));

                if (loginHash != sha256Forstrl || passwordHash != sha256Forstrp) //якщо логін або пароль не співпадають, надається ще одна спроба
                {
                    tt++;
                }
                else
                {
                    tt = 1;
                }
            }
            while ((loginHash != sha256Forstrl || passwordHash != sha256Forstrp) && (tt != 3)); ;//"do" буде виконуватися допоки не буде все правильно виведене
                                                                                                 //або допоки логін або пароль не співпадають та кількість спроб не = 3

            if (tt == 3)
            {
                Console.Write("\nPlease check that your input is correct...Try again!\n\n");
            }
            else
            {
                Console.Write("\nYou successfully entered to your personal account!\n\n");
            }
            Console.ReadLine();
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
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                return rfc2898.GetBytes(20);
            }
        }
    }
}
