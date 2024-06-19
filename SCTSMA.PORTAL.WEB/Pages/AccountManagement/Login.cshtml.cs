using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using SCTSMA.PORTAL.DOMAIN.User;

namespace SCTSMA.PORTAL.WEB.Pages.AccountManagement
{
    public class LoginModel : PageModel
    {
        public bool isLoading { get; set; } = false;
        public string errorMessage { get; set; } = string.Empty;
        [BindProperty]
        public UserModel? userPost { get; set; } = new UserModel();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginUserAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            isLoading = true;
            if (userPost.Username!.Equals("fchilo@gmail.com") && userPost.Password!.Equals("2025"))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userPost.Username),
                new Claim("FullName", "Faith Chilo"),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

                var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {

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
            //return Redirect("~/dashboard");

        }
    }
}
