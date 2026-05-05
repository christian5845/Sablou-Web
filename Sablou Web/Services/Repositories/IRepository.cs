using Sablou_Web.Models;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

namespace Sablou_Web.Services.Repositories;

public interface IRepository<T> where T : IHarId
{
    Dictionary<int,T> Data { get; }

    T? GetItem(int id);

    void Create(T t);

    bool Delete(int id);

    void Update(T t);
}
