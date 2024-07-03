using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.DOMAIN.User.ActivateUser;

namespace SCTSMA.PORTAL.WEB.Pages.AccountManagement
{
    public class ActivateUserModel : PageModel
    {
        public bool isLoading { get; set; } = false;
        public string errorMessage { get; set; } = string.Empty;
        public string otpValue { get; set; } = string.Empty;
        [BindProperty]
        public ActivateUserRequestModel? userPost { get; set; } = new ActivateUserRequestModel();

        private readonly IUserRepository _userRepository;
        public ActivateUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnPostActivateUserAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                isLoading = true;
                ActivateUserResponseModel activateResponse = await _userRepository.ActivateUser(userPost);

                if (activateResponse != null && activateResponse.message != null)
                {
                    isLoading = false;
                    return Redirect("~/login");
                    
                }
                else
                {
                    return Page();
                }

                
            }
            catch (Exception ex)
            {
                errorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
            finally
            {
                isLoading = false;
            }
        }

    }
}
