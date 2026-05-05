using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Ingredienser;

public class AlleModel : PageModel
{
    public Dataservice Data { get; set; }

    public AlleModel()
    {
        Data = new Dataservice();
    }

    public void OnGet()
    {
    }
}
