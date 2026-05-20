using Sablou_Web.Models;

namespace Sablou_Web.Services.Repositories;

public interface IOrdreLinjeRepository : IRepository<OrdreLinje>
{
    int OpretMedId(OrdreLinje element);
}
