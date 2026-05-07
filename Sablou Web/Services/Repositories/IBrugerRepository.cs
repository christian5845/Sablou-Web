using Sablou_Web.Models;
namespace Sablou_Web.Services.Repositories
{
    public interface IBrugerRepository : IRepository<Bruger>
    {
        public interface IBrugerRepository
        {
            Bruger? VerifyUser(string providedEmail, string providedPassword);
            List<string> Roller { get; }
        }
    }
}
