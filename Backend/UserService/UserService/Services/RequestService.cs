using User.Data;
using User.DTO;
using User.Models;

namespace UserService.Services
{
    public class RequestService
    {
        private UserContext _dbContext;
        HttpClient _httpClient;
        public RequestService(UserContext userContext, HttpClient httpClient)
        {
            this._dbContext = userContext;
            _httpClient = httpClient;
        }

        public async Task<int> CreateBankAccount(string userName)
        {

            try
            {
                var userId = _dbContext.Users.ToList().Count + 1;
                var url = $"http://localhost:9005/api/Bank/CreateBankAccount/{userId}";
                HttpResponseMessage response = await _httpClient.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    return userId;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
