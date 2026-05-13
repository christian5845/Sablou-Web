using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Services;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Pages.Kataloger;

public class EfterårsKatalogModel : KatalogBase
{
    public EfterårsKatalogModel(IDataService repo) : base(repo)
    {
    }

    protected override string Højtidsnavn => "Efterår";
    public void OnGet()
    {
    }
    public IDataService Repo { get; }



}
