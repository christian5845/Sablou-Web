using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Kataloger;
[Authorize(Roles = "Admin")]
public class OpdaterHøjtidModel : PageModel
{

    public IDataService Repositories { get; }

    [BindProperty]
    public string Valg { get; set; }

    public bool ErHøjtidValgt { get; set; }

    public OpdaterHøjtidModel(IDataService ds)
    {
        Repositories = ds;
    }

    public void OnGet(int id)
    {
        if (id == null)
        {
            ErHøjtidValgt = false;
        }
        else
        {
            ErHøjtidValgt = true;
        }
    }

    public IActionResult OnPost()
    {
        return RedirectToPage();
    }
}
