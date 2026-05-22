using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;
using Sablou_Web.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client;

namespace Sablou_Web.Pages.Brugere;

[Authorize(Roles = "Admin")]
public class BrugerlisteModel : PageModel
{
    private IDataService _repo;

    public List<Bruger> Brugere { get; set; } = new List<Bruger>();

    [BindProperty]
    public string SearchString { get; set; }

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
    public IActionResult OnPostNameSearch()
    {
        Brugere = _repo.BrugerRepository.Data.Values
            .Where(b => b.Email.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
            .OrderBy(b => b.Email)
            .ToList();
        return Page();
    }
    public IActionResult OnPostSkiftRolle(int id)
    {
        Bruger? bruger = _repo.BrugerRepository.GetItem(id);

        if (bruger == null)
        {
            return NotFound();
        }

        if (bruger.Rolle == "Admin")
        {
            bruger.Rolle = "Kunde";
        }
        else
        {
            bruger.Rolle = "Admin";
        }

        _repo.BrugerRepository.Update(bruger);

        return RedirectToPage();
    }
    public IActionResult OnPostSlet(int id)
    {
        Bruger? bruger = _repo.BrugerRepository.GetItem(id);
        if (bruger == null)
        {
            return NotFound();
        }
        _repo.BrugerRepository.Delete(id);
        return RedirectToPage();
    }
}
