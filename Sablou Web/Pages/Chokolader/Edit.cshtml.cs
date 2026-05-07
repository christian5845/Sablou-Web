using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;

namespace Sablou_Web.Pages.Chokolader
{
    public class EditModel : PageModel
    {
        private readonly cralle_dk_db_sablouContext _context;

        public EditModel(cralle_dk_db_sablouContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Chokolade Chokolade { get; set; } = default!;

        public List<SelectListItem> IngrediensValg { get; set; } = new();

        [BindProperty]
        public List<int> ValgteIngrediensIds { get; set; } = new();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            Chokolade = _context.Chokolade
                .Include(c => c.Ingrediens)
                .FirstOrDefault(c => c.Id == id);

            if (Chokolade == null)
                return NotFound();

            IngrediensValg = _context.Ingrediens
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Navn
                })
                .ToList();

            ValgteIngrediensIds = Chokolade.Ingrediens
                .Select(i => i.Id)
                .ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            var chokoladeFraDb = _context.Chokolade
                .Include(c => c.Ingrediens)
                .FirstOrDefault(c => c.Id == Chokolade.Id);

            if (chokoladeFraDb == null)
                return NotFound();

            chokoladeFraDb.Navn = Chokolade.Navn;
            chokoladeFraDb.Stykpris = Chokolade.Stykpris;
            chokoladeFraDb.Beskrivelse = Chokolade.Beskrivelse;

            chokoladeFraDb.Ingrediens.Clear();

            var valgteIngredienser = _context.Ingrediens
                .Where(i => ValgteIngrediensIds.Contains(i.Id))
                .ToList();

            foreach (var ingrediens in valgteIngredienser)
            {
                chokoladeFraDb.Ingrediens.Add(ingrediens);
            }

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}