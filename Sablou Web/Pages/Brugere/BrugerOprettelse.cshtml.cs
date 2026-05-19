using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sablou_Web.Models;
using Sablou_Web.Services;
using Sablou_Web.Services.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Sablou_Web.Pages.Brugere
{

   
    public class BrugerOprettelseModel : PageModel
    {
        private IDataService _repo;

        [BindProperty]
        public string Navn { get; set; }


        [BindProperty]
        //Validerer om emailen ligner en rigtig email.
        [Required(ErrorMessage = "Email skal udfyldes")]
        [EmailAddress(ErrorMessage = "Indtast en gyldig emailadresse")]
        public string Email { get; set; }

        [BindProperty]
        public string Adresse { get; set; }

        [BindProperty]
        public int? Telefonnummer { get; set; }

        [BindProperty]
        public string Adgangskode { get; set; }

        public BrugerOprettelseModel(IDataService repo)
        {
            _repo = repo;          
        }

        public IActionResult OnPost()
        {
            // Tjek om det indtastede data er validt
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Send data videre til repository
                _repo.BrugerRepository.Create(new Bruger(Navn, Email, Adresse, Telefonnummer, Adgangskode));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(Email), ex.Message);
                return Page();
            }

            // Vend tilbage til startsiden
            return RedirectToPage("/Forside");
        }
    }

}

