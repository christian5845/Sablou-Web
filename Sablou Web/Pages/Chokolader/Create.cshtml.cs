using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Chokolader
{
    public class CreateModel : PageModel
    {
        private IDataService _repositories;
        private readonly cralle_dk_db_sablouContext _context;



        public CreateModel(cralle_dk_db_sablouContext context)
        {
            _context = context;
            _repositories = new Dataservice();
        }

        [BindProperty]
        public Models.Chokolade Chokolade { get; set; } = default!;

        public List<SelectListItem> IngrediensValg { get; set; } = new();

        [BindProperty]
        public List<int> ValgteIngrediensIds { get; set; } = new();

        public IActionResult OnGet()
        {
            if (LoginModel.CurrentBruger?.Rolle != "Admin")
            {
                return RedirectToPage("/Forside");
            }
            IngrediensValg = _context.Ingrediens
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Navn
                })
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (LoginModel.CurrentBruger?.Rolle != "Admin")
            {
                return RedirectToPage("/Forside");
            }
            if (!ModelState.IsValid)
            {
                IngrediensValg = _context.Ingrediens
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Navn
                    })
                    .ToList();

                return Page();
            }

            _repositories.ChokoladeRepository.Create(Chokolade);

            foreach (var id in ValgteIngrediensIds)
            {
                _repositories.IngrediensListeRepository.Create(new IngrediensListe(Chokolade.Id, id));
            }

            return RedirectToPage("./Index");
        }
    }
}