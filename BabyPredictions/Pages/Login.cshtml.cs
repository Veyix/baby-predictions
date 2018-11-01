using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace BabyPredictions.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AdminSettings _settings;

        public LoginModel(IOptions<AdminSettings> settings)
        {
            _settings = settings.Value;
        }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a valid authentication code")]
        public string AdminCode { get; set; }

        public ActionResult OnPost()
        {
            if (!IsValidCode())
            {
                ModelState.AddModelError(nameof(AdminCode), "The admin code you have entered is not valid");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string returnUrl = "/index";
            if (Request.Query.TryGetValue("ReturnUrl", out var values))
            {
                returnUrl = values.First();
            }

            Login(returnUrl);

            return LocalRedirect(returnUrl);
        }

        private bool IsValidCode() =>
            !String.IsNullOrWhiteSpace(AdminCode) && AdminCode == _settings.AdminCode;

        private void Login(string redirectUri)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "Admin"));
            identity.AddClaim(new Claim(ClaimTypes.Name, "Admin"));

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUri,
                ExpiresUtc = DateTime.Now.AddDays(1),
                IsPersistent = true
            };

            SignIn(principal, properties, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
