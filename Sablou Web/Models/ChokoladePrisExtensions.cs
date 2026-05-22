using System.Globalization;

namespace Sablou_Web.Models;

// Denne klasse bruges til at lave en pæn pris-tekst for chokolade
public static class ChokoladePrisExtensions
{
    // Denne metode kan bruges direkte på en Chokolade, fx c.VisPris()
    public static string VisPris(this Chokolade chokolade)
    {
        try
        {
            // Hvis chokoladen ikke findes, vises en fejlbesked
            if (chokolade == null)
                throw new Exception("Pris ikke tilgængelig");

            // Hvis prisen er 0 eller mindre, vises en fejlbesked
            if (chokolade.Stykpris <= 0)
                throw new Exception("Pris ikke tilgængelig");

            // Prisen vises med 2 decimaler og dansk format
            return chokolade.Stykpris.ToString("0.00", CultureInfo.GetCultureInfo("da-DK")) + " kr.";
        }
        catch
        {
            // Hvis der sker en fejl, vises denne besked i stedet
            return "Pris ikke tilgængelig";
        }
    }
}