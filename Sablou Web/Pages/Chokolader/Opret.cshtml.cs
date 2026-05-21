using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Chokolader;

[Authorize(Roles = "Admin")]
public class OpretModel : PageModel
{
    public  IDataService Repo { get; }
    
    public OpretModel(IDataService dataService)
    {
        Repo = dataService;
    }

    [BindProperty]
    public Chokolade Chokolade { get; set; } = default!;

    public List<SelectListItem> IngrediensValg { get; set; } = new();

    [BindProperty]
    public List<int> ValgteIngrediensIds { get; set; } = new();

    public IActionResult OnGet()
    {
        if (LoginModel.CurrentBruger?.Rolle != "Admin")
        {
            return RedirectToPage("/Forside");
        }
        IngrediensValg = Repo.IngrediensRepository.Data.Values
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
        if (LoginModel.CurrentBruger?.Rolle != "Admin")
        {
            return RedirectToPage("/Forside");
        }
        if (!ModelState.IsValid)
        {
            IngrediensValg = Repo.IngrediensRepository.Data.Values
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Navn
                })
                .ToList();

            return Page();
        }

        Repo.ChokoladeRepository.Create(Chokolade);

        foreach (var id in ValgteIngrediensIds)
        {
            Repo.IngrediensListeRepository.Create(new IngrediensListe(Chokolade.Id, id));
        }

        return RedirectToPage("./Oversigt");
    }
}