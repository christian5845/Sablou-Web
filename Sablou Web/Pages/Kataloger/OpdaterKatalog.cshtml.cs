using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Kataloger;

public class OpdaterKatalogModel : PageModel
{


    public IDataService Repositories { get; }

    [BindProperty]
    public string Valg { get; set; }

    public bool ErKatalogValgt { get; set; }

    public OpdaterKatalogModel(IDataService ds)
    {
        Repositories = ds;
    }

    public void OnGet(int id)
    {
        if (id == null)
        {
            ErKatalogValgt = false;
        }
        else
        {
            ErKatalogValgt = true;
        }
    }

    public IActionResult OnPost()
    {
        return RedirectToPage();
    }
}
