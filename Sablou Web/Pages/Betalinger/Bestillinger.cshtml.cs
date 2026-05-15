using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Pages.Kurve;
using Sablou_Web.Services;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;

namespace Sablou_Web.Pages.Bestillinger
{
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
            // Hent ordrer fra DB og map til DTO for visning
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
                        BrugerId = o.BrugerId
                    };

                    gemt.Linjer = _repo.KurvLinjeRepository.Data.Values
                        .Where(l => l.KurvId == o.Id)
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

                    return gemt;
                }).ToList();
        }

        // Admin: slet ordre
        public IActionResult OnPostSlet(int id)
        {
            if (LoginModel.CurrentBruger?.Rolle != "Admin")
                return Forbid();

            // Slet ordrelinjer først (foreign key)
            var linjer = _repo.KurvLinjeRepository.Data.Values.Where(l => l.KurvId == id).ToList();
            foreach (var l in linjer)
                _repo.KurvLinjeRepository.Delete(l.Id);

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
}