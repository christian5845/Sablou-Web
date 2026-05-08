using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Ingredienser;
[Authorize(Roles = "Admin")]
public class OpdaterModel : PageModel
{
    private IDataService _data;

    public OpdaterModel()
    {
        _data = new Dataservice();
    }

    public void OnGet(int id)
    {

    }
}
