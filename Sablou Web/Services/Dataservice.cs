using Sablou_Web.Models;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Services;

public class Dataservice : IDataService
{
    public IRepository<Ingrediens> IngrediensRepository { get; }
    public IBrugerRepository BrugerRepository { get; } 
    public IRepository<Chokolade> ChokoladeRepository { get; }
    public IRepository<IngrediensListe> IngrediensListeRepository { get; }

    public IRepository<HøjtidsKatalog> HøjtidsKatalogRepository { get; }

    public IRepository<Højtider> HøjtiderRepository { get; }

    public IRepository<Kurv> KurvRepository { get; }

    public IRepository<ChokoladerIkatalog> ChokoladerIKatalogRepository { get; }

    public Dataservice()
    {
        IngrediensRepository = new IngrediensRepository();
        BrugerRepository = new BrugerRepository();
        ChokoladeRepository = new ChokoladeRepository();
        IngrediensListeRepository = new IngrediensListeRepository();
        HøjtidsKatalogRepository = new HøjtidsKatalogRepository();
        HøjtiderRepository = new HøjtiderRepository();
        KurvRepository = new KurvRepository();

        ChokoladerIKatalogRepository = new ChokoladerIKatalogRepository();
    }
}
