using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;

namespace Sablou_Web.Services.Repositories
{
    public class ChokoladerIKatalogRepository : RepositoryBase<ChokoladerIkatalog>
    {
        protected virtual IQueryable GetAllWithIncludes(DbContext context)
        {
            return context.Set<ChokoladerIkatalog>().Include(p=> p.ChokoladeId).Include(p=>p.KatalogId);
        }
    }
}
