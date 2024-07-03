using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.CONFIG;
using SCTSMA.PORTAL.DOMAIN.Order;
using System.Net.Http;


namespace SCTSMA.PORTAL.SERVICES.OrderManagement
{
    public class OrderManagementBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IOrderRepository _orderRepository { get; set; }
        public IEnumerable<OrderResponseModel> Orders { get; set; } = Enumerable.Empty<OrderResponseModel>();
        public OrderRequestModel orderPost { get; set; } = new OrderRequestModel();
        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }

        //General notifiers
        public bool isLoading { get; set; } = false;
        public string errorMessage { get; set; } = string.Empty;
        //Add Order notifiers
        public bool isAddOrderLoading { get; set; } = false;
        public string addOrderErrorMessage { get; set; } = string.Empty;
        public string userAccessToken = string.Empty ;
        //Add Order notifiers
        public bool isUpdateOrderLoading { get; set; } = false;
        public string updateOrderErrorMessage { get; set; } = string.Empty;
        //Delete Order notifiers
        public bool isDeleteOrderLoading { get; set; } = false;
        public string deleteOrderErrorMessage { get; set; } = string.Empty;
        public string imagePath { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isLoading = true;
                errorMessage = string.Empty;
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                if (authState != null)
                {
                    var user = authState.User;
                    var userClaims = user.Claims.ToList();
                    var accessToken = userClaims.FirstOrDefault(c => c.Type == "access_token")?.Value;
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        userAccessToken = accessToken.Trim();   
                    }
                }
                // Initialize Orders with 10 sample Order data
                List<OrderResponseModel> orders = await _orderRepository.GetAllOrders();
                Orders = orders;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                isLoading = false;
            }
        }

        public void OnClose()
        {
            orderPost = new OrderRequestModel();
            isLoading = false;
            errorMessage = string.Empty;
            isAddOrderLoading = false;
            addOrderErrorMessage = string.Empty;
            isUpdateOrderLoading = false;
            updateOrderErrorMessage = string.Empty;
            isDeleteOrderLoading = false;
            deleteOrderErrorMessage = string.Empty;
            //NavigationManager.NavigateTo("/ordermanagement", forceLoad: true);
        }

        public async Task AddOrder()
        {
            try
            {
                isAddOrderLoading = true;
                addOrderErrorMessage = string.Empty;
                string orderJson = JsonConvert.SerializeObject(orderPost, Formatting.Indented);
                Console.WriteLine("Sending Order JSON: " + orderJson);
                OrderResponseModel response = await _orderRepository.CreateOrder(orderPost, userAccessToken);
                if (response.id != null)
                {
                    errorMessage = string.Empty;
                    NavigationManager.NavigateTo("/ordermanagement", forceLoad: true);
                }
                else
                {
                    addOrderErrorMessage = "Failed to add order";
                }
            }
            catch (Exception ex)
            {
                addOrderErrorMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                isAddOrderLoading = false;
            }
        }

        public async Task UpdateOrder()
        {
            try
            {
                isUpdateOrderLoading = true;
                updateOrderErrorMessage = string.Empty;
                string orderJson = JsonConvert.SerializeObject(orderPost, Formatting.Indented);
                Console.WriteLine("Sending Order JSON: " + orderJson);
                OrderResponseModel response = await _orderRepository.UpdateOrder(orderPost, userAccessToken, orderPost.id.Value);
                if (response.id != null)
                {
                    updateOrderErrorMessage = string.Empty;
                    NavigationManager.NavigateTo("/ordermanagement", forceLoad: true);
                }
                else
                {
                    updateOrderErrorMessage = "Failed to update order";
                }
            }
            catch (Exception ex)
            {
                updateOrderErrorMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                isUpdateOrderLoading = false;
            }
        }

        public async Task DeleteOrder()
        {
            try
            {
                isDeleteOrderLoading = true;
                deleteOrderErrorMessage = string.Empty;
                string orderJson = JsonConvert.SerializeObject(orderPost, Formatting.Indented);
                Console.WriteLine("Sending Order JSON: " + orderJson);
                bool response = await _orderRepository.DeleteOrder(orderPost.id.Value);
                if (response != null && response != false)
                {
                    deleteOrderErrorMessage = string.Empty;
                    NavigationManager.NavigateTo("/ordermanagement", forceLoad: true);
                }
                else
                {
                    deleteOrderErrorMessage = "Failed to delete order";
                }
            }
            catch (Exception ex)
            {
                deleteOrderErrorMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                isDeleteOrderLoading = false;
            }
        }

        public async void GetSingleOrderById(int orderId)
        {
            isUpdateOrderLoading = false;
            var order = Orders.FirstOrDefault(u => u.id == orderId);
            if (order != null)
            {
                // Map the properties to kioskpost if kiosk is found
                orderPost.id = order.id;
                orderPost.name = order.name;
                orderPost.description = order.description;
                orderPost.status = order.status;
                orderPost.tot_price = order.tot_price;
                orderPost.buyer = order.buyer;
                orderPost.seller = order.seller;
                orderPost.address = order.address;
                orderPost.order_number = order.order_number;
                imagePath = order.image;
            }
            else
            {

            }
            isUpdateOrderLoading = false;
            updateOrderErrorMessage = string.Empty;
        }

    }
}
