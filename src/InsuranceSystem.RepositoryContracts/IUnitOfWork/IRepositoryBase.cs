
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.RepositoryContracts.IUnitOfWork
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges);
        void Create(T entity);
        void CreateBulk(List<T> entity);
        void Update(T entity);
        void UpdateBulk(List<T> entity);
        void Delete(T entity);
    }
}
