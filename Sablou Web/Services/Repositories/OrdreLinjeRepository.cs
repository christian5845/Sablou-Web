using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
namespace Sablou_Web.Services.Repositories;


public class OrdreLinjeRepository : RepositoryBase<OrdreLinje>, IOrdreLinjeRepository
{
    public int OpretMedId(OrdreLinje element)
    {
        using DbContext context = CreateDbContext();
        int id = NextId();
        element.Id = id;

        context.Add(element);
        context.SaveChanges();
        return id;
    }

    public override bool Delete(int id)
    {
        return base.Delete(id);
    }

    public override OrdreLinje? GetItem(int id)
    {
        return base.GetItem(id);
    }

    public override void Update(OrdreLinje t)
    {
        base.Update(t);
    }
}
