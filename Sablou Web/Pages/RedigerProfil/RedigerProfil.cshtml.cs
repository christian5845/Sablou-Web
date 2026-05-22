using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Services;
using Sablou_Web.Pages.BrugerLogin;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sablou_Web.Pages.Brugere;

public class RedigerProfilModel : PageModel
{
    private readonly IDataService _repo;

    [BindProperty]
    [Required(ErrorMessage = "Navn skal udfyldes")]
    public string Navn { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Email skal udfyldes")]
    [EmailAddress(ErrorMessage = "Ugyldig email")]
    public string Email { get; set; }

    [BindProperty]
    public int? Telefonnummer { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Adresse skal udfyldes")]
    public string Adresse { get; set; }

    public string? SuccessMessage { get; set; }

    public RedigerProfilModel(IDataService repo)
    {
        _repo = repo;
    }

    public IActionResult OnGet()
    {
        // Kun loggede brugere må redigere deres profil
        if (LoginModel.CurrentBruger == null)
        {
            return RedirectToPage("/Forside");
        }

        // Hent den loggede bruger
        Bruger bruger = LoginModel.CurrentBruger;

        // Fyld formularen med brugerens nuværende oplysninger
        Navn = bruger.Navn;
        Email = bruger.Email;
        Telefonnummer = bruger.Telefonnummer;
        Adresse = bruger.Adresse;

        return Page();
    }

    public IActionResult OnPost()
    {
        // Kun loggede brugere må redigere deres profil
        if (LoginModel.CurrentBruger == null)
        {
            return RedirectToPage("/Forside");
        }

        // Tjekker validation fx gyldig email
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Hent den rigtige bruger fra databasen/repository via id
        Bruger? bruger = _repo.BrugerRepository.GetItem(LoginModel.CurrentBruger.Id);

        if (bruger == null)
        {
            return RedirectToPage("/Forside");
        }

        // Tjek om den nye email allerede bruges af en anden bruger
        bool emailExists = _repo.BrugerRepository.Data.Values
            .Any(b => b.Email.Equals(Email, StringComparison.OrdinalIgnoreCase)
                   && b.Id != bruger.Id);

        if (emailExists)
        {
            ModelState.AddModelError(nameof(Email), "E-mailen bruges allerede af en anden bruger.");
            return Page();
        }

        // Opdater kun den loggede brugers egne informationer
        bruger.Navn = Navn;
        bruger.Email = Email;
        bruger.Telefonnummer = Telefonnummer;
        bruger.Adresse = Adresse;

        _repo.BrugerRepository.Update(bruger);

        // Opdater CurrentBruger, så navbar/min side viser de nye oplysninger
        LoginModel.CurrentBruger = bruger;

        SuccessMessage = "Din profil blev opdateret.";

        return Page();
    }
}