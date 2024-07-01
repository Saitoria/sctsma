using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.DOMAIN.Dispute;
using SCTSMA.PORTAL.DOMAIN.Order;

namespace SCTSMA.PORTAL.SERVICES.DisputeManagement
{
    public class DisputeManagementBase: ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IDisputeRepository _disputeRepository { get; set; }
        public IEnumerable<GetDisputesResponseModel> Disputes { get; set; } = Enumerable.Empty<GetDisputesResponseModel>();
        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }
        public CreateDisputeRequestModel disputePost { get; set; } = new CreateDisputeRequestModel();
        //General notifiers
        public bool isLoading { get; set; } = false;
        public string errorMessage { get; set; } = string.Empty;
        public string userAccessToken = string.Empty;
        //Add Order notifiers
        public bool isAddDisputeLoading { get; set; } = false;
        public string addDisputeErrorMessage { get; set; } = string.Empty;

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
                List<GetDisputesResponseModel> disputes= await _disputeRepository.GetAllDisputes();
                Disputes = disputes;

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
            //orderPost = new OrderRequestModel();
            isLoading = false;
            errorMessage = string.Empty;
            isAddDisputeLoading = false;
            addDisputeErrorMessage = string.Empty;
            //NavigationManager.NavigateTo("/ordermanagement", forceLoad: true);
        }

        public async Task AddDispute()
        {
            try
            {
                isAddDisputeLoading = true;
                addDisputeErrorMessage = string.Empty;
                string orderJson = JsonConvert.SerializeObject(disputePost, Formatting.Indented);
                Console.WriteLine("Sending Order JSON: " + orderJson);
                CreateDisputeResponseModel response = await _disputeRepository.CreateDispute(disputePost);
                if (response.id != null && response.id != 0)
                {
                    errorMessage = string.Empty;
                    NavigationManager.NavigateTo("/disputemanagement", forceLoad: true);
                }
                else
                {
                    addDisputeErrorMessage = "Failed to add dispute";
                }
            }
            catch (Exception ex)
            {
                addDisputeErrorMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                isAddDisputeLoading = false;
            }
        }

    }
}
