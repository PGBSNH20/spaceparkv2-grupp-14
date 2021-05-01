using System;

namespace SpaceParkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logInChoice = new string[] { "Register", "Log in" };
            var option = Menu.ShowMenu("Welcome", logInChoice);

            if(option.ToString() == logInChoice[0])
            {
                Console.Clear();
                Console.WriteLine("Register your username!");
                var userInput = Console.ReadLine();
                Console.WriteLine("Register your password!");
                var passInput = Console.ReadLine();
                //Docker inkludering för att spara ner det i DB:n
                Console.WriteLine("Done with the registration!");
            }
            else
            {
                //Docker inkludering här med. Behöver manuellt lägga in ett admin konto.
                Console.WriteLine("Enter your username!");
                var userInput = Console.ReadLine();
                Console.WriteLine("Enter your password!");
                var passInput = Console.ReadLine();
            }
        }
    }
}
