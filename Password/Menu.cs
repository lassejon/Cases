using System.Text.RegularExpressions;
using Soccer;
using Dance;

namespace Password
{
    public class Menu
    {
        public Serializer Serializer { get; set; }

        public Menu(Serializer serializer)
        {
            Serializer = serializer;
        }

        public void Start()
        {
            // switch der tjekker om man vælger en valid menu menu
            // hvis ikke kører den et rekursivt kald til sig selv.
            Console.WriteLine("[1] soccer   [2] dance   [3] create user   [4] login");
            Console.Write("Choose a menu: ");
            var menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    Soccer();
                    break;
                case "2":
                    Dance();
                    break;
                case "3":
                    CreateUser();
                    break;
                case "4":
                    Login();
                    break;
                default:
                    Console.WriteLine("Invalid choice! Try again.");
                    Start();
                    break;
            }

        }

        public void Soccer() 
        {
            // hardcoded lort om man vil. Ved ikke lige hvad "brugeren" skal kunne her hehe
            var soccer = new SoccerLogic();

            Console.WriteLine(soccer.Chant(0, "omÅL"));
        }

        public void Dance() 
        {
            // mere hård kode ^_^ 
            var dave = new Dancer("Dave", 8);
            var jessica = new Dancer("Jessica", 7);

            var teamDaveJess = dave + jessica;

            Console.WriteLine(dave);
            Console.WriteLine(jessica);
            Console.WriteLine(teamDaveJess);
        }

        public void Login()
        {
            var name = "";

            // løkke der tjekker om brugernavnet findes i filen
            do {
                Console.Write("Enter username: ");
                name = Console.ReadLine();

            } while (!Serializer.Exists(name));

            var user = Serializer.FindUser(name)[0];
            var password = "";

            // løkke der tjekker om indtastede kodeord høre til brugernavnet
            do
            {
                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();
            } while (password != user.Password);

            Console.WriteLine("Login was succesful!");
        }

        public void CreateUser()
        {
            var name = "";

            do
            {
                Console.Write("Enter username: ");
                name = Console.ReadLine();

            } while (Serializer.Exists(name));

            var password = "";

            // løkke der tjekker om kodeordet er validt
            do
            {
                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();

            } while (!IsValidPassword(name, password));

            // oprettelse af bruger
            User user = new User(name, password);
            Serializer.WriteUser(user);

            Console.WriteLine("User creation was succesful!");

        }

        public void UpdatePassword()
        {
            var name = "";

            do
            {
                Console.Write("Enter username: ");
                name = Console.ReadLine();

            } while (!Serializer.Exists(name));

            var user = Serializer.FindUser(name)[0];
            var password = "";

            // løkke der tjekker om det nye kodeord er validt
            // og om det matcher det sidst brugte kodeord.
            do
            {
                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();

            } while (!IsValidPassword(name, password)
                  && !password.Equals(user.Password));

            User userWithUpdatedPassword = new User(name, password);
            Serializer.WriteUser(userWithUpdatedPassword);

            Console.WriteLine("User creation was succesful!");
        }

        public bool IsValidPassword(string name, string password)
        {
            // regex der tjekker for alle kodeordskravene pånær for mellemrum
            var UpperLowerSpecialCaseNoDigitStartEnd12CharlongRegex =
            new Regex("^[^\\d](?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{12,}[^\\d]$");

            // regex der tjekker, om der er mellemrum
            var noWhiteSpaceRegex = new Regex("^[^\\s]*$");

            return UpperLowerSpecialCaseNoDigitStartEnd12CharlongRegex.IsMatch(password)
                && noWhiteSpaceRegex.IsMatch(password)
                && name.ToLower() != password.ToLower();
        }
    }
}