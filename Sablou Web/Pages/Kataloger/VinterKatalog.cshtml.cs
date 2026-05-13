using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Kataloger;

public class VinterKatalogModel : KatalogBase
{
    public VinterKatalogModel(IDataService repo) : base(repo)
    {
    }

    protected override string Højtidsnavn => "Vinter";
    public void OnGet()
    {
    }
    public IDataService Repo { get; }



}


