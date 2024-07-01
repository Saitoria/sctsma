using Newtonsoft.Json;
using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.CONFIG;
using SCTSMA.PORTAL.DOMAIN.User;
using System.Text;

namespace SCTSMA.PORTAL.INFRASTRUCTURE
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        public UserRepository(HttpClient httpClient) 
        { 
            _httpClient = httpClient;
        }
        public async Task<ActivateUserResponseModel> ActivateUser(ActivateUserRequestModel activateUserRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserModel> GetUser()
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseModel> LoginUser(LoginRequestModel loginRequest)
        {
            LoginResponseModel? postResponse = null;
            try
            {
                var json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{@Names.BaseURL}/users/login/", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);

                    // Check the response for success
                    if (apiResponse != null)
                    {
                        postResponse = apiResponse;
                    }
                    else
                    {
                        postResponse = apiResponse;
                    }
                }
                else
                {
                    postResponse = new LoginResponseModel();                
                }
            }
            catch (Exception ex)
            {
                postResponse = new LoginResponseModel();
            }
            return postResponse ?? new LoginResponseModel(); 
        }
    }
}
