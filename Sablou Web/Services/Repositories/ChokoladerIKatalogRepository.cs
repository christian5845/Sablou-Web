using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;

namespace Sablou_Web.Services.Repositories;

public class ChokoladerIKatalogRepository : RepositoryBase<ChokoladerIkatalog>
{
    public Dictionary<int,ChokoladerIkatalog> Data { get { return base.Data; } }

    protected virtual IQueryable GetAllWithIncludes(DbContext context)
    {
        return context.Set<ChokoladerIkatalog>().Include(p=> p.ChokoladeId).Include(p=>p.KatalogId);
    }
}
