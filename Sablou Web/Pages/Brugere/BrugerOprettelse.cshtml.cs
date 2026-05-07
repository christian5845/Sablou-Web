using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sablou_Web.Models;
using Sablou_Web.Services;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Pages.Brugere
{

   
    public class BrugerOprettelseModel : PageModel
    {
        private IDataService _repo;

        [BindProperty]
        public Bruger Element { get; set; } = new Bruger();

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

            // Send data videre til repository
            _repo.BrugerRepository.Create(Element);

            // Vend tilbage til startsiden
            return RedirectToPage("/Forside");
        }
    }

}

