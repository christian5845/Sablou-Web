using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Chokolader
{
    public class OversigtModel : PageModel
    {
        private readonly Sablou_Web.Models.cralle_dk_db_sablouContext _context;
        private IDataService _repositories;

        public OversigtModel(Sablou_Web.Models.cralle_dk_db_sablouContext context)
        {
            _context = context;
            _repositories = new Dataservice();
            Chokolader = _repositories.ChokoladeRepository.Data.Values.ToList();
        }

        public IList<Chokolade> Chokolader { get;set; } = default!;

        public async Task OnGet()
        {
           
        }
    }
}
