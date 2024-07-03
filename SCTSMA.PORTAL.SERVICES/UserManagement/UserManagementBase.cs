using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.DOMAIN.Order;
using SCTSMA.PORTAL.DOMAIN.User.ListUser;

namespace SCTSMA.PORTAL.SERVICES.UserManagement
{
    public class UserManagementBase: ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IUserRepository _userRepository { get; set; }
        public IEnumerable<ListUserResponseModel> Users { get; set; } = Enumerable.Empty<ListUserResponseModel>();
        //General notifiers
        public bool isLoading { get; set; } = false;
        public string errorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isLoading = true;
                errorMessage = string.Empty;
                List<ListUserResponseModel> users = await _userRepository.GetAllUsers();
                Users = users;  

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


    }
}
