using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using SpaceParkAPI.Models;
using SpaceParkAPI.Database;
using Newtonsoft.Json;
using SpaceParkAPI.Swapi;
using System.Text;
using SpaceParkAPI.Security;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaceParkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var db = new SpaceContext())
            {
                var json = JsonConvert.SerializeObject(db.Pay);
                return Ok(json);
            }

            //if (account.Username.ToLower() == "admin")
            //{
            //    
            //}
            //else return Unauthorized("Only a system administrator can view the full parking history.");
        }
        // GET api/<ParkController>/5
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            using (var db = new SpaceContext())
            {
                var result = db.Pay.Where(x =>
                    x.Name.ToLower() == name.ToLower());

                if (result.Count() == 0)
                {
                    return BadRequest();
                }

                var json = JsonConvert.SerializeObject(result);
                return Ok(json);
            }
        }

        // POST api/<ParkController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pay park)
        {
            using (var db = new SpaceContext())
            {
                //var local = db.Set<Pay>()
                //    .Local
                //    .FirstOrDefault(entry => entry.ID.Equals(park.ID));
                var port = db.SpacePorts.Find(park.SpacePort.ID);
                
                if (port.Slots < 5)
                {
                    if (db.Pay.Any(x => x.Name == park.Name && x.PaidAt == null))
                    {
                        return Conflict("Your account already has one unpaid parking at the moment. To resolve this, pay and retry parking.");
                    }
                    else
                    {
                        if (park != null)
                        {
                            port.Slots++;
                            park.PaidAt = null;
                            db.Entry(port).State = EntityState.Detached;
                            db.SpacePorts.Add(port);
                            db.SaveChanges(); 
                        }
                        db.Entry(port).State = EntityState.Modified;
                        return StatusCode(StatusCodes.Status201Created, "Your parking has been registered!");
                    }

                }
                else
                {
                    return Conflict($"The following Spaceport {park.SpacePort.Slots} is at it's max limit!");
                }
            }
        }

        // PUT api/<ParkController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            using (var db = new SpaceContext())
            {
                try
                {
                    var pay = db.Pay.Where(x => x.ID == id).First();
                    if (pay.PaidAt == null)
                    {
                        pay.PaidAt = DateTime.Now;
                        db.SaveChanges();

                        return Ok("Your parking has been paid for and you may now park again if you wish.");
                    }
                    else
                    {
                        return Conflict($"Parking with id [{id}] has already been paid");
                    }
                }
                catch (InvalidOperationException)
                {
                    return NotFound($"No parking with id [{id}] exists.");
                }
            }
        }
    }
}
