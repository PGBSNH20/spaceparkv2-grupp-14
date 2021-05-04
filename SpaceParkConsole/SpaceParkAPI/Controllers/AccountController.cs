using Microsoft.AspNetCore.Mvc;
using SpaceParkAPI.Database;
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
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public async void Post([FromBody] Account value)
        {
            var ppl = new Swapi.SwapiPeople();
            var result = await ppl.FetchAll();
            var person =  result.Find(x => x.Name == value.People.Name);
            using (var db = new SpaceContext())
            {
                db.Accounts.Add(value);
                db.SaveChanges();
            }
            if (person != null)
            {
                //lägg in datan
                //return StatusCode.200_OK;
            }
            else
            {
                //return StatusCode.404_NotFound;
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
