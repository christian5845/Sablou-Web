using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sablou_Web.Models;

namespace Sablou_Web.Services.Repositories;

public abstract class RepositoryBase<T> : IRepository<T> where T : class, IHarId
{
    public Dictionary<int, T> Data 
    {
        get
        {
            using DbContext context = CreateDbContext();
            Dictionary<int, T> dict = new Dictionary<int, T>();
            List<T> list = GetList();

            foreach (var item in list)
            {
                dict.Add(item.Id, item);
            }

            return dict;
        }
    }

    public virtual T? GetItem(int id)
    {
        if (!Data.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        return Data[id];
    }


    public virtual void Create(T element)
    {
        using DbContext context = CreateDbContext();
        int id = NextId();
        element.Id = id;

        context.Add(element);
        context.SaveChanges();
    }

    public virtual bool Delete(int id)
    {
        using DbContext context = CreateDbContext();

        T? t = context.Set<T>().Find(id);

        if (t == null)
        {
            return false;
        }

        context.Remove(t);
        return context.SaveChanges() > 0;
    }

    public virtual void Update(T t)
    {
        using DbContext context = CreateDbContext();

        context.Set<T>().Update(t);

        context.SaveChanges();
    }

    protected virtual IQueryable GetAllWithIncludes(DbContext context)
    {
        return context.Set<T>();
    }

    protected virtual DbContext CreateDbContext()
    {
        return new cralle_dk_db_sablouContext();
    }

    protected int NextId()
    {
        return Data.Select(t => t.Value.Id).DefaultIfEmpty(0).Max() + 1;
    }

    private List<T> GetList()
    {
        using DbContext context = CreateDbContext();

        return GetAllWithIncludes(context)
            .Cast<T>()
            .ToList();
    }


}
