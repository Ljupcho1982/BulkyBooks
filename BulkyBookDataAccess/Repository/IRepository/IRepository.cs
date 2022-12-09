using System.Linq.Expressions;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFristorDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);


    }
}
