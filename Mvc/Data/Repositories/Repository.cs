using Core.IRepostories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        AppDbContext _context = new AppDbContext();

        DbSet<TEntity> _dbSet;

        public Repository()
        {
            _dbSet = _context.Set<TEntity>();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);

            _dbSet.Remove(entity);

            _context.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetBookByID(int id)
        {
            return _dbSet.Find(id);
        }

        public List<TEntity> List(Expression<Func<TEntity, bool>> Filter)
        {
            return _dbSet.Where(Filter).ToList();
        }

        public void Save(TEntity student)
        {
            
            _dbSet.Add(student);

            _context.SaveChanges();
        }

        public void Update(TEntity student)
        {
            var updateentity = _context.Entry(student);

            updateentity.State = EntityState.Modified;

            _context.SaveChanges();


        }

       
    }
}
