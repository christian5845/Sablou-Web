using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services.Repositories;
using Sablou_Web.Models;

namespace Sablou_Web.Pages.Chokolader;

public class InformationModel : PageModel
{
    private ChokoladeRepository _repository;

    public InformationModel(ChokoladeRepository repository)
    {
        _repository = repository;
    }

    public Chokolade? Chokolade { get; set; }

    public IActionResult OnGet(int id)
    {
        if (LoginModel.CurrentBruger?.Rolle != "Admin")
        {
            return RedirectToPage("/Forside");
        }
        Chokolade = _repository.GetItem(id);

        if (Chokolade == null)
        {
            return NotFound();
        }

        return Page();
    }
}