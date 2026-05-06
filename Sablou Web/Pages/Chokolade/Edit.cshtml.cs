using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;

namespace Sablou_Web.Pages.Chokolade
{
    public class EditModel : PageModel
    {
        private readonly Sablou_Web.Models.cralle_dk_db_sablouContext _context;

        public EditModel(Sablou_Web.Models.cralle_dk_db_sablouContext context)
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

            var chokolade =  await _context.Chokolade.FirstOrDefaultAsync(m => m.Id == id);
            if (chokolade == null)
            {
                return NotFound();
            }
            Chokolade = chokolade;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Chokolade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChokoladeExists(Chokolade.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ChokoladeExists(int id)
        {
            return _context.Chokolade.Any(e => e.Id == id);
        }
    }
}
