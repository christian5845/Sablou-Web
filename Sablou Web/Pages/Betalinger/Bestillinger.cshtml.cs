using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;

namespace Sablou_Web.Pages.Betalinger;

public class BestillingerModel : PageModel
{
    private readonly IDataService _repo;
    public List<GemtOrdre> Ordrer { get; set; } = new();

    public BestillingerModel(IDataService repo)
    {
        _repo = repo;
    }

    public void OnGet()
    {
        Ordrer = _repo.OrdreRepository.Data.Values
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

                List<GemtOrdreLinje> gemtOrdreLinjer = _repo.OrdreLinjeRepository.Data.Values
                    .Where(l => l.OrdreId == o.Id)
                    .Select(l =>
                    {
                        var ch = _repo.ChokoladeRepository.GetItem(l.ChokoladeId);
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
            }).ToList();
    }

    // Admin: slet ordre permanent
    public IActionResult OnPostSlet(int id)
    {
        if (LoginModel.CurrentBruger?.Rolle != "Admin")
            return Forbid();

        var linjer = _repo.OrdreLinjeRepository.Data.Values.Where(l => l.OrdreId == id).ToList();
        foreach (var l in linjer)
            _repo.OrdreLinjeRepository.Delete(l.Id);

        _repo.OrdreRepository.Delete(id);
        return RedirectToPage();
    }

    // Admin: markér som behandlet
    public IActionResult OnPostMarkBehandlet(int id)
    {
        if (LoginModel.CurrentBruger?.Rolle != "Admin")
            return Forbid();

        var ordre = _repo.OrdreRepository.Data.Values.FirstOrDefault(o => o.Id == id);
        if (ordre == null) return NotFound();

        ordre.Behandlet = true;
        _repo.OrdreRepository.Update(ordre);
        return RedirectToPage();
    }
}
