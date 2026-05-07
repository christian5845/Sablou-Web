using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Chokolader
{
    public class EditModel : PageModel
    {
        private IDataService _repositories;
        private cralle_dk_db_sablouContext _context;

        public EditModel(cralle_dk_db_sablouContext context)
        {
            _context = context;
            _repositories = new Dataservice();
        }

        [BindProperty]
        public Models.Chokolade Chokolade { get; set; } = default!;

        public List<SelectListItem> IngrediensValg { get; set; } = new();

        [BindProperty]
        public List<int> ValgteIngrediensIds { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            if (id == null)
                return NotFound();

            Chokolade = _repositories.ChokoladeRepository.GetItem(id);

            if (Chokolade == null)
                return NotFound();

            IngrediensValg = _repositories.IngrediensRepository.Data
                .Select(i => new SelectListItem
                {
                    Value = i.Value.Id.ToString(),
                    Text = i.Value.Navn
                })
                .ToList();

            ValgteIngrediensIds = _repositories.IngrediensRepository.Data
                
                .Select(i => i.Key)
                .ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            //    var chokoladeFraDb = _context.Chokolade
            //        .Include(c => c.Ingrediens)
            //        .FirstOrDefault(c => c.Id == Chokolade.Id);

            //    if (chokoladeFraDb == null)
            //        return NotFound();

            //    chokoladeFraDb.Navn = Chokolade.Navn;
            //    chokoladeFraDb.Stykpris = Chokolade.Stykpris;
            //    chokoladeFraDb.Beskrivelse = Chokolade.Beskrivelse;

            //    chokoladeFraDb.Ingrediens.Clear();

            //    var valgteIngredienser = _context.Ingrediens
            //        .Where(i => ValgteIngrediensIds.Contains(i.Id))
            //        .ToList();

            //    foreach (var ingrediens in valgteIngredienser)
            //    {
            //        chokoladeFraDb.Ingrediens.Add(ingrediens);
            //    }



            //    _repositories.ChokoladeRepository.Create(Chokolade);

            return RedirectToPage("./Index");
        }
    }
}