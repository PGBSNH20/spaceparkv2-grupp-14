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
                var username = Menu.RequestUserInput("Register your username!");
                var password = Menu.RequestUserInput("Register your password!");
                //Docker inkludering för att spara ner det i DB:n
                Console.WriteLine("Registration complete!");
            }
            else
            {
                //Docker inkludering här med. Behöver manuellt lägga in ett admin konto.
                var username = Menu.RequestUserInput("Enter username!");
                var password = Menu.RequestUserInput("Enter password!");
            }
        }
    }
}
