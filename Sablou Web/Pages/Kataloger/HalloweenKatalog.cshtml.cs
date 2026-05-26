using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;
using System.Text.Json;

namespace Sablou_Web.Pages.Kataloger;

public class HalloweenKatalogModel : KatalogBase
{

   
    public override string Højtidsnavn => "Halloween";
    public override string SessionKey => "GæsteKurv";

    public HalloweenKatalogModel(IDataService repo) : base(repo)
    {
    }


    public void OnGet()
    {
    }

}


