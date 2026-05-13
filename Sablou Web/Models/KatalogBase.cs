using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;

namespace Sablou_Web.Models;

public abstract class KatalogBase: PageModel
{
    protected abstract string Højtidsnavn { get; }
    public IDataService Repo { get; }
    public KatalogBase(IDataService repo)
    {
        Repo = repo;
    }

    public HøjtidsKatalog Kataloget
    {
        get
        {
            return Repo.HøjtidsKatalogRepository.Data
                .Select(t => t.Value)
                .First(t => t.Højtidsnavn == Højtidsnavn);
        }
    }
    public List<Chokolade> Chokolader
    {
        get
        {
            {
                var a = Repo.ChokoladerIKatalogRepository.Data.Select(t => t).Where(t => t.Value.KatalogId == Kataloget.Id);
                List<Chokolade> list = new List<Chokolade>();
                foreach (var item in a)
                {
                    if (Repo.ChokoladeRepository.Data.ContainsKey(item.Value.ChokoladeId))
                    {
                        list.Add(Repo.ChokoladeRepository.GetItem(item.Value.ChokoladeId));
                    }
                }
                return list;
            }
        }
    }
}

