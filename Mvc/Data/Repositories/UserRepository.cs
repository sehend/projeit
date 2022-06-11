using Core.IRepostories;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
   public  class UserRepository : IRepository<AppUser>
    {
        AppDbContext _context = new AppDbContext();

        Repository<AppUser> repository = new Repository<AppUser>();

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public AppUser Get(int id)
        {
            return repository.Get(id);
        }

        public IEnumerable<AppUser> GetAll()
        {
            return repository.GetAll();
        }

        public AppUser GetBookByID(int id)
        {
            return repository.GetBookByID(id);
        }

        public List<AppUser> List(Expression<Func<AppUser, bool>> Filter)
        {
            return repository.List(Filter);
        }

        public void Save(AppUser student)
        {
            repository.Save(student);
        }

        public void Update(AppUser student)
        {
            repository.Update(student);
        }

        public IEnumerable<AppUser> WhereUser(IEnumerable<AppUser> anaTable, string p)
        {

            var model = anaTable.Where(m => m.Email == p);

            return model;
        }
    }
}
