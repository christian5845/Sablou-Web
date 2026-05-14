using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;
using System.Text.Json;

namespace Sablou_Web.Pages.Kataloger;

public class SommerKatalogModel : KatalogBase
{
    public IDataService Repo { get; }
    protected override string Højtidsnavn => "Fars dag";
    private const string SessionKey = "GæsteKurv";

    public SommerKatalogModel(IDataService repo) : base(repo)
    {
    }


    public void OnGet()
    {
    }

    public IActionResult OnPostTilføjTilKurv(int chokoladeId)
    {
        var bruger = LoginModel.CurrentBruger;

        if (bruger != null)
        {
            // Find eller opret kurv i DB
            var kurv = Repo.KurvRepository.Data.Values
                .FirstOrDefault(k => k.BrugerId == bruger.Id);

            if (kurv == null)
            {
                kurv = new Kurv { BrugerId = bruger.Id };
                Repo.KurvRepository.Create(kurv);
            }

            // Find eksisterende linje eller opret ny
            var linje = Repo.KurvLinjeRepository.Data.Values
                .FirstOrDefault(l => l.KurvId == kurv.Id && l.ChokoladeId == chokoladeId);

            if (linje != null)
            {
                linje.Antal++;
                Repo.KurvLinjeRepository.Update(linje);
            }
            else
            {
                Repo.KurvLinjeRepository.Create(new KurvLinje
                {
                    KurvId = kurv.Id,
                    ChokoladeId = chokoladeId,
                    Antal = 1
                });
            }
        }
        else
        {
            // Gæst – gem i session
            var kurv = HentSessionData();
            var linje = kurv.FirstOrDefault(l => l.ChokoladeId == chokoladeId);
            if (linje != null)
                linje.Antal++;
            else
                kurv.Add(new KurvLinje { ChokoladeId = chokoladeId, Antal = 1 });
            GemSessionData(kurv);
        }

        TempData["CartFeedback"] = "Varen er lagt i kurven.";
        return RedirectToPage();
    }
    private List<KurvLinje> HentSessionData()
    {
        var json = HttpContext.Session.GetString(SessionKey);
        return json == null
            ? new List<KurvLinje>()
            : JsonSerializer.Deserialize<List<KurvLinje>>(json)!;
    }

    private void GemSessionData(List<KurvLinje> data)
    {
        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(data));
    }



}

