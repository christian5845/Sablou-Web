using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Pages.BrugerLogin
{
    public class LoginModel : PageModel
    {
        private IBrugerRepository _brugerRepository;

        public static Bruger? CurrentBruger { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel(IBrugerRepository brugerRepository)
        {
            _brugerRepository = brugerRepository;
        }

        public async Task<IActionResult> OnPost()
        {
            // CurrentUser = _brugerRepository.VerifyUser(Email, Password);

            VerifyUser(Email, Password);

            if (CurrentBruger == null)
            {
                ErrorMessage = "Kunne ikke logge ind";
                return Page();
            }

            // Log ind
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                BuildClaimsPrincipal(CurrentBruger));

            return RedirectToPage("/Index");
        }

        private ClaimsPrincipal BuildClaimsPrincipal(Bruger bruger)
        {
            // Opbyg Claims-liste
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, bruger.Email));
            claims.Add(new Claim(ClaimTypes.Role, bruger.Rolle));

            // Opret ClaimsIdentity (claims plus Authentication-strategi)
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Opret endeligt ClaimsPrincipal-objekt
            return new ClaimsPrincipal(claimsIdentity);
        }

        public Bruger? VerifyBruger(string providedBrugerEmail, string providedPassword)
        {
            // List<Bruger> Bruger = All;

            List<Bruger> brugere = new List<Bruger>();

            using cralle_dk_db_sablouContext context = new cralle_dk_db_sablouContext();

            brugere = context.Bruger.ToList();


            Bruger? bruger = brugere.FirstOrDefault(u => u.Email == providedBrugerEmail &&
                                                   u.Password == providedPassword);

            return bruger;
        }
    }
}
