using Newtonsoft.Json;
using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.CONFIG;
using SCTSMA.PORTAL.DOMAIN.User.ActivateUser;
using SCTSMA.PORTAL.DOMAIN.User.CreateUser;
using SCTSMA.PORTAL.DOMAIN.User.ListUser;
using SCTSMA.PORTAL.DOMAIN.User.LoginUser;
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
            ActivateUserResponseModel? postResponse = null;
            try
            {
                var json = JsonConvert.SerializeObject(activateUserRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{@Names.BaseURL}/users/activate-account", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ActivateUserResponseModel>(responseContent);


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
                    postResponse = new ActivateUserResponseModel();
                }
            }
            catch (Exception ex)
            {
                postResponse = new ActivateUserResponseModel();
            }
            return postResponse ?? new ActivateUserResponseModel();
        }

        public async Task<CreateUserResponseModel> CreateUser(CreateUserRequestModel createUserRequest)
        {
            CreateUserResponseModel? postResponse = null;
            try
            {
                var json = JsonConvert.SerializeObject(createUserRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{@Names.BaseURL}/users/register/", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<CreateUserResponseModel>(responseContent);


                    if (apiResponse != null)
                    {
                        postResponse = apiResponse;
                    }
                    else
                    {
                        postResponse = new CreateUserResponseModel
                        {
                            message = "Add user failed"
                        };
                    }
                }
                else
                {
                    postResponse = new CreateUserResponseModel
                    {
                        message = "Connection failed"
                    };
                }
            }
            catch (Exception ex)
            {
                postResponse = new CreateUserResponseModel
                {
                    message = ex.Message,
                };
            }
            return postResponse ?? new CreateUserResponseModel();
        }

        public async Task<List<ListUserResponseModel>> GetAllUsers()
        {
            List<ListUserResponseModel>? getResponse = null;
            try
            {
                var response = await _httpClient.GetAsync($"{Names.BaseURL}/users/list_users/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<List<ListUserResponseModel>>(content);
                    if (apiResponse != null)
                    {
                        getResponse = apiResponse;
                    }
                    else
                    {
                        getResponse = apiResponse;
                    }
                }
                else
                {
                    getResponse = new List<ListUserResponseModel>();
                }
            }
            catch (Exception ex)
            {
                getResponse = new List<ListUserResponseModel>();
            }
            return getResponse ?? new List<ListUserResponseModel>();
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
