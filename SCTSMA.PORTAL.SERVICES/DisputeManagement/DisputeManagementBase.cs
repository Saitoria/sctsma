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
        //Add Dispute notifiers
        public bool isAddDisputeLoading { get; set; } = false;
        public string addDisputeErrorMessage { get; set; } = string.Empty;
        //Update Dispute notifiers
        public bool isUpdateDisputeLoading { get; set; } = false;
        public string updateDisputeErrorMessage { get; set; } = string.Empty;
        public string imagePath { get; set; } = string.Empty ;
        //Delete Dispute notifiers
        public bool isDeleteDisputeLoading { get; set; } = false;
        public string deleteDisputeErrorMessage { get; set; } = string.Empty;

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
            isUpdateDisputeLoading = false;
            updateDisputeErrorMessage = string.Empty;
            imagePath = string.Empty ;
            isDeleteDisputeLoading = false;
            deleteDisputeErrorMessage = string.Empty;
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
                    addDisputeErrorMessage = string.Empty;
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

        public async Task UpdateDispute()
        {
            try
            {
                isUpdateDisputeLoading = true;
                updateDisputeErrorMessage = string.Empty;
                string orderJson = JsonConvert.SerializeObject(disputePost, Formatting.Indented);
                Console.WriteLine("Sending Order JSON: " + orderJson);
                CreateDisputeResponseModel response = await _disputeRepository.UpdateDispute(disputePost,disputePost.id.Value);
                if (response.id != null && response.id != 0)
                {
                    updateDisputeErrorMessage = string.Empty;
                    NavigationManager.NavigateTo("/disputemanagement", forceLoad: true);
                }
                else
                {
                    updateDisputeErrorMessage = "Failed to update dispute";
                }
            }
            catch (Exception ex)
            {
                updateDisputeErrorMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                isUpdateDisputeLoading = false;
            }
        }

        public async Task DeleteDispute()
        {
            try
            {
                isDeleteDisputeLoading = true;
                deleteDisputeErrorMessage = string.Empty;
                string orderJson = JsonConvert.SerializeObject(disputePost, Formatting.Indented);
                Console.WriteLine("Sending Order JSON: " + orderJson);
                bool response = await _disputeRepository.DeleteDispute(disputePost.id.Value);
                if (response!= null && response != false)
                {
                    deleteDisputeErrorMessage = string.Empty;
                    NavigationManager.NavigateTo("/disputemanagement", forceLoad: true);
                }
                else
                {
                    deleteDisputeErrorMessage = "Failed to delete dispute";
                }
            }
            catch (Exception ex)
            {
                deleteDisputeErrorMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                isDeleteDisputeLoading = false;
            }
        }

        public async void GetSingleDisputeById(int disputeId)
        {
            isUpdateDisputeLoading = false;
            var dispute = Disputes.FirstOrDefault(u => u.id == disputeId);
            if (dispute != null)
            {
                // Map the properties to kioskpost if kiosk is found
                disputePost.id = dispute.id;
                disputePost.description = dispute.description;
                disputePost.status = dispute.status.Value;
                disputePost.order = dispute.order;
                imagePath = dispute.image;
            }
            else
            {

            }
            isUpdateDisputeLoading = false;
            updateDisputeErrorMessage = string.Empty;
        }

    }
}
