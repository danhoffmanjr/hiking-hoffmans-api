using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Shared
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly hhDbContext context;
        private DbSet<T> entities;
        public Repository(hhDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> List()
        {
            return entities.AsEnumerable();
        }
        public T GetById(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        public bool Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);

            bool succeeded = context.SaveChanges() > 0;
            if (!succeeded) throw new RestException(HttpStatusCode.BadRequest, new { Save = $"Problem adding {nameof(T)}." });
            return succeeded;
        }
        public bool Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            bool succeeded = context.SaveChanges() > 0;
            if (!succeeded) throw new RestException(HttpStatusCode.BadRequest, new { Update = $"Problem updating {nameof(T)}." });
            return succeeded;
        }
        public bool Delete(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            T entity = entities.SingleOrDefault(s => s.Id == id);
            if (entity == null) throw new RestException(HttpStatusCode.NotFound, new { Delete = $"{nameof(T)} not found for deletion." });
            entities.Remove(entity);

            bool succeeded = context.SaveChanges() > 0;
            if (!succeeded) throw new RestException(HttpStatusCode.BadRequest, new { Delete = $"Problem deleting {nameof(T)}." });
            return succeeded;
        }

        public IAsyncEnumerable<T> ListAsync()
        {
            return entities.AsAsyncEnumerable();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            await entities.AddAsync(entity);

            bool succeeded = await context.SaveChangesAsync() > 0;
            if (!succeeded) throw new RestException(HttpStatusCode.BadRequest, new { Save = $"Problem adding {nameof(T)}." });
            return succeeded;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            bool succeeded = await context.SaveChangesAsync() > 0;
            if (!succeeded) throw new RestException(HttpStatusCode.BadRequest, new { Update = $"Problem updating {nameof(T)}." });
            return succeeded;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            T entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null) throw new RestException(HttpStatusCode.NotFound, new { Delete = $"{nameof(T)} not found for deletion." });
            entities.Remove(entity);

            bool succeeded = await context.SaveChangesAsync() > 0;
            if (!succeeded) throw new RestException(HttpStatusCode.BadRequest, new { Delete = $"Problem deleting {nameof(T)}." });
            return succeeded;
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate) => await context.Set<T>().FirstOrDefaultAsync(predicate);
    }
}