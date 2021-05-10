using System;
using Xunit;
using SpaceParkAPI.Swapi;
using SpaceParkAPI.Models;

namespace SpaceParkAPITests
{
    public class SwapiTests
    {
        [Fact]
        public void FetchAllStarships_Test()
        {
            ISwapi<StarShip> swapi = new SwapiStarship();
            var result = swapi.FetchAll().Result;

            Assert.Equal(36, result.Count);
        }

        [Fact]
        public void FetchStarShipById_Test()
        {
            ISwapi<StarShip> swapi =  new SwapiStarship();
            var result = swapi.FetchById(9).Result;

            Assert.Equal("Death Star", result.Name);
        }

        [Fact]
        public void FetchAllPeople_Test()
        {
            ISwapi<People> swapi = new SwapiPeople();
            var result = swapi.FetchAll().Result;

            Assert.Equal(82, result.Count);
        }

        [Fact]
        public void FetchPeopleById_Test()
        {
            ISwapi<People> swapi = new SwapiPeople();
            var result = swapi.FetchById(3).Result;

            Assert.Equal("R2-D2", result.Name);
        }
    }
}
