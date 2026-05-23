using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Services;
using System.Security.Claims;

namespace Sablou_Web.Pages.RedigerProfil;

[Authorize]
public class MinSideModel : PageModel
{
    private readonly IDataService _repo;

    public Bruger Bruger { get; set; }

    public MinSideModel(IDataService repo)
    {
        _repo = repo;
    }

    public IActionResult OnGet()
    {
        // Henter email fra den bruger, der er logget ind via cookie/claims
        string? email = User.FindFirstValue(ClaimTypes.Email);

        // Hvis der ikke findes en email claim, sendes brugeren til forsiden
        if (string.IsNullOrWhiteSpace(email))
        {
            return RedirectToPage("/Forside");
        }

        // Finder brugeren i databasen/repository ud fra emailen
        Bruger? fundetBruger = _repo.BrugerRepository.Data.Values
            .FirstOrDefault(b => b.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        // Hvis brugeren ikke findes i databasen, sendes personen til forsiden
        if (fundetBruger == null)
        {
            return RedirectToPage("/Forside");
        }

        // Gemmer den fundne bruger, så cshtml-siden kan vise oplysningerne
        Bruger = fundetBruger;

        return Page();
    }
}