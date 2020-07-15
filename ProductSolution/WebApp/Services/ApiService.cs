using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public class ApiService : IApiService
    {
        public async Task<T> Get<T>(string url, string parameters = "")
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url + parameters);

                var responseString = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(responseString);

                    return result;
                }

                throw new HttpRequestException($"{ (int)response.StatusCode }: { responseString }");
            }
        }

        public async Task<string> Post<T>(T body, string url, string parameters = "")
        {
            using (HttpClient client = new HttpClient())
            {
                var bodyString = JsonConvert.SerializeObject(body);

                var content = new StringContent(bodyString, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url + parameters, content);

                var responseString = response.Content.ReadAsStringAsync().Result;

                return responseString;
            }
        }

        public async Task<string> Put<T>(T body, string url, string parameters = "")
        {
            using (HttpClient client = new HttpClient())
            {
                var bodyString = JsonConvert.SerializeObject(body);

                var content = new StringContent(bodyString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(url + parameters, content);

                var responseString = response.Content.ReadAsStringAsync().Result;

                return responseString;
            }
        }

        public async Task<string> Delete(string url, string parameters = "")
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(url + parameters);

                var responseString = response.Content.ReadAsStringAsync().Result;

                return responseString;
            }
        }
    }
}
