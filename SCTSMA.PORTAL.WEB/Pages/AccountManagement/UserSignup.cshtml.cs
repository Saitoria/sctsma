using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SCTSMA.PORTAL.APPLICATION;
using SCTSMA.PORTAL.DOMAIN.User.CreateUser;

namespace SCTSMA.PORTAL.WEB.Pages.AccountManagement
{
    public class UserSignupModel : PageModel
    {
        public bool isLoading { get; set; } = false;
        public string errorMessage { get; set; } = string.Empty;
        public string otpValue { get; set; } = string.Empty;
        [BindProperty]
        public CreateUserRequestModel? userPost { get; set; } = new CreateUserRequestModel();
        private readonly IUserRepository _userRepository;

        public UserSignupModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnPostSignupUserAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                isLoading = true;
                CreateUserResponseModel signupResponse = await _userRepository.CreateUser(userPost);

                if (signupResponse!=null && signupResponse.data != null)
                {
                    otpValue = signupResponse.data.otp.ToString();
                }

                isLoading = false;
                //return Redirect("~/activateuser");
                return Page();
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
