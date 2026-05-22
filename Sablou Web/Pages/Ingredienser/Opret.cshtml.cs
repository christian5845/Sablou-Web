using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Ingredienser;
[Authorize(Roles = "Admin")]
public class OpretModel : PageModel
{
    private IDataService _repositories;

    [BindProperty]
    public string Navn { get; set; }
    [BindProperty]
    public string Beskrivelse { get; set; }
    [BindProperty]
    public int Antal { get; set; }

    public OpretModel(IDataService repo)
    {
        _repositories = repo;
    }

    public IActionResult OnPost()
    {
        if(ModelState.IsValid == false)
        {
           return Page();
        }
        _repositories.IngrediensRepository.Create(new Ingrediens(Navn,Beskrivelse,Antal));

        return RedirectToPage("Alle");
    }
}
