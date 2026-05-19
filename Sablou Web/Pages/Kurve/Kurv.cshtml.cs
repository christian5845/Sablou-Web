using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;
using System.Text.Json;

namespace Sablou_Web.Pages.Kurve;

public class KurvModel : PageModel
{
    private IDataService _repo;
    private const string SessionKey = "GæsteKurv";

    
    public List<KurvLinjeViewModel> KurvLinjer { get; set; } = new();
    public decimal Total => KurvLinjer.Sum(l => l.Subtotal);


    public KurvModel(IDataService repo)
    {
        _repo = repo;
    }

    public void OnGet()
    {
        KurvLinjer = HentKurvLinjer();
    }

    //public IActionResult OnPostTilføjTilKurv(int chokoladeId)
    //{
    //    var bruger = LoginModel.CurrentBruger;

    //    if (bruger != null)
    //    {
    //        // Find eller opret kurv i DB
    //        var kurv = _repo.KurvRepository.Data.Values
    //            .FirstOrDefault(k => k.BrugerId == bruger.Id);

    //        if (kurv == null)
    //        {
    //            kurv = new Kurv { BrugerId = bruger.Id };
    //            _repo.KurvRepository.Create(kurv);
    //        }

    //        // Find eksisterende linje eller opret ny
    //        var linje = _repo.KurvLinjeRepository.Data.Values
    //            .FirstOrDefault(l => l.KurvId == kurv.Id && l.ChokoladeId == chokoladeId);

    //        if (linje != null)
    //        {
    //            linje.Antal++;
    //            _repo.KurvLinjeRepository.Update(linje);
    //        }
    //        else
    //        {
    //            _repo.KurvLinjeRepository.Create(new KurvLinje
    //            {
    //                KurvId = kurv.Id,
    //                ChokoladeId = chokoladeId,
    //                Antal = 1
    //            });
    //        }
    //    }
    //    else
    //    {
    //        // Gæst – gem i session
    //        var kurv = HentSessionData();
    //        var linje = kurv.FirstOrDefault(l => l.ChokoladeId == chokoladeId);
    //        if (linje != null)
    //            linje.Antal++;
    //        else
    //            kurv.Add(new KurvLinje { ChokoladeId = chokoladeId, Antal = 1 });
    //        GemSessionData(kurv);
    //    }

    //    return RedirectToPage();
    //}

    public IActionResult OnPostFjern(int chokoladeId)
    {
        var bruger = LoginModel.CurrentBruger;

        if (bruger != null)
        {
            var kurv = _repo.KurvRepository.Data.Values
                .FirstOrDefault(k => k.BrugerId == bruger.Id);
            if (kurv != null)
            {
                var linje = _repo.KurvLinjeRepository.Data.Values
                    .FirstOrDefault(l => l.KurvId == kurv.Id && l.ChokoladeId == chokoladeId);
                if (linje != null)
                    _repo.KurvLinjeRepository.Delete(linje.Id);
            }
        }
        else
        {
            var kurv = HentSessionData();
            kurv.RemoveAll(l => l.ChokoladeId == chokoladeId);
            GemSessionData(kurv);
        }

        return RedirectToPage();
    }

    public IActionResult OnPostOpretAntal(int chokoladeId)
    {
        JusterAntal(chokoladeId, 1);
        return RedirectToPage();
    }

    public IActionResult OnPostReducerAntal(int chokoladeId)
    {
        JusterAntal(chokoladeId, -1);
        return RedirectToPage();
    }

    private void JusterAntal(int chokoladeId, int antalSkift)
    {
        var bruger = LoginModel.CurrentBruger;

        if (bruger != null)
        {
            var kurv = _repo.KurvRepository.Data.Values
                .FirstOrDefault(k => k.BrugerId == bruger.Id);

            if (kurv == null)
                return;

            var linje = _repo.KurvLinjeRepository.Data.Values
                .FirstOrDefault(l => l.KurvId == kurv.Id && l.ChokoladeId == chokoladeId);

            if (linje == null)
            {
                return;
            }

            linje.Antal += antalSkift;
            if (linje.Antal <= 0)
            {
                _repo.KurvLinjeRepository.Delete(linje.Id);
            }
            else
            {
                _repo.KurvLinjeRepository.Update(linje);
            }
        }
        else
        {
            var kurv = HentSessionData();
            var linje = kurv.FirstOrDefault(l => l.ChokoladeId == chokoladeId);

            if (linje == null)
            {
                return;
            }

            linje.Antal += antalSkift;
            if (linje.Antal <= 0)
            {
                kurv.RemoveAll(l => l.ChokoladeId == chokoladeId);
            }

            GemSessionData(kurv);
        }
    }

    private List<KurvLinjeViewModel> HentKurvLinjer()
    {
        var bruger = LoginModel.CurrentBruger;
        List<KurvLinje> linjer;

        if (bruger != null)
        {
            var kurv = _repo.KurvRepository.Data.Values
                .FirstOrDefault(k => k.BrugerId == bruger.Id);

            if (kurv == null) return new List<KurvLinjeViewModel>();

            linjer = _repo.KurvLinjeRepository.Data.Values
                .Where(l => l.KurvId == kurv.Id)
                .ToList();
        }
        else
        {
            linjer = HentSessionData();
        }

        // Slå chokoladedata op og byg ViewModels
        return linjer.Select(l =>
        {
            var choko = _repo.ChokoladeRepository.GetItem(l.ChokoladeId);
            return new KurvLinjeViewModel
            {
                ChokoladeId = l.ChokoladeId,
                Navn = choko.Navn,
                Stykpris = choko.Stykpris,
                Antal = l.Antal
            };
        }).ToList();
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

