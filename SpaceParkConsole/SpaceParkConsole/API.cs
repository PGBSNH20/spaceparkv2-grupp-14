using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp;
using Newtonsoft.Json;
using SpaceParkConsole.Models;

namespace SpaceParkConsole
{
    public class API
    {
        private const string BASE = "https://localhost:44332/";

        public static string ParseStatus(IRestResponse response)
        {
            return $"[{(int)response.StatusCode}: {response.StatusCode}]: {response.Content}";
        }

        public static async Task<IRestResponse> Register(string user, string pw, string character)
        {
            var account = new Account()
            {
                Username = user,
                Password = pw,
                People = new People()
                {
                    Name = character
                }
            };

            var client = new RestClient(BASE);
            var request = new RestRequest("api/Account", Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(account));

            var response = await client.ExecuteAsync(request);

            return response;
        }
    }
}
