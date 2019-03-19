using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CarvedRock.Web.Models;
using Newtonsoft.Json;

namespace CarvedRock.Web.Clients
{
    public class ProductHttpClient
    {
        private readonly HttpClient _httpClient;

        public ProductHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<ProductsContainer>> GetProducts()
        {
            var response = await _httpClient.GetAsync(@"?query= 
            { products 
                { id name price rating photoFileName } 
            }");
            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ProductsContainer>>(stringResult);
        }
    }
}
