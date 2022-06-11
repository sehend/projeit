using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepostories
{
    public interface IRepository<TEntity>
    {
     

        List<TEntity> List(Expression<Func<TEntity, bool>> Filter);

        void Save(TEntity student);
        IEnumerable<TEntity> GetAll();
        TEntity GetBookByID(int id);
        TEntity Get(int id);
        void Delete(int id);
        void Update(TEntity student);
    }
}
