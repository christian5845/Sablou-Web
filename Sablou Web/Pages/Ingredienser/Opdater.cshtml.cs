using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;
using System.ComponentModel.DataAnnotations;

namespace Sablou_Web.Pages.Ingredienser
{
    public class OpdaterModel : PageModel
    {
        private readonly IDataService _data;

        public OpdaterModel(IDataService data)
        {
            _data = data;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Navn skal udfyldes")]
        [StringLength(30, ErrorMessage = "Navn må højst være 30 tegn")]
        public string Navn { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Beskrivelse skal udfyldes")]
        [StringLength(100, ErrorMessage = "Beskrivelse må højst være 100 tegn")]
        public string Beskrivelse { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            // Kun admins må åbne siden
            if (LoginModel.CurrentBruger?.Rolle != "Admin")
            {
                return RedirectToPage("/Forside");
            }

            // Henter ingrediensen ud fra det id, der kommer fra knappen/linket
            Ingrediens? ingrediens = _data.IngrediensRepository.GetItem(id);

            if (ingrediens == null)
            {
                return NotFound();
            }

            // Fylder formularen med de nuværende værdier
            Id = ingrediens.Id;
            Navn = ingrediens.Navn;
            Beskrivelse = ingrediens.Beskrivelse;

            return Page();
        }

        public IActionResult OnPost()
        {
            // Kun admins må gemme ændringer
            if (LoginModel.CurrentBruger?.Rolle != "Admin")
            {
                return RedirectToPage("/Forside");
            }

            // Tjekker validation
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Finder ingrediensen igen
            Ingrediens? ingrediens = _data.IngrediensRepository.GetItem(Id);

            if (ingrediens == null)
            {
                return NotFound();
            }

            // Opdaterer kun selve ingrediensen
            ingrediens.Navn = Navn;
            ingrediens.Beskrivelse = Beskrivelse;

            // Gemmer ændringer i databasen
            _data.IngrediensRepository.Update(ingrediens);

            // Besked efter succesfuld opdatering
            TempData["SuccessMessage"] = "Ingrediensen blev opdateret.";

            return RedirectToPage("/Ingredienser/Alle");
        }
    }
}