using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace lab11_12._1
{   //клас User для представлення користувача у системі з такими властивостями:
    class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string[] Roles { get; set; }
    }
    //клас Protector для інкапсуляції процесів автентифікації та авторизації користувачі
    class Protector
    {
        //Поле типу Словник для зберігання зареєстрованих користувачів
        private static Dictionary<string, User> _users = new Dictionary<string, User>(); 
        
        public static User Register(string userName, string password, string[] roles = null)
        {
            //генерація солі
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);

            //генерація соленого и хешированого пароля
            var sha = SHA256.Create();
            var saltedPassword = password + saltText;
            var saltedhashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
            var user = new User
            {
                Login = userName,
                PasswordHash = saltedhashedPassword,
                Salt = saltText,
                Roles = roles
            };
            //запис данних у клас
            _users.Add(user.Login, user);
            return user;
        }
        //перевірка пароля
        public static bool CheckPassword(string username, string password)
        {
            if (!_users.ContainsKey(username))
            {
                return false;
            }
            var user = _users[username];

            //повторна генерація соленого и хешированого пароля
            var sha = SHA256.Create();
            var saltedPassword = password + user.Salt;
            var saltedhashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));

            return (saltedhashedPassword == user.PasswordHash);
        }
        //генерація солі
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

        //Метод для автентифікації користувачів
        public static void LogIn(string userName, string password)
        {
            for (int i = 0; i < 4; i++)//цикл до 4, для виконання аутентифікації чотирьох користувачів
            {
                // Перевірка пароля
                if (CheckPassword(userName, password))
                {
                    // Створюється екземпляр автентифікованого користувача
                    var identity = new GenericIdentity(userName, "OIBAuth");
                    // Виконується прив’язка до ролей, до яких належить користувач
                    var principal = new GenericPrincipal(identity, _users[userName].Roles);
                    // Створений екземпляр автентифікованого користувача з відповідними
                    // ролями присвоюється потоку, в якому виконується програма
                    System.Threading.Thread.CurrentPrincipal = principal;
                }
            } 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)//цикл до 4, для виконання реєстрації чотирьох користувачів
            {
                Console.WriteLine("Enter a new username to register: ");
                string username = Console.ReadLine();
                Console.WriteLine($"Enter a password for {username}: ");
                var password = Console.ReadLine();
                var user = Protector.Register(username, password);
                Console.WriteLine($"Login: {user.Login}");
                Console.WriteLine($"Salt: {user.Salt}");
                Console.WriteLine("Password (salted & hashed): {0}", user.PasswordHash);
                Console.WriteLine();
                bool correctPassword = false;
                while (!correctPassword)
                {
                    Console.WriteLine("Enter a username to log in: ");
                    string loginUsername = Console.ReadLine();
                    Console.WriteLine("Enter a password to log in: ");
                    string loginPassword = Console.ReadLine();
                    correctPassword = Protector.CheckPassword(loginUsername, loginPassword);
                    if (correctPassword)
                    {
                        Console.WriteLine($"Correct! {loginUsername} has been logged in.");
                    }
                    else //якщо не правильний пароль або логін
                    {
                        Console.WriteLine("Invalid username or password. Please try again!");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
