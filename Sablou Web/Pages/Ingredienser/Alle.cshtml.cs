using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Ingredienser;
[Authorize(Roles = "Admin")]
public class AlleModel : PageModel
{
    public IDataService Repositories { get; }

    public AlleModel(IDataService dataservice)
    {
        Repositories = dataservice;
    }

    public void OnGet()
    {
    }
}
