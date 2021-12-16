using NLog;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace lab13_14
{

    //клас User для представлення користувача у системі з такими властивостями:
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
        private static Logger log = NLog.LogManager.GetCurrentClassLogger();

        //Поле типу Словник для зберігання зареєстрованих користувачів
        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        public static User Register(string userName, string password, string[] roles = null)
        {
            //генерація солі
            log.Trace($"Generating Salt");
            log.Trace($"Generating random numbers");
            var rng = RandomNumberGenerator.Create();
            log.Trace($"Creating an empty RandomNumber array");
            var saltBytes = new byte[16];
            log.Trace($"Assigning {saltBytes} to saltBytes");
            log.Debug($"Assigning {saltBytes} to saltBytes");
            log.Trace($"Returning {saltBytes}");
            rng.GetBytes(saltBytes);
            log.Trace($"Converting {saltBytes} to string");
            var saltText = Convert.ToBase64String(saltBytes);

            //генерація соленого и хешированого пароля
            log.Trace($"Generating Hash+Salt password");
            log.Trace($"Creating an instance of the default implementation of SHA256 algorithm");
            var sha = SHA256.Create();
            log.Trace($"Adding salt to our password");
            var saltedPassword = password + saltText;
            log.Trace($"Converting {saltedPassword} to string");
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
            log.Info("New user was created");
            return user;
        }
        //перевірка пароля
        public static bool CheckPassword(string username, string password)
        {
            log.Trace($"Checking Password function");
            log.Trace($"Checking userName before start");
            log.Debug($"Checking userName before start");
            if (!_users.ContainsKey(username))
            {
                log.Fatal($"Wrong user/password: login={username}");
                log.Trace($"Mistake! Check your password!");
                log.Warn($"Warning! Uknown user");
                return false;
            }
            var user = _users[username];

            //повторна генерація соленого и хешированого пароля
            log.Trace($"Generating Hash+Salt password");
            log.Trace($"Creating an instance of the default implementation of SHA256 algorithm");
            var sha = SHA256.Create();
            log.Trace($"Adding salt to our password");
            var saltedPassword = password + user.Salt;
            log.Trace($"Converting {saltedPassword} to string");
            var saltedhashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
            log.Trace($"Salted hash generated");
            log.Trace($"Returning {saltedhashedPassword == user.PasswordHash}");
            return (saltedhashedPassword == user.PasswordHash);
        }

        //Метод для автентифікації користувачів
        public static void LogIn(string userName, string password)
        {
            // Перевірка пароля
            log.Trace($"Checking the password");
            log.Debug($"Checking the password");
            if (CheckPassword(userName, password))
            {
                // Створюється екземпляр автентифікованого користувача
                log.Trace($"Creating new authentic GenericIdentity");
                var identity = new GenericIdentity(userName, "OIBAuth");
                // Виконується прив’язка до ролей, до яких належить користувач
                log.Trace($"Creating new GenericPrincipal");
                var principal = new GenericPrincipal(identity, _users[userName].Roles);
                // Створений екземпляр автентифікованого користувача з відповідними
                // ролями присвоюється потоку, в якому виконується програма
                log.Trace($"Assigning {principal} to CurrentPrincipal");
                log.Debug($"Assigning {principal} to CurrentPrincipal");
                System.Threading.Thread.CurrentPrincipal = principal;
                log.Debug($"The user was stored");
                log.Trace($"The user logined successfully");
            }
        }
    }

    class Program
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            log.Trace("Starting a cycle for registration");
            for (int i = 0; i < 4; i++)//цикл до 4, для виконання реєстрації чотирьох користувачів
            {
                Console.WriteLine();
                log.Info($"Registration of a new user");
                log.Trace($" {i} iteration of redistration cycle");
                log.Debug($" {i} iteration of redistration cycle");
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Enter a new username to register: ");
                string username = Console.ReadLine();
                log.Trace($"{username} - user's login");
                log.Debug($"{username} - user's login");
                Console.WriteLine($"Enter a password for {username}: ");
                var password = Console.ReadLine();
                log.Trace($"{password} - user's password");
                log.Debug($"{password} - user's password");
                var user = Protector.Register(username, password);
                log.Info($"User's data was stored");
                Console.WriteLine($"Login: {user.Login}");
                log.Trace($"User's salt was generated");
                log.Debug($"User's salt was generated");
                Console.WriteLine($"Salt: {user.Salt}");
                log.Trace($"User's password hashed+salted");
                log.Debug($"User's password hashed+salted");
                Console.WriteLine("Password (salted & hashed): {0}", user.PasswordHash);
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------");
                bool correctPassword = false;
                log.Trace($"Checking user authentification before logging in");
                log.Debug($"Checking user authentification before logging in");
                while (!correctPassword)
                {
                    log.Info($"Login of a user");
                    Console.WriteLine("Enter a username to log in: ");
                    string loginUsername = Console.ReadLine();
                    log.Trace($"{loginUsername} - user's entered login");
                    log.Debug($"{loginUsername} - user's entered login");
                    Console.WriteLine("Enter a password to log in: ");
                    string loginPassword = Console.ReadLine();
                    log.Trace($"{loginPassword} - user's entered password");
                    log.Debug($"{loginPassword} - user's entered password");
                    correctPassword = Protector.CheckPassword(loginUsername, loginPassword);
                    log.Info($"User's data was stored");
                    log.Trace($"Checking {username} and {password} before start logging in");
                    log.Debug($"Checking {username} and {password} before start logging in");
                    if (correctPassword)
                    {
                        Console.WriteLine($"Correct! {loginUsername} has been logged in.");
                        log.Trace($"User has been logged in");
                        log.Debug($"User has been logged in");
                    }
                    else //якщо не правильний пароль або логін
                    {
                        Console.WriteLine("Invalid username or password. Please try again!");
                        log.Warn($"Warning! Invalid username or password. Please try again!");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}

