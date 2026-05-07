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
    public Bruger(string navn, string email, string adresse, int? telefonnummer, string password)
    {
        Navn = navn; Email = email; Adresse = adresse; Telefonnummer = telefonnummer; Password = password; Rolle = "Kunde";
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


