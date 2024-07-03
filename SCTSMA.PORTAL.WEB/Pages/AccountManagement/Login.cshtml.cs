using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using SCTSMA.PORTAL.APPLICATION;
using System.IdentityModel.Tokens.Jwt;
using SCTSMA.PORTAL.DOMAIN.User.LoginUser;

namespace SCTSMA.PORTAL.WEB.Pages.AccountManagement
{
    public class LoginModel : PageModel
    {
        public bool isLoading { get; set; } = false;
        public string errorMessage { get; set; } = string.Empty;
        [BindProperty]
        //public CreateUserModel? userPost { get; set; } = new CreateUserModel();
        public LoginRequestModel? userPost { get; set; } = new LoginRequestModel();
        private readonly IUserRepository _userRepository;

        public LoginModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnPostLoginUserAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                isLoading = true;
                LoginResponseModel loginResponse = await _userRepository.LoginUser(userPost);

                if (loginResponse.access != null && loginResponse.refresh != null)
                {
                    var handler = new JwtSecurityTokenHandler();

                    var accessToken = handler.ReadJwtToken(loginResponse.access);
                    var refreshToken = handler.ReadJwtToken(loginResponse.refresh);

                    var userId = accessToken.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;

                    if (string.IsNullOrEmpty(userId))
                    {
                        throw new Exception("User ID not found in the access token");
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userPost.username),
                        new Claim("userid", userId),
                        new Claim("access_token", loginResponse.access),
                        new Claim("refresh_token", loginResponse.refresh)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    isLoading = false;
                    return Redirect("~/");
                }
                else
                {
                    errorMessage = "Please fill in the correct details";
                    isLoading = false;
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
