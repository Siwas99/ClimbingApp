using ClimbingApp.Models;
using ClimbingApp.Repositories;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IRepository<T>
    {
        List<T> List();
        bool Update(T obj);
        bool Insert(T obj);
        bool Delete(int id);
        T GetById(int id);

    }
}