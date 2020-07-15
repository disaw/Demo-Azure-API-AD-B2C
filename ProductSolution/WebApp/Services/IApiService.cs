using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public interface IApiService
    {
        Task<T> Get<T>(string url, string parameters = "");

        Task<string> Post<T>(T body, string url, string parameters = "");

        Task<string> Put<T>(T body, string url, string parameters = "");

        Task<string> Delete(string url, string parameters = "");
    }
}
