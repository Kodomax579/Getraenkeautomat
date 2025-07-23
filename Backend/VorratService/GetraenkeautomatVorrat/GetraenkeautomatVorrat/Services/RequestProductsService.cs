//using System.Runtime.CompilerServices;

//namespace GetraenkeautomatVorrat.Services
//{
//    public class RequestProductsService
//    {
//        private HttpClient _httpClient;
//        private VorratService _vorratService;
//        private string url = "http://localhost:9004/";
//        public RequestProductsService(HttpClient httpClient, VorratService vorratService)
//        {
//            _httpClient = httpClient;
//            _vorratService = vorratService;
//        }

//        public async Task RefillProducts(int amount, string name)
//        {
//            try
//            {
//                HttpResponseMessage httpResponse = await _httpClient.GetAsync($"{url}/GetNewProducts?amount={amount}&name={name}");

//                if (httpResponse.IsSuccessStatusCode)
//                {
//                    this._vorratService.Refill(name,amount);
//                }
//            }
//            catch
//            {

//            }
//        }
//    }
//}
