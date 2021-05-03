using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceParkAPI.Models;
using RestSharp;

namespace SpaceParkAPI.Swapi
{
    public class SwapiPeople : ISwapi<People>
    {
        public RestClient Client { get; set; }
        public RestRequest Request { get; set; }

        public SwapiPeople()
        {
            this.Client = new RestClient("https://swapi.dev/api/");
            this.Request = new RestRequest("people/", DataFormat.Json);
        }

        public async Task<List<People>> FetchAll()
        {
            var ppl = new List<People>();
            this.Request = new RestRequest("people/?page=1", DataFormat.Json);
            var pplResponse = await Client.GetAsync<PeopleResponse>(Request);

            bool nextPage = true;

            while (nextPage)
            {
                pplResponse.Results
                    .ForEach(x =>
                    ppl.Add(x));

                if (string.IsNullOrEmpty(pplResponse.Next))
                {
                    nextPage = false;
                    break;
                }
                else
                {
                    string next = pplResponse.Next
                        .Split('/')
                        .Last();

                    this.Request = new RestRequest($"people/{next}", DataFormat.Json);
                    pplResponse = await Client.GetAsync<PeopleResponse>(this.Request);
                }
            }
            return ppl;
        }

        public async Task<People> FetchById(int id)
        {
            this.Request = new RestRequest($"people/{id}/", DataFormat.Json);
            var result = await Client.GetAsync<People>(Request);

            return result;
        }
    }
}
