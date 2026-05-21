using Sablou_Web.Pages.Betalinger;

namespace Sablou_Web.Models;

public class GemtOrdre
{
    public string Id { get; set; }
    public DateTime Dato { get; set; }
    public string Navn { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public string Adresse { get; set; }
    public string Besked { get; set; }
    public bool ErLoggetInd { get; set; }
    public int? BrugerId { get; set; }
    public List<GemtOrdreLinje> Linjer { get; set; } = new();
    public decimal Total => Linjer.Sum(l => l.Stykpris * l.Antal);
    public bool ErBehandlet { get; set; }
    public bool ErAnnulleret { get; set; }
}
