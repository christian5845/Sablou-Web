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

public partial class Bruger : IHarId
{
    public Bruger(string navn, string email, string adresse, int? telefonNummer, string password, string rolle)
    {
        Navn = navn;
        Email = email;
        Adresse = adresse;
        TelefonNummer = telefonNummer;
        Password = password;
        Rolle = rolle;
    }
}