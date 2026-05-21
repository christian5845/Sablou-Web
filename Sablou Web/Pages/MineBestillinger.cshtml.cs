using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;
using Sablou_Web.Models;

namespace Sablou_Web.Pages;

public class MineBestillingerModel : PageModel
{
    public IDataService Repo { get; }

    public int BrugerId { get; set; }
    public List<GemtOrdre> Ordrer { get; set; } = new();

    public MineBestillingerModel(IDataService dataService)
    {
        Repo = dataService;
        BrugerId = LoginModel.CurrentBruger.Id;
    }

    public void OnGet()
    {
        Ordrer = Repo.OrdreRepository.Data.Values
           .OrderByDescending(o => o.Dato)
           .Select(o =>
           {
               var gemt = new GemtOrdre
               {
                   Id = o.Id.ToString(),
                   Dato = o.Dato,
                   Navn = o.Navn,
                   Email = o.Email,
                   Telefon = o.Telefon,
                   Adresse = o.Adresse,
                   Besked = o.Besked,
                   ErLoggetInd = o.ErLoggetInd,
                   BrugerId = o.BrugerId,
                   ErBehandlet = o.Behandlet,            
                   ErAnnulleret = o.ErAnnulleret
               };

               List<GemtOrdreLinje> gemtOrdreLinjer = Repo.OrdreLinjeRepository.Data.Values
                   .Where(l => l.OrdreId == o.Id)
                   .Select(l =>
                   {
                       var ch = Repo.ChokoladeRepository.GetItem(l.ChokoladeId);
                       return new GemtOrdreLinje
                       {
                           ChokoladeId = l.ChokoladeId,
                           Navn = ch?.Navn ?? "Ukendt",
                           Stykpris = ch?.Stykpris ?? 0,
                           Antal = l.Antal
                       };
                   }).ToList();

               foreach (GemtOrdreLinje l in gemtOrdreLinjer)
                   gemt.Linjer.Add(l);

               return gemt;
           })
           .Where(o => o.BrugerId == BrugerId)
           .ToList();
    }

    // Annullér ordre
    // Kaldes når kunden trykker "Ja" i bekræftelses-popup'en.
    public IActionResult OnPostAnnuller(int id)
    {
        // Sikkerhedstjek: ordren skal tilhøre den indloggede bruger
        var ordre = Repo.OrdreRepository.Data.Values
            .FirstOrDefault(o => o.Id == id && o.BrugerId == BrugerId);

        if (ordre == null || ordre.Behandlet)
            return Forbid(); // Ordren findes ikke eller tilhører en anden bruger, eller allerede behandlet

        // Sæt ErAnnulleret i stedet for at slette
        // Så admin stadig kan se den
        ordre.ErAnnulleret = true;
        Repo.OrdreRepository.Update(ordre);

        return RedirectToPage();
    }
}