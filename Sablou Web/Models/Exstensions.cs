namespace Sablou_Web.Models;

public partial class Ingrediens : IHarId
{
    public Ingrediens(string navn, string beskrivelse)
    {
        Navn = navn;
        Beskrivelse = beskrivelse;
    }
}