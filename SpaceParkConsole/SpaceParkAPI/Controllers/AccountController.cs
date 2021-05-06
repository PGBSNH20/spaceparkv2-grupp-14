using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET api/<AccountController>/5
        [HttpGet("{username},{password}")]
        public async Task<HttpResponseMessage> Get(string username, string password)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var db = new SpaceContext())
            {
                try
                {
                    var acc = db.Accounts.First(x => x.Username.ToLower() == username.ToLower()
                        && Encryption.Encrypt(password) == x.Password);

                    response.StatusCode = HttpStatusCode.OK;
                    response.ReasonPhrase = "Log in success.";
                }
                catch (InvalidOperationException)
                {
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.ReasonPhrase = "Invalid login parameters.";
                }
            }
            return response;
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Account value)
        {
            var ppl = new SwapiPeople();
            var result = await ppl.FetchAll();
            var person = result.Find(x => x.Name == value.People.Name);

            using (var db = new SpaceContext())
            {
                try
                {
                    Account acc = db.Accounts.First(x => x.Username == value.Username);

                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        ReasonPhrase = $"Account with username {value.Username} already exists!"
                    };
                }
                catch (InvalidOperationException)
                {
                }

                if (person != null)
                {
                    value.People.PersonalID = result.IndexOf(person) + 1;
                    value.Password = Encryption.Encrypt(value.Password);
                    db.Accounts.Add(value);
                    db.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.Created);

                }
                else
                {
                    HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                    msg.ReasonPhrase = $"{value.People.Name} is not a valid Star Wars character, see https://swapi.dev/api/people/ for all valid characters.";
                    return msg;
                }
            }
        }
    }
}
