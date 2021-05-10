using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SpaceParkConsole.Models;

namespace SpaceParkConsole
{
    public class AccountHelper
    {
        public static Account RegisterPrompt()
        {
            var username = Menu.RequestUserInput("Register your username!");
            var password = Menu.RequestUserInput("Register your password!");
            var realname = Menu.RequestUserInput("Input your Star Wars name!");

            var response = API.Register(username, password, realname);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                return new Account()
                {
                    Username = username,
                    People = new People()
                    {
                        Name = realname
                    }
                };
            }
            else
            {
                Console.WriteLine(API.ParseStatus(response.Result));
                var option = Menu.ShowMenu($"{API.ParseStatus(response.Result)}\nRetry?", new string[] { "Yes", "No" });

                if(option == "Yes")
                {
                    return null;
                }
                else
                {
                    Initializer.Run();
                }


                return null;
            }
        }
    }
}
