using Sablou_Web.Models;

namespace Sablou_Web.Services.Repositories;

public interface IOrdreRepository : IRepository<Ordre>
{
    int OpretMedId(Ordre element);
}
