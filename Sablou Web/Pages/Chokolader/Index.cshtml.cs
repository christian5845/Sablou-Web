using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;

namespace Sablou_Web.Pages.Chokolader
{
    public class IndexModel : PageModel
    {
        private readonly Sablou_Web.Models.cralle_dk_db_sablouContext _context;

        public IndexModel(Sablou_Web.Models.cralle_dk_db_sablouContext context)
        {
            _context = context;
        }

        public IList<Models.Chokolade> Chokolade { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Chokolade = await _context.Chokolade.ToListAsync();
        }
    }
}
