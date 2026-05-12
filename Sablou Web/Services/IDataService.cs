using Sablou_Web.Models;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Services;

public interface IDataService
{
    IRepository<Ingrediens> IngrediensRepository { get; }
    IBrugerRepository BrugerRepository { get; }
    IRepository<Chokolade> ChokoladeRepository { get; }
    IRepository<IngrediensListe> IngrediensListeRepository { get; }
    IRepository<HøjtidsKatalog> HøjtidsKatalogRepository { get; }
    IRepository<Højtider> HøjtiderRepository { get; }


}
