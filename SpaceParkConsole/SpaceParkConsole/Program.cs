using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Newtonsoft.Json;
using SpaceParkConsole.Models;
using System.Net;

namespace SpaceParkConsole
{
    class Program
    {
        static Account _Account;

        static void Main(string[] args)
        {
            var logInChoice = new string[] { "Register", "Log in" };
            var parkingChoice = new string[] { "Register Parking", "Pay for parking", "exit" };
            var option = Menu.ShowMenu("Welcome", logInChoice);

            if (option.ToString() == logInChoice[0])
            {
                Account account = null;
                while (account == null)
                {
                    account = AccountHelper.RegisterPrompt();
                }
            }
            Console.ReadLine();
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
