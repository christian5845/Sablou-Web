using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;
using Sablou_Web.Models;

namespace Sablou_Web.Pages;

public class ProduktsideModel : PageModel
{
    public IDataService Repositories { get; }
    public Chokolade Chokoladen { get; }

    public ProduktsideModel()
    {
        Repositories = new Dataservice();
    }

    public void OnGet(int id)
    {
        //Chokoladen = Repositories.
    }
}
