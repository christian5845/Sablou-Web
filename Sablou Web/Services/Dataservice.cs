using Sablou_Web.Models;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Services;

public class Dataservice : IDataService
{
    public IRepository<Ingrediens> IngrediensRepository { get; }
    public IBrugerRepository BrugerRepository { get; } 
    public IRepository<Chokolade> ChokoladeRepository { get; }
    public IRepository<IngrediensListe> IngrediensListeRepository { get; }

    public Dataservice()
    {
        IngrediensRepository = new IngrediensRepository();
        BrugerRepository = new BrugerRepository();
        ChokoladeRepository = new ChokoladeRepository();
        IngrediensListeRepository = new IngrediensListeRepository();
    }
}
