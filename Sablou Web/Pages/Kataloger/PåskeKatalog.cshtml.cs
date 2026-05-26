using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;
using Sablou_Web.Services.Repositories;
using System.Text.Json;


namespace Sablou_Web.Pages.Kataloger;

public class PåskeKatalogModel : KatalogBase
{

    public override string Højtidsnavn => "Påske";
    public override string SessionKey => "GæsteKurv";

    public PåskeKatalogModel(IDataService repo) : base(repo)
    {

    }

    
    public void OnGet()
    {
    }

}
