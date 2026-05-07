using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Chokolader
{
    public class EditModel : PageModel
    {
        private IDataService _repositories;

        public EditModel()
        {
            _repositories = new Dataservice();
        }

        [BindProperty]
        public Chokolade Chokolade { get; set; } = default!;

        public List<SelectListItem> IngrediensValg { get; set; } = new();

        [BindProperty]
        public List<int> ValgteIngrediensIds { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            if (LoginModel.CurrentBruger?.Rolle != "Admin")
            {
                return RedirectToPage("/Forside");
            }
            Chokolade = _repositories.ChokoladeRepository.GetItem(id);

            if (Chokolade == null)
            {
                return NotFound();
            }

            IngrediensValg = _repositories.IngrediensRepository.Data
                .Select(i => new SelectListItem
                {
                    Value = i.Value.Id.ToString(),
                    Text = i.Value.Navn
                })
                .ToList();

            ValgteIngrediensIds = _repositories.IngrediensListeRepository.Data
                .Where(il => il.Value.ChokoladeId == id)
                .Select(il => il.Value.IngrediensId)
                .ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (LoginModel.CurrentBruger?.Rolle != "Admin")
            {
                return RedirectToPage("/Forside");
            }
            if (!ModelState.IsValid)
            {
                IngrediensValg = _repositories.IngrediensRepository.Data
                    .Select(i => new SelectListItem
                    {
                        Value = i.Value.Id.ToString(),
                        Text = i.Value.Navn
                    })
                    .ToList();

                return Page();
            }

            _repositories.ChokoladeRepository.Update(Chokolade);

            var gamleIngredienser = _repositories.IngrediensListeRepository.Data
                .Where(il => il.Value.ChokoladeId == Chokolade.Id)
                .Select(il => il.Value.Id)
                .ToList();

            foreach (var id in gamleIngredienser)
            {
                _repositories.IngrediensListeRepository.Delete(id);
            }

            foreach (var ingrediensId in ValgteIngrediensIds)
            {
                _repositories.IngrediensListeRepository.Create(
                    new IngrediensListe(Chokolade.Id, ingrediensId)
                );
            }

            return RedirectToPage("./Index");
        }
    }
}