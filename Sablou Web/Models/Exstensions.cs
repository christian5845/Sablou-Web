using Microsoft.CodeAnalysis.CSharp.Syntax;
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
public partial class Højtider : IHarId
{


}

public partial class ChokoladerIkatalog : IHarId
{
    public ChokoladerIkatalog()
    {

    }
    public ChokoladerIkatalog(int chokoladeid, int katalogid)
    {
        ChokoladeId = chokoladeid;
        KatalogId = katalogid;
    }
}

public partial class HøjtidsKatalog : IHarId
{



    [NotMapped]
    public string Højtidsnavn
    {
        get
        {
            if (Højtid == 1)
            {
                return "Påske";
            }
            else if (Højtid == 2)
            {
                return "Mors dag";
            }
            else if (Højtid == 3)
            {
                return "Fars dag";
            }
            else if (Højtid == 4)
            {
                return "Halloween";
            }
            else
                return "tom";

        }
    }

}

public partial class Kurv : IHarId
{
    public Kurv()
    { 
    }
}

public partial class KurvLinje : IHarId
{
    public KurvLinje()
    {
    }
}

public partial class Ordre : IHarId
{
    public Ordre()
    {
    }
}