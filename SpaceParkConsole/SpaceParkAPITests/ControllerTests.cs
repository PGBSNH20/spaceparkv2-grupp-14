using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SpaceParkAPI.Controllers;
using SpaceParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpaceParkAPITests
{
    public class ControllerTests
    {
        [Fact]
        public void Get_All_Account()
        {
            var client = new RestClient("https://localhost:44332/api");
            var request = new RestRequest("account/", Method.GET);
            request.AddJsonBody(JsonConvert.SerializeObject(new Account { Username = "Luke456", Password = "rocket50" }));
            var result = client.ExecuteAsync(request);
            //var status = new RestResponse();
            Assert.Equal(200, (int)result.Result.StatusCode);
        }
    }
}
