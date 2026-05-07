using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;

namespace Sablou_Web.Pages.Chokolader
{
    public class CreateModel : PageModel
    {
        private readonly cralle_dk_db_sablouContext _context;

        public CreateModel(cralle_dk_db_sablouContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Chokolade Chokolade { get; set; } = default!;

        public List<SelectListItem> IngrediensValg { get; set; } = new();

        [BindProperty]
        public List<int> ValgteIngrediensIds { get; set; } = new();

        public IActionResult OnGet()
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

        public async Task<IActionResult> OnPostAsync()
        {
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

            var valgteIngredienser = await _context.Ingrediens
                .Where(i => ValgteIngrediensIds.Contains(i.Id))
                .ToListAsync();

            Chokolade.Ingrediens = valgteIngredienser;

            _context.Chokolade.Add(Chokolade);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}