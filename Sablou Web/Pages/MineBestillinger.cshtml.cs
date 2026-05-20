using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Sablou_Web.Pages.Betalinger;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;
using Sablou_Web.Models;

namespace Sablou_Web.Pages;

public class MineBestillingerModel : PageModel
{
    public IDataService Repo { get;}

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
                   ErBehandlet = o.Behandlet
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
               {
                   gemt.Linjer.Add(l);
               }
               return gemt;
           }).Where(o => o.BrugerId == BrugerId) .ToList();

    }
}
