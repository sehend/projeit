using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);


       

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

      
        Task AddAsync(TEntity entity);

        void Save(TEntity entity);


        Task AddRangeAsync(IEnumerable<TEntity> entities);

        

        void Remove(TEntity entity);

      

        void RemoveRange(IEnumerable<TEntity> entities);


        TEntity Update(TEntity entity);




    }
}
