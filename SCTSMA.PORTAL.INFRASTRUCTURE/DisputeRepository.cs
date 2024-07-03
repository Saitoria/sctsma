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

        public async Task<bool> DeleteDispute(int disputeId)
        {
            bool deleteResponse = false;
            try
            {
                var response = await _httpClient.DeleteAsync($"{Names.BaseURL}/disputes/dispute/delete/{disputeId}/");
                if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    // If the response status code is 204 No Content, it means the deletion was successful
                    deleteResponse = true;
                }
                else
                {
                    // If the status code is not successful, log or handle the error appropriately
                    deleteResponse = false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                deleteResponse = false;
            }
            return deleteResponse;
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

        public async Task<CreateDisputeResponseModel> UpdateDispute(CreateDisputeRequestModel disputePost, int disputeId)
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

                var request = new HttpRequestMessage(HttpMethod.Put, $"{Names.BaseURL}/disputes/dispute/{disputeId}/update");
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
    }
}
