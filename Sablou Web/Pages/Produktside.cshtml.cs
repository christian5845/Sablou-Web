using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Pages.Kurve;
using Sablou_Web.Services;
using System.Text.Json;

namespace Sablou_Web.Pages;

public class ProduktsideModel : PageModel
{
    public IDataService Repositories { get; }
    public Chokolade Chokoladen { get; set; }
    public int AntalChokolader { get; set; }

    private const string SessionKey = "GæsteKurv";

    public List<KurvLinjeViewModel> KurvLinjer { get; set; } = new();
    public decimal Total => KurvLinjer.Sum(l => l.Subtotal);

    public ProduktsideModel(IDataService dataService)
    {
        Repositories = dataService;
        AntalChokolader = 1;
    }


    public void OnGet(int id)
    {
        Chokoladen = Repositories.ChokoladeRepository.GetItem(id);
    }

    //public IActionResult OnPostOpretAntal(int chokoladeId)
    //{
    //    JusterAntal(chokoladeId, 1);
    //    return RedirectToPage();
    //}

    //public IActionResult OnPostReducerAntal(int chokoladeId)
    //{
    //    JusterAntal(chokoladeId, -1);
    //    return RedirectToPage();
    //}

    //private void JusterAntal(int chokoladeId, int antalSkift)
    //{
    //    var bruger = LoginModel.CurrentBruger;

    //    if (bruger != null)
    //    {
    //        var kurv = _repo.KurvRepository.Data.Values
    //            .FirstOrDefault(k => k.BrugerId == bruger.Id);

    //        if (kurv == null)
    //            return;

    //        var linje = _repo.KurvLinjeRepository.Data.Values
    //            .FirstOrDefault(l => l.KurvId == kurv.Id && l.ChokoladeId == chokoladeId);

    //        if (linje == null)
    //        {
    //            return;
    //        }

    //        linje.Antal += antalSkift;
    //        if (linje.Antal <= 0)
    //        {
    //            _repo.KurvLinjeRepository.Delete(linje.Id);
    //        }
    //        else
    //        {
    //            _repo.KurvLinjeRepository.Update(linje);
    //        }
    //    }
    //    else
    //    {
    //        var kurv = HentSessionData();
    //        var linje = kurv.FirstOrDefault(l => l.ChokoladeId == chokoladeId);

    //        if (linje == null)
    //        {
    //            return;
    //        }

    //        linje.Antal += antalSkift;
    //        if (linje.Antal <= 0)
    //        {
    //            kurv.RemoveAll(l => l.ChokoladeId == chokoladeId);
    //        }

    //        GemSessionData(kurv);
    //    }
    //}
}
