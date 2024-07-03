using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.CONFIG;
using SCTSMA.PORTAL.DOMAIN.Order;
using Newtonsoft.Json;

namespace SCTSMA.PORTAL.INFRASTRUCTURE
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HttpClient _httpClient;
        public OrderRepository(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        public async Task<List<OrderResponseModel>> GetAllOrders()
        {
            List<OrderResponseModel>? getResponse = null;
            try
            {
                var response = await _httpClient.GetAsync($"{Names.BaseURL}/order/list_orders/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<List<OrderResponseModel>>(content);
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
                    getResponse = new List<OrderResponseModel>();
                }
            }
            catch (Exception ex)
            {
                getResponse = new List<OrderResponseModel>();
            }
            return getResponse ?? new List<OrderResponseModel>();
        }


        public async Task<OrderResponseModel> CreateOrder(OrderRequestModel order, string token)
        {
            OrderResponseModel? createdOrder = null;
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(order.name), "name");
                //content.Add(new StringContent(order.status), "status");
                content.Add(new StringContent(order.description), "description");
                content.Add(new StringContent(order.tot_price.ToString()), "tot_price");
                //content.Add(new StringContent(order.order_number.ToString()), "order_number");
                content.Add(new StringContent(order.address), "address");
                content.Add(new StringContent(order.buyer.ToString()), "buyer");
                content.Add(new StringContent(order.seller.ToString()), "seller");

                if (order.image != null)
                {
                    var imageContent = new ByteArrayContent(order.image);
                    imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                    content.Add(imageContent, "image", "image.jpg");
                }

                var request = new HttpRequestMessage(HttpMethod.Post, $"{Names.BaseURL}/order/agreement/");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                request.Content = content;

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    createdOrder = JsonConvert.DeserializeObject<OrderResponseModel>(responseContent);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
            }
            return createdOrder ?? new OrderResponseModel();
        }


        public async Task<OrderResponseModel> CreateOrder2(OrderRequestModel order, string token)
        {
            OrderResponseModel? createdOrder = null;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{Names.BaseURL}/order/agreement/");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(JsonConvert.SerializeObject(order), System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    createdOrder = JsonConvert.DeserializeObject<OrderResponseModel>(content);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
            }
            return createdOrder ?? new OrderResponseModel();
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            bool deleteResponse = false;
            try
            {
                var response = await _httpClient.DeleteAsync($"{Names.BaseURL}/order/{orderId}/delete/");
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

        public async Task<OrderResponseModel> UpdateOrder(OrderRequestModel order, string token, int orderId)
        {
            OrderResponseModel? createdOrder = null;
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(order.name), "name");
                content.Add(new StringContent(order.status), "status");
                content.Add(new StringContent(order.description), "description");
                content.Add(new StringContent(order.tot_price.ToString()), "tot_price");
                content.Add(new StringContent(order.order_number.ToString()), "order_number");
                content.Add(new StringContent(order.address), "address");
                content.Add(new StringContent(order.buyer.ToString()), "buyer");
                content.Add(new StringContent(order.seller.ToString()), "seller");

                if (order.image != null)
                {
                    var imageContent = new ByteArrayContent(order.image);
                    imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                    content.Add(imageContent, "image", "image.jpg");
                }

                var request = new HttpRequestMessage(HttpMethod.Put, $"{Names.BaseURL}/order/update_order/{orderId}/");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                request.Content = content;

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    createdOrder = JsonConvert.DeserializeObject<OrderResponseModel>(responseContent);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
            }
            return createdOrder ?? new OrderResponseModel();
        }
    }
}
