using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Sablou_Web.Pages.Betalinger;

public class BetalingModel : PageModel
{
    public IDataService Repo;
    private const string SessionKey = "GæsteKurv";

    public List<KurvLinjeViewModel> KurvLinjer { get; set; } = new();
    public decimal Total => KurvLinjer.Sum(l => l.Subtotal);

    [BindProperty]
    [Required(ErrorMessage = "Navn er påkrævet")]
    public string GæsteNavn { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "E-mail er påkrævet")]
    [EmailAddress(ErrorMessage = "Ugyldig e-mailadresse")]
    public string GæsteEmail { get; set; }

    [BindProperty]
    public string GæsteTelefon { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Adresse er påkrævet")]
    public string GæsteAdresse { get; set; }

    [BindProperty]
    public string? Besked { get; set; }

    public BetalingModel(IDataService repo)
    {
        Repo = repo;
    }

    public void OnGet()
    {
        KurvLinjer = HentKurvLinjer();
    }

    public IActionResult OnPostAfgivBestilling()
    {
        //Repo = new Dataservice();

        KurvLinjer = HentKurvLinjer();

        if (!ModelState.IsValid)
            return Page();

        if (!KurvLinjer.Any())
        {
            TempData["Error"] = "Kurven er tom.";
            return RedirectToPage("/Kurve/Kurv");
        }

        // Opret ordre i databasen
        var dbOrdre = new Ordre
        {
            Navn = GæsteNavn,
            Email = GæsteEmail,
            Telefon = GæsteTelefon,
            Adresse = GæsteAdresse,
            Besked = Besked,
            ErLoggetInd = LoginModel.CurrentBruger != null,
            BrugerId = LoginModel.CurrentBruger?.Id ?? 0,
            // Behandlet håndteres i admin; default i DB = false
        };

        int ordreId = Repo.OrdreRepository.OpretMedId(dbOrdre);

        // Opret KurvLinje-entries i DB til den oprettede ordre
        if (LoginModel.CurrentBruger != null)
        {
            var kurv = Repo.KurvRepository.Data.Values
                .FirstOrDefault(k => k.BrugerId == LoginModel.CurrentBruger.Id);
            if (kurv != null)
            {
                var linjer = Repo.KurvLinjeRepository.Data.Values
                    .Where(l => l.KurvId == kurv.Id)
                    .ToList();

                foreach (var linje in linjer)
                {
                    var ny = new OrdreLinje
                    {
                        OrdreId = ordreId,
                        ChokoladeId = linje.ChokoladeId,
                        Antal = linje.Antal,
                    };
                    Repo.OrdreLinjeRepository.Create(ny);
                }
            }
        }
        else
        {
            // Gæstekurv: KurvLinjeViewModel -> opret KurvLinje i DB (ordrelinje)
            // Eksempel: opret ordrelinjer for gæster — bemærk: ingen KurvId sættes
            foreach (var linje in KurvLinjer)
            {
                var ny = new OrdreLinje
                {
                    OrdreId = ordreId,
                    ChokoladeId = linje.ChokoladeId,
                    Antal = linje.Antal,
                };
                Repo.OrdreLinjeRepository.OpretMedId(ny);
            }
        }

        // Tøm kurven efter bestilling (session eller DB-kurv)
        TømKurv();

        TempData["BestillingNavn"] = GæsteNavn;
        TempData["BestillingId"] = dbOrdre.Id.ToString();
        return RedirectToPage("/Betalinger/Bekræftelse");
    }

    // Hjælpemetoder

    private List<KurvLinjeViewModel> HentKurvLinjer()
    {
        var bruger = LoginModel.CurrentBruger;
        List<KurvLinje> linjer;

        if (bruger != null)
        {
            var kurv = Repo.KurvRepository.Data.Values
                .FirstOrDefault(k => k.BrugerId == bruger.Id);
            if (kurv == null) return new List<KurvLinjeViewModel>();
            linjer = Repo.KurvLinjeRepository.Data.Values
                .Where(l => l.KurvId == kurv.Id)
                .ToList();
        }
        else
        {
            var json = HttpContext.Session.GetString(SessionKey);
            linjer = json == null
                ? new List<KurvLinje>()
                : JsonSerializer.Deserialize<List<KurvLinje>>(json)!;
        }

        return linjer.Select(l =>
        {
            var choko = Repo.ChokoladeRepository.GetItem(l.ChokoladeId);
            return new KurvLinjeViewModel
            {
                ChokoladeId = l.ChokoladeId,
                Navn = choko?.Navn ?? "Ukendt",
                Stykpris = choko?.Stykpris ?? 0,
                Antal = l.Antal
            };
        }).ToList();
    }

    private void TømKurv()
    {
        var bruger = LoginModel.CurrentBruger;
        if (bruger != null)
        {
            var kurv = Repo.KurvRepository.Data.Values
                .FirstOrDefault(k => k.BrugerId == bruger.Id);
            if (kurv != null)
            {
                var linjer = Repo.KurvLinjeRepository.Data.Values
                    .Where(l => l.KurvId == kurv.Id)
                    .ToList();
                foreach (var linje in linjer)
                    Repo.KurvLinjeRepository.Delete(linje.Id);
            }
        }
        else
        {
            // Guest cart stored in session; fjern kun kurv-session
            HttpContext.Session.Remove(SessionKey);
        }
    }
}

