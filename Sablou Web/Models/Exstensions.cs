namespace Sablou_Web.Models;

public partial class Ingrediens : IHarId
{
    public Ingrediens(string navn, string beskrivelse,int antal)
    {
        Navn = navn;
        Beskrivelse = beskrivelse;
        Antal = antal;
    }
}