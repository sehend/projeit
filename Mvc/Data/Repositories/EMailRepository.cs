using Core.IRepostories;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
  public  class EMailRepository :IRepository<EMailTable>
    {
        AppDbContext _context = new AppDbContext();

        Repository<EMailTable> repository = new Repository<EMailTable>();

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public EMailTable Get(int id)
        {
            return repository.Get(id);
        }

        public IEnumerable<EMailTable> GetAll()
        {       
            return repository.GetAll();
        }

        public EMailTable GetBookByID(int id)
        {
            return repository.GetBookByID(id);
        }

        public List<EMailTable> List(Expression<Func<EMailTable, bool>> Filter)
        {
            return repository.List(Filter);
        }

        public void Save(EMailTable student)
        {
            repository.Save(student);
        }

        public void Update(EMailTable student)
        {
            repository.Update(student);
        }

        public EMailTable GetEmail(EMailTable eMailTable)
        {

            


            return eMailTable;

        }


    }
}
