using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceParkAPI.Models;
using RestSharp;

namespace SpaceParkAPI.Swapi
{
    public class SwapiStarship : ISwapi<StarShip>
    {
        public RestClient Client { get; set; }
        public RestRequest Request { get; set; }

        public SwapiStarship()
        {
            this.Client = new RestClient("https://swapi.dev/api/");
            this.Request = new RestRequest("starships/", DataFormat.Json);
        }

        public async Task<List<StarShip>> FetchAll()
        {
            var starships = new List<StarShip>();
            this.Request = new RestRequest("starships/?page=1", DataFormat.Json);
            var starshipResponse = await Client.GetAsync<StarshipRespone>(Request);

            bool nextPage = true;

            while(nextPage)
            {
                starshipResponse.Results
                    .ForEach(x =>
                    starships.Add(x));

                if(string.IsNullOrEmpty(starshipResponse.Next))
                {
                    nextPage = false;
                    break;
                }
                else
                {
                    string next = starshipResponse.Next
                        .Split('/')
                        .Last();

                    this.Request = new RestRequest($"starships/{next}", DataFormat.Json);
                    starshipResponse = await Client.GetAsync<StarshipRespone>(this.Request);
                }
            }
            return starships;
        }

        public async Task<StarShip> FetchById(int id)
        {
            this.Request = new RestRequest($"starships/{id}/", DataFormat.Json);
            var result = await Client.GetAsync<StarShip>(Request);

            return result;
        }
    }
}
