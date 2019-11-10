using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace WpfClient.Tools.Managers
{
    internal static class ClientManager
    {
        internal static HttpClient _client = new HttpClient();

        internal static void Initialize()
        {
            _client.BaseAddress = new Uri("http://localhost:18339/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal static async Task<> 
    }
}
