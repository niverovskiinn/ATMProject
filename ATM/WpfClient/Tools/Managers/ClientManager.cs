using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WpfClient.Models;


namespace WpfClient.Tools.Managers
{
    public class ClientManager 
    {
        private static readonly object Locker = new object();
        private static ClientManager _instance;

        private HttpClient _client;
        
        private const string UriBaseAddress = "https://localhost:5001/";


        internal static ClientManager Instance
        {
        get
        {
            if (_instance != null)
                return _instance;
            lock (Locker)
            {
                return _instance ?? (_instance = new ClientManager());
            }
        }
        }

        private ClientManager()
        {

        }

        internal User GetUserByCredentials(string cardNumber, string pin)
        {
            string uri = "api/users/login";

            var response = _client.PostAsJsonAsync(uri, new { number = cardNumber, pincode = pin }).Result;
            if ((int)response.StatusCode == 215)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            //response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<User>().Result;
        }

        internal List<Account> GetAccountsByPassport(string passN)
        {
            string uri = "api/accounts/list";

            var response = _client.PostAsJsonAsync(uri, new { passport = passN }).Result;

            if ((int)response.StatusCode == 215)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            //response.EnsureSuccessStatusCode();

            string responseBody = response.Content.ReadAsStringAsync().Result;

            return
                JsonConvert.DeserializeObject<List<Account>>(responseBody);
        }

        internal List<Transaction> GetTransactionsPeriod(int accId, DateTime f, DateTime t)
        {
            string uri = "api/transactions/period";

            DateTime f1 = DateTime.ParseExact(f.ToString("yyyy-MM-ddTHH:mm:ss"), "yyyy-MM-ddTHH:mm:ss", null);
            DateTime t1 = DateTime.ParseExact(t.ToString("yyyy-MM-ddTHH:mm:ss"), "yyyy-MM-ddTHH:mm:ss", null);

            var response = _client.PostAsJsonAsync(uri, new { account = accId, from = f1, to = t1 }).Result;

            if ((int)response.StatusCode == 215)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            //response.EnsureSuccessStatusCode();

            string responseBody = response.Content.ReadAsStringAsync().Result;

            var serializeSettings = new JsonSerializerSettings();
            serializeSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss" });
            var result = JsonConvert.DeserializeObject<List<Transaction>>(responseBody, serializeSettings);

            return result;
            //JsonConvert.DeserializeObject<List<Transaction>>(responseBody);
        }

        internal bool SendMoneyToCard(int accId, string recipientCard, decimal am, string not)
        {
            string uri = "api/transactions/send";

            var response = _client.PostAsJsonAsync(uri, new { account = accId, number = recipientCard, amount = am, notes = not}).Result;
            //response.EnsureSuccessStatusCode();
            if ((int)response.StatusCode == 215)
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            return true;
        }

        internal bool DepositToAccount(int accId, decimal am)
        {
            string uri = "api/transactions/deposit";

            var response = _client.PostAsJsonAsync(uri, new { account = accId, amount = am}).Result;
            //response.EnsureSuccessStatusCode();
            if ((int)response.StatusCode == 215)
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            return true;
        }

        internal bool WithdrawFromAccount(int accId, decimal am)
        {
            string uri = "api/transactions/withdraw";

            var response = _client.PostAsJsonAsync(uri, new { account = accId, amount = am , notes = ""}).Result;
            //response.EnsureSuccessStatusCode();
            if ((int)response.StatusCode == 215)
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            return true;
        }

        internal bool FreezeAccountById(int accId)
        {
            string uri = "api/accounts/froze";

            var response = _client.PostAsJsonAsync(uri, new { id = accId}).Result;
//            response.EnsureSuccessStatusCode();
            if ((int)response.StatusCode == 215)
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            return true;
        }

        internal void Initialize()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(UriBaseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


    }

    //public class ClientManager : IDisposable
    //{
    //    HttpClient _client = new HttpClient();
    //    private const string _uriBaseAddress = "https://localhost:5001/";

    //    public ClientManager()
    //    {
    //        _client.BaseAddress = new Uri(_uriBaseAddress);
    //        _client.DefaultRequestHeaders.Accept.Clear();
    //        _client.DefaultRequestHeaders.Accept.Add(
    //            new MediaTypeWithQualityHeaderValue("application/json"));
    //    }

    //    internal User GetUserByCredentials(string cardNumber, string pin)
    //    {
    //        string uri = "api/users/login";
    //        var response = _client.PostAsJsonAsync(uri, new { number = cardNumber, pincode = pin }).Result;
    //        //or use https://stackoverflow.com/questions/6117101/posting-jsonobject-with-httpclient-from-web-api
    //        response.EnsureSuccessStatusCode();
    //        //if (!response.IsSuccessStatusCode)
    //        //    throw new InvalidOperationException(response.Content.ToString());

    //        return response.Content.ReadAsAsync<User>().Result;
    //    }

    //    public void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            _client = null;
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        this.Dispose(false);
    //        GC.SuppressFinalize(this);
    //    }

    //    ~ClientManager()
    //    {
    //        this.Dispose(false);
    //    }
    //}
    //internal static class ClientManager
    //{
    //    internal static HttpClient _client = new HttpClient();

    //    internal const string _uriBaseAddress = "https://localhost:5001/";

    //    internal static void Initialize()
    //    {
    //        _client.BaseAddress = new Uri(_uriBaseAddress);
    //        _client.DefaultRequestHeaders.Accept.Clear();
    //        _client.DefaultRequestHeaders.Accept.Add(
    //            new MediaTypeWithQualityHeaderValue("application/json"));
    //    }

    //    internal static async Task<User> GetUserByCredentialsAsync(string cardNumber, string pin)
    //    {
    //        string uri = "api/users/login";

    //        HttpResponseMessage response = await _client.PostAsJsonAsync(uri,new {number=cardNumber,pincode=pin});
    //        //or use https://stackoverflow.com/questions/6117101/posting-jsonobject-with-httpclient-from-web-api
    //        response.EnsureSuccessStatusCode();
    //        //if (!response.IsSuccessStatusCode)
    //        //    throw new InvalidOperationException(response.Content.ToString());

    //        return response.Content.ReadAsAsync<User>().Result;
    //    }

    //    internal static async Task<IEnumerable<User>> GetAccountsByPassportAsync(string passN)
    //    {
    //        string uri = "api/accounts/list";

    //        HttpResponseMessage response = await _client.PostAsJsonAsync(uri, new { passport = passN});
    //        //or use https://stackoverflow.com/questions/6117101/posting-jsonobject-with-httpclient-from-web-api
    //        response.EnsureSuccessStatusCode();
    //        //if (!response.IsSuccessStatusCode)
    //        //    throw new InvalidOperationException(response.Content.ToString());

    //        return response.Content.ReadAsAsync<IEnumerable<Account>>().Result;
    //    }

    //}

}
