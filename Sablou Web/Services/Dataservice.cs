using Sablou_Web.Models;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Services;

public class Dataservice : IDataService
{
    public IRepository<Ingrediens> IngrediensRepository { get; }

    public Dataservice()
    {
        IngrediensRepository = new IngrediensRepository();
    }
}
