using RestSharp;
using System;
using System.Net.Http;
using System.Text.Json;

namespace SpaceParkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logInChoice = new string[] { "Register", "Log in" };
            var parkingChoice = new string[] { "Register Parking", "Pay for parking", "exit" };
            var option = Menu.ShowMenu("Welcome", logInChoice);

            if (option.ToString() == logInChoice[0])
            {
                var username = Menu.RequestUserInput("Register your username!");
                var password = Menu.RequestUserInput("Register your password!");
                var realname = Menu.RequestUserInput("Input your Star Wars name!"); 
                //HttpClient client = new HttpClient("localhost:xxx");
                //HttpStatusCode code = client.SendAsync(username, password, realname).Response;
                //if(code == 200) Console.WriteLine("Successfully registered");
                //else if(code == 404) Console.WriteLine("No person with name {realname} found");


                Console.WriteLine("Registration complete!");
                Console.ReadLine();
                var parkOption = Menu.ShowMenu("Welcome", parkingChoice);
                if (parkOption.ToString() == parkingChoice[0])
                {
                    //HttpClient client = new HttpClient("localhost:xxx");
                    //HttpStatusCode code = client.SendAsync(username, password, realname).Response;
                    //if(code == 200) Console.WriteLine("Successfully registered");
                    //else if(code == 404) Console.WriteLine("No person with name {realname} found");
                }
                else if (parkOption.ToString() == parkingChoice[1])
                {

                }
                else if (parkOption.ToString() == parkingChoice[2])
                {
                    Console.WriteLine("Thank you, goodbye");
                }
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
