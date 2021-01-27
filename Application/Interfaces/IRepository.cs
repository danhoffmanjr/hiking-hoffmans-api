using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> List();
        IAsyncEnumerable<T> ListAsync();
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        bool Add(T entity);
        Task<bool> AddAsync(T entity);
        bool Update(T entity);
        Task<bool> UpdateAsync(T entity);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}