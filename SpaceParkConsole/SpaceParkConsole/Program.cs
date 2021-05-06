using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

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

                // If admin user och password stämmer så ska följande alternatuv dyka upp
                // var adminChoice = new string[] { "Info about current parking", "Parking History", "Add new spaceport"}
                // - första admin valet då hämtar vi det som är sparat i paytabellen (kanske behövs att ha någon form av bool)
                // där vi enbart hämtar listan på kunden som ej har betalat
                // - andra admin valet ska man kunna Hämta allting som är sparat från DB:n
                // 

                //HttpClient client = new HttpClient("localhost:xxx");
                //var task = SendRequest(username, password).Result;
                //var code = task.StatusCode;
                //if (code == HttpStatusCode.OK)
                //{
                //    Console.WriteLine("Succesfully registered");
                //}
                //else if (code == HttpStatusCode.NotFound)
                //{
                //    Console.WriteLine($"No person with name {realname} found");
                //}
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
        //public async static Task<HttpResponseMessage> SendRequest(Uri uri, HttpContent content)
        //{
        //    var url = "https://localhost:44332/api/account/Post";
        //    string json = JsonConvert.SerializeObject("");
        //    //var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        //    var stringContent = new StringContent(json.ToString());
        //    var client = new RestClient("https://localhost:44332/api/account/");
        //    HttpResponseMessage response = await client.PostAsync(url, stringContent);
        //    return response;

        //}
    }
}
