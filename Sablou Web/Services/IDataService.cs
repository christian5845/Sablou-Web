using Sablou_Web.Models;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Services;

public interface IDataService
{
    IRepository<Ingrediens> IngrediensRepository { get; }
    IRepository<Bruger> BrugerRepository { get; }

    IRepository<Chokolade> ChokoladeRepository { get; }

}
