using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sablou_Web.Models;

public partial class Ingrediens : IHarId
{
    public Ingrediens()
    {
    }
    public Ingrediens(string navn, string beskrivelse,int antal)
    {
        Navn = navn;
        Beskrivelse = beskrivelse;
        Antal = antal;
    }
}

public partial class IngrediensListe : IHarId
{
    public IngrediensListe(int chokoladeId, int ingrediensId)
    {
        ChokoladeId = chokoladeId;
        IngrediensId = ingrediensId;
    }
}


public partial class Bruger : IHarId
{
    public static Bruger Construct(string navn, string email, string adresse, int? telefonNummer, string password)
    {
        return new Bruger { Navn = navn, Email = email, Adresse = adresse, Telefonnummer = telefonNummer, Password = password, Rolle = "Kunde" };
    }
}

public partial class Chokolade : IHarId
{
    public Chokolade()
    {
    }

    public Chokolade(string navn, decimal stykpris, string beskrivelse)
    {
        Navn = navn;
        Stykpris = stykpris;
        Beskrivelse = beskrivelse;
    }
}


