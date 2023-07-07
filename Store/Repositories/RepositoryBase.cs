using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class , new()
    {
        protected readonly RepositoryContext _repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges
            ? _repositoryContext.Set<T>()
            : _repositoryContext.Set<T>().AsNoTracking();
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
           return trackChanges

                                ? _repositoryContext.Set<T>().Where(expression).SingleOrDefault()
                                : _repositoryContext.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
        }
    }
}