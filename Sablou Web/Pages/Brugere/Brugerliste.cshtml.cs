using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;
using Sablou_Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sablou_Web.Pages.Brugere
{
    [Authorize(Roles = "Admin")]
    public class BrugerlisteModel : PageModel
    {
        private IDataService _repo;

        public List<Bruger> Brugere { get; set; } = new List<Bruger>();

        public BrugerlisteModel(IDataService repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
            // Hent alle brugere fra repository og sorter efter navn
            Brugere = _repo.BrugerRepository.Data.Values
                .OrderBy(b => b.Navn)
                .ToList();
        }
    }
}
