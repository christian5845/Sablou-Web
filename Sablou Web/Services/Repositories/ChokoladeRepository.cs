using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
using System.Linq;

namespace Sablou_Web.Services.Repositories
{
    public class ChokoladeRepository : RepositoryBase<Chokolade>
    {
        //protected override IQueryable GetAllWithIncludes(DbContext context)
        //{
        //    return context.Set<Chokolade>()
        //        .Include(c => c.Ingrediens);
        //}
    }
}