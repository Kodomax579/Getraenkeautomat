using System.Runtime.CompilerServices;

namespace GetraenkeautomatVorrat.Services
{
    public class Request
    {
        private HttpClient _httpClient;
        private string urlStorage = "http://localhost:9004/api/Storage/";
        private string urlOrder = "http://localhost:9003/api/Order/";

        public Request(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RefillProducts(int amount, string name)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync($"{urlStorage}GetNewProducts?amount={amount}&name={name}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> PutOrder(int amount, string name)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync($"{urlOrder}CreateNewOrder?amount={amount}&name={name}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
