namespace Sablou_Web.Models;

public class KurvLinjeViewModel
{
    public int ChokoladeId { get; set; }
    public string Navn { get; set; }
    public decimal Stykpris { get; set; }
    public int Antal { get; set; }
    public decimal Subtotal => Stykpris * Antal;
}
