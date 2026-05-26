using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Chokolader;

public class SletModel : PageModel
{
    private IDataService _repositories;

    public SletModel(IDataService repo)
    {
        _repositories = repo;
    }

    [BindProperty]
    public Chokolade Chokolade { get; set; } = default!;

    public IActionResult OnGet(int? id)
    {
        if (LoginModel.CurrentBruger?.Rolle != "Admin")
        {
            return RedirectToPage("/BrugerLogin/AccessDenied");
        }

        if (id == null)
        {
            return NotFound();
        }

        Chokolade = _repositories.ChokoladeRepository.GetItem(id.Value);

        if (Chokolade == null)
        {
            return NotFound();
        }

        return Page();
    }

    public IActionResult OnPost(int? id)
    {
        if (LoginModel.CurrentBruger?.Rolle != "Admin")
        {
            return RedirectToPage("/Forside");
        }

        if (id == null)
        {
            return NotFound();
        }

        var ingrediensListeIds = _repositories.IngrediensListeRepository.Data
            .Where(il => il.Value.ChokoladeId == id.Value)
            .Select(il => il.Value.Id)
            .ToList();
        var chokoladerIKatalogIdListe = _repositories.ChokoladerIKatalogRepository.Data
            .Where(il => il.Value.ChokoladeId == id.Value)
            .Select(il => il.Value.Id)
            .ToList();


        foreach (var katalogId in chokoladerIKatalogIdListe)
        {
            _repositories.ChokoladerIKatalogRepository.Delete(katalogId);
        }

        foreach (var ingrediensListeId in ingrediensListeIds)
        {
            _repositories.IngrediensListeRepository.Delete(ingrediensListeId);
        }
      


        _repositories.ChokoladeRepository.Delete(id.Value);

        return RedirectToPage("./Oversigt");
    }
}