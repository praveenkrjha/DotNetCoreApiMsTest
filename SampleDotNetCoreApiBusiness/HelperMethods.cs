using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotNetCoreApiBusiness
{
    public static class HelperMethods
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient, string uri, object data)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var strData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(uri, strData);
        }
    }
}
