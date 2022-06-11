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
    public class LoginRepository : IRepository<AppUser>
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

        public AppUser FirstOrDefaultLogin(string password)
        {

            var model = _context.Users.FirstOrDefault(m => m.Password == password);

            return model;
        }

        public AppUser FirstOrDefaultGiris(AppUser appUser)
        {

            var model = _context.Users.FirstOrDefault(x => x.Email == appUser.Email && x.Password == appUser.Password);

            return model;
        }
    }
}
