using Microsoft.AspNetCore.Identity;
using Sablou_Web.Models;

namespace Sablou_Web.Services.Repositories;

public class BrugerRepository : RepositoryBase<Bruger>, IBrugerRepository
{
    private PasswordHasher<string> _passwordHasher;

    public BrugerRepository()
    {
        _passwordHasher = new PasswordHasher<string>();
    }

    public List<string> Roller
    {
        get { return new List<string> { "Admin", "Kunde" }; }
    }

    public override void Create(Bruger bruger)
    {
        using cralle_dk_db_sablouContext context = new cralle_dk_db_sablouContext();

        bool emailExists = context.Bruger.Any(u => u.Email == bruger.Email);
        if (emailExists)
        {
            throw new InvalidOperationException("E-mailen findes allerede.");
        }

        bruger.Password = _passwordHasher.HashPassword(bruger.Email, bruger.Password);
        base.Create(bruger);
    }

    public Bruger? VerifyUser(string providedEmail, string providedPassword)
    {
        // List<Bruger> Bruger = All;

        List<Bruger> brugere = new List<Bruger>();

        using cralle_dk_db_sablouContext context = new cralle_dk_db_sablouContext();

        brugere = context.Bruger.ToList();


        Bruger? bruger = brugere.FirstOrDefault(u => u.Email == providedEmail);
        bool result = bruger != null && VerifyPassword(bruger, providedPassword);
        
        if (result)
        {
            return bruger;
        }
        else
            return null;

    }

    private bool VerifyPassword(Bruger bruger, string providedPassword)
    {
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(
            bruger.Email, bruger.Password, providedPassword);

        return result == PasswordVerificationResult.Success;
    }

    //private bool VerifyPassword(Bruger bruger, string providedPassword)
    //{
    //	return bruger.Password == providedPassword;
    //}
}

