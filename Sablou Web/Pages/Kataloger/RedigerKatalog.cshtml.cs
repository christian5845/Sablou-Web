using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Tasks;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Sablou_Web.Models;
using Sablou_Web.Services;
using Sablou_Web.Services.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sablou_Web.Pages.Kataloger;
[Authorize(Roles = "Admin")]

public class RedigerKatalogModel : PageModel
{
    public IDataService Repo { get; }
    [BindProperty]
    public int Id { get; set; }
    [BindProperty]
    public HøjtidsKatalog Kataloget { get; set; }

    [BindProperty]
    public List<int> IdListe { get; set; }

    public RedigerKatalogModel(IDataService repo)
    {
        Repo = repo;
    }

    public IActionResult OnGet(int id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Id = id;
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
        IdListe = new List<int>();
        foreach (var listeid in Repo.ChokoladerIKatalogRepository.Data.Values)
        {
            if (listeid.KatalogId == id)
            {
                IdListe.Add(listeid.ChokoladeId);
            }
        }
        if (IdListe.Contains(cid))
        {
            DeleteMedChokoladeId(cid);
        }
        else
        {
            Repo.ChokoladerIKatalogRepository.Create(new ChokoladerIkatalog(cid, Kataloget.Id));
        }
        return RedirectToPage(new {id});
    }
    public bool DeleteMedChokoladeId(int cid)
    {
        int id = 0;
        var a = Repo.ChokoladerIKatalogRepository.Data.Select(t => t).Where(t => t.Value.ChokoladeId == cid);
        id = a.Select(t => t.Key).First();

        return Repo.ChokoladerIKatalogRepository.Delete(id);
    }
}
