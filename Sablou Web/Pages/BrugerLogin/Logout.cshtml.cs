using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Pages.BrugerLogin;

namespace Sablou_Web.Pages.BrugerLogin;

public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        LoginModel.CurrentBruger = null;

        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToPage("/Forside");
    }
}