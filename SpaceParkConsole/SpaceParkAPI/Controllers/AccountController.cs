using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SpaceParkAPI.Database;
using SpaceParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using SpaceParkAPI.Security;
using SpaceParkAPI.Swapi;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaceParkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET api/<AccountController>/5
        [HttpGet("{username},{password}")]
        public async Task<IActionResult> Get(string username, string password)
        {
            using (var db = new SpaceContext())
            {
                try
                {
                    var acc = db.Accounts.First(x => 
                        x.Username.ToLower() == username.ToLower()
                        && Encryption.Encrypt(password) == x.Password);

                    return Ok("Logged in successfully!");
                }
                catch (InvalidOperationException)
                {
                    return Unauthorized("Invalid login parameters.");
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account value)
        {
            var ppl = new SwapiPeople();
            var result = await ppl.FetchAll();
            var person = result.Find(x => x.Name == value.People.Name);

            using (var db = new SpaceContext())
            {
                if (db.Accounts.Any(x => x.Username == value.Username))
                {
                    return Conflict($"Account with username {value.Username} already exists!");
                }
                if (person != null)
                {
                    value.People.PersonalID = result.IndexOf(person) + 1;
                    value.Password = Encryption.Encrypt(value.Password);
                    db.Accounts.Add(value);
                    db.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created, "Account registered!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, $"{value.People.Name} is not a valid Star Wars character, see https://swapi.dev/api/people/ for all valid characters.");
                }
            }
        }
    }
}
