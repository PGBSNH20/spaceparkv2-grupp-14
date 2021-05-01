using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SpaceParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaceParkAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StarshipController : ControllerBase
    {
        // GET: api/<StarshipController>
        [HttpGet]
        public async Task<List<StarShip>> Get()
        {
            var starships = new List<StarShip>();
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest("starships/", DataFormat.Json);
            var starshipResponse = await client.GetAsync<StarshipRespone>(request);

            foreach (var s in starshipResponse.Results)
            {
                starships.Add(s);
            }

            return starships;
        }

        // GET api/<StarshipController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StarshipController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StarshipController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StarshipController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
