using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace SpaceParkAPI.Swapi
{
    interface ISwapi<T>
    {
        RestClient Client { get; set; }
        RestRequest Request { get; set; }
        Task<List<T>> FetchAll();
        Task<T> FetchById(int x);
    }
}
