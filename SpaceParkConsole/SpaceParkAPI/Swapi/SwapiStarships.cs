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
            int index = 1;
            this.Request = new RestRequest($"starships/?page={index}", DataFormat.Json);
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
                    index++;
                    this.Request = new RestRequest($"starships/?page{index}");
                    starshipResponse = await Client.GetAsync<StarshipRespone>(this.Request);
                }
            }
            return starships;
        }

        public async Task<StarShip> FetchById(int id)
        {
            this.Request = new RestRequest($"starships/{id}");
            var result = await Client.GetAsync<StarShip>(Request);

            return result;
        }
    }
}
