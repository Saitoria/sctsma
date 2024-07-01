using Newtonsoft.Json;
using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.CONFIG;
using SCTSMA.PORTAL.DOMAIN.Dispute;

namespace SCTSMA.PORTAL.INFRASTRUCTURE
{
    public class DisputeRepository : IDisputeRepository
    {
        private readonly HttpClient _httpClient;
        public DisputeRepository(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<CreateDisputeResponseModel> CreateDispute(CreateDisputeRequestModel disputePost)
        {
            CreateDisputeResponseModel? createdDispute = null;
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(disputePost.order.ToString()), "order");
                content.Add(new StringContent(disputePost.description), "description");
                content.Add(new StringContent(disputePost.status.ToString()), "status");

                if (disputePost.image != null)
                {
                    var imageContent = new ByteArrayContent(disputePost.image);
                    imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                    content.Add(imageContent, "image", "image.jpg");
                }

                var request = new HttpRequestMessage(HttpMethod.Post, $"{Names.BaseURL}/disputes/create/");
                request.Content = content;

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    createdDispute = JsonConvert.DeserializeObject<CreateDisputeResponseModel>(responseContent);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
            }
            return createdDispute ?? new CreateDisputeResponseModel();
        }

        public async Task<List<GetDisputesResponseModel>> GetAllDisputes()
        {
            List<GetDisputesResponseModel>? getResponse = null;
            try
            {
                var response = await _httpClient.GetAsync($"{Names.BaseURL}/disputes/dispute/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<List<GetDisputesResponseModel>>(content);
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
                    getResponse = new List<GetDisputesResponseModel>();
                }
            }
            catch (Exception ex)
            {
                getResponse = new List<GetDisputesResponseModel>();
            }
            return getResponse ?? new List<GetDisputesResponseModel>();
        }
    }
}
