using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Kataloger;

public class OpdaterKatalogModel : PageModel
{

    public IDataService Repositories { get; }

    public OpdaterKatalogModel(IDataService ds)
    {
        Repositories = ds;
    }

    public void OnGet()
    {
    }
}
