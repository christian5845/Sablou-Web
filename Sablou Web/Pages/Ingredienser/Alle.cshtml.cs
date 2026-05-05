using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Ingredienser;
//[Authorize(Rolle = "admin")]
public class AlleModel : PageModel
{
    public Dataservice Repositories { get; set; }

    public AlleModel()
    {
        Repositories = new Dataservice();
    }

    public void OnGet()
    {
    }
}
