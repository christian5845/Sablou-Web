using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Kataloger;

public class SommerKatalogModel : KatalogBase
{
    public SommerKatalogModel(IDataService repo) : base(repo)
    {
    }

    protected override string Højtidsnavn => "Sommer";
    public void OnGet()
    {
    }
    public IDataService Repo { get; }



}

