using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Pages.Kurve;
using Sablou_Web.Services;
using System.Text.Json;

namespace Sablou_Web.Pages;

public class ProduktsideModel : PageModel
{
    private int _antalChokolader;
    public IDataService Repo { get; set; }
    public Chokolade Chokoladen { get; set; }
    public int AntalChokolader { get; set; }
    public Decimal? TotalPris { get { return Chokoladen.Stykpris * AntalChokolader; } }
    private const string SessionKey = "GæsteKurv";
    private static string ProduktSessionKey = "Produkt";

    public ProduktsideModel(IDataService dataService)
    {
        Repo = dataService;
    }

    #region Metoder
    public IActionResult OnGet(int id)
    {
        Chokoladen = Repo.ChokoladeRepository.GetItem(id);
        string idString = Chokoladen.Id.ToString();
        ProduktSessionKey = "Produkt" + idString;
        _antalChokolader = HentSessionDataTilInt();
        if (_antalChokolader > 1 || _antalChokolader != null)
        {
            AntalChokolader = _antalChokolader;
        }
        else
        {
            AntalChokolader = 1;
        }
        return Page();
    }
    public IActionResult OnPostTilføjTilKurv(int id)
    {
        DataIndLæsning(id);

        Repo = new Dataservice();
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
                .FirstOrDefault(l => l.KurvId == kurv.Id && l.ChokoladeId == id);

            if (linje != null)
            {
                 linje.Antal += AntalChokolader;
                Repo.KurvLinjeRepository.Update(linje);
            }
            else
            {
                Repo.KurvLinjeRepository.Create(new KurvLinje
                {
                    KurvId = kurv.Id,
                    ChokoladeId = id,
                    Antal = 1
                });
            }
        }
        else
        {
            // Gæst – gem i session
            List<KurvLinje> kurv = HentSessionDataKurv();
            var linje = kurv.FirstOrDefault(l => l.ChokoladeId == id);
            if (linje != null)
                linje.Antal+=AntalChokolader;
            else
                kurv.Add(new KurvLinje { ChokoladeId = id, Antal = AntalChokolader });
            GemSessionDataKurv(kurv);
        }
        TempData["CartFeedback"] = "Varen er lagt i kurven.";
        GemSessionDataFraInt(1);
        return RedirectToPage(new { id });
    }
    public IActionResult OnPostOpretAntal(int id)
    {
        DataIndLæsning(id);
        JusterAntalAfChokolader(id, 1);
        return RedirectToPage(new { id });
    }
    public IActionResult OnPostReducerAntal(int id)
    {
        DataIndLæsning(id);
        JusterAntalAfChokolader(id, -1);
        return RedirectToPage(new { id });
    }
    private void JusterAntalAfChokolader(int chokoladeId, int antalSkift)
    {
        if (chokoladeId != null)
        {
            int? antal = AntalChokolader + antalSkift;

            if (antal <1 || antal == null)
            {
                return;
            }

            GemSessionDataFraInt(antal);
        }
    }
    private void DataIndLæsning(int id)
    {
        Chokoladen = Repo.ChokoladeRepository.GetItem(id);
        string idString = Chokoladen.Id.ToString();
        ProduktSessionKey = "Produkt" + idString;
        _antalChokolader = HentSessionDataTilInt();
        if (_antalChokolader > 1 || _antalChokolader != null)
        {
            AntalChokolader = _antalChokolader;
        }
        else
        {
            AntalChokolader = 1;
        }
    }

    #region Session data metoder
    private int HentSessionDataTilInt()
    {
        List<string> list = HentSessionData();
        string dataString = list.FirstOrDefault();
        if (dataString != null)
        {
            int dataInt = Int32.Parse(dataString);
            return dataInt;
        }
        return 1;

    }
    private void GemSessionDataFraInt(int? data)
    {
        if (data != null)
        {


            string dataString = data.ToString();
            List<string> dataList = new List<string>();
            dataList.Add(dataString);
            GemSessionData(dataList);
        }

    }
    private List<string> HentSessionData()
    {
        var json = HttpContext.Session.GetString(ProduktSessionKey);
        return json == null
            ? new List<string>()
            : JsonSerializer.Deserialize<List<string>>(json)!;
    }
    private void GemSessionData(List<string> data)
    {
        HttpContext.Session.SetString(ProduktSessionKey, JsonSerializer.Serialize(data));
    }
    private List<KurvLinje> HentSessionDataKurv()
    {
        var json = HttpContext.Session.GetString(SessionKey);
        return json == null
            ? new List<KurvLinje>()
            : JsonSerializer.Deserialize<List<KurvLinje>>(json)!;
    }
    private void GemSessionDataKurv(List<KurvLinje> data)
    {
        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(data));
    }
    #endregion
    #endregion
}
