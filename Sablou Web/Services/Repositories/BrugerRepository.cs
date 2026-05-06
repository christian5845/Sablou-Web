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
        bruger.Password = _passwordHasher.HashPassword(bruger.Email, bruger.Password);
        base.Create(bruger);
    }

    public Bruger? VerifyBruger(string providedBrugerEmail, string providedPassword)
    {
        // List<Bruger> Bruger = All;

        List<Bruger> brugere = new List<Bruger>();

        using cralle_dk_db_sablouContext context = new cralle_dk_db_sablouContext();

        brugere = context.Bruger.ToList();


        Bruger? bruger = brugere.FirstOrDefault(u => u.Email == providedBrugerEmail &&
                                               u.Password == providedPassword);

        return bruger;
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

