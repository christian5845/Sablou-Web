using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Ingredienser;
[Authorize(Roles = "Admin")]
public class SletModel : PageModel
{
    private IDataService _data;
    public Ingrediens Ingrediens { get; set; }

   public SletModel(IDataService dataservice)
    {
        _data = dataservice;
    }

    public void OnGet(int id)
    {
        Ingrediens = _data.IngrediensRepository.GetItem(id);
    }

    public IActionResult OnPost(int id)
    {

        _data.IngrediensRepository.Delete(_data.IngrediensRepository.GetItem(id).Id);
        return RedirectToPage("Alle");
    }
}
