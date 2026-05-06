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
    public class DetailsModel : PageModel
    {
        private readonly Sablou_Web.Models.cralle_dk_db_sablouContext _context;

        public DetailsModel(Sablou_Web.Models.cralle_dk_db_sablouContext context)
        {
            _context = context;
        }

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
    }
}
