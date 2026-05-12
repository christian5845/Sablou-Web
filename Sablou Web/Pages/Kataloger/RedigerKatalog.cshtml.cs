using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Tasks;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Sablou_Web.Models;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Kataloger;

public class RedigerKatalogModel : PageModel
{
    [BindProperty]
    public int Id { get; set; }
    [BindProperty]
    public HøjtidsKatalog Kataloget { get; set; }

    [BindProperty]
    public List<int> IdListe { get; set; }

    public IActionResult OnGet(int id)
    {
        if (id == null)
        {
            return NotFound();
        }
        IdListe = new List<int>();
        foreach (var listeid in Repo.ChokoladerIKatalogRepository.Data.Values)
        {
            if (listeid.KatalogId == id)
            {
                IdListe.Add(listeid.ChokoladeId);
            }
        }
        Kataloget = Repo.HøjtidsKatalogRepository.GetItem(id);

        return Page();
    }
    public IActionResult OnPostOpdaterKatalog(int id, int cid)
    {
        if (IdListe.Contains(cid))
        {
            Repo.ChokoladerIKatalogRepository.Delete(cid);
        }
        else
        {
            Repo.ChokoladerIKatalogRepository.Create(new ChokoladerIkatalog(cid, Kataloget.Id));
        }
        return RedirectToPage(new {id});
    }
    public IDataService Repo { get; }

    public RedigerKatalogModel(IDataService repo)
    {
        Repo = repo;
    }
}
