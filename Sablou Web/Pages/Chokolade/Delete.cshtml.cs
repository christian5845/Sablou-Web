using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;

namespace Sablou_Web.Pages.Chokolade
{
    public class DeleteModel : PageModel
    {
        private readonly Sablou_Web.Models.cralle_dk_db_sablouContext _context;

        public DeleteModel(Sablou_Web.Models.cralle_dk_db_sablouContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Chokolade Chokolade { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chokolade = await _context.Chokolade.FirstOrDefaultAsync(m => m.Id == id);

            if (chokolade is not null)
            {
                Chokolade = chokolade;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chokolade = await _context.Chokolade.FindAsync(id);
            if (chokolade != null)
            {
                Chokolade = chokolade;
                _context.Chokolade.Remove(Chokolade);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
