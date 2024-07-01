﻿using Microsoft.AspNetCore.Components;
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
    }
}
