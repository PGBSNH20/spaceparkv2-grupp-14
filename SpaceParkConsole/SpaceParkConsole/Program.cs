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
                Console.WriteLine("Enter your username!");
                var userInput = Console.ReadLine();
                Console.WriteLine("Enter your password!");
                var passInput = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Hello");
            }
        }
    }
}
