using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using WpfClient.Models;


namespace WpfClient.Tools.Managers
{
    internal static class ClientManager
    {
        internal static HttpClient _client = new HttpClient();

        internal static void Initialize()
        {
            _client.BaseAddress = new Uri("https://localhost:5001/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal static async Task<User> GetUserByCredentialsAsync(string cardNumber, string pin)
        {
            User resultUser;
            //TODO URL
            string url = "api/users/login";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url,new {number=cardNumber,pincode=pin});
            //or use https://stackoverflow.com/questions/6117101/posting-jsonobject-with-httpclient-from-web-api
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            return await response.Content.ReadAsAsync<User>();
        }
    }
}
