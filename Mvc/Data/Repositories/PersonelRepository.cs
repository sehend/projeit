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
  public  class PersonelRepository : IRepository<AnaTable>
    {
        AppDbContext _context = new AppDbContext();

        Repository<AnaTable> repository = new Repository<AnaTable>();

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public AnaTable Get(int id)
        {
            return repository.Get(id);
        }

        public IEnumerable<AnaTable> GetAll()
        {
            return repository.GetAll();
        }

        public AnaTable GetBookByID(int id)
        {
            return repository.GetBookByID(id);
        }

        public List<AnaTable> List(Expression<Func<AnaTable, bool>> Filter)
        {
            return repository.List(Filter);
        }

        public void Save(AnaTable student)
        {
            repository.Save(student);
        }

        public void Update(AnaTable student)
        {
            repository.Update(student);
        }

        public IEnumerable<AnaTable> WherePersonel(IEnumerable<AnaTable> anaTable , string p,string ad)
        {

          var  model = anaTable.Where(m => m.Tarih == p && m.Alıcı==ad);

            return model;
        }

        public IEnumerable<AnaTable> WhereAdmin(IEnumerable<AnaTable> anaTable, string p)
        {

            var model = anaTable.Where(m => m.Tarih == p );

            return model;
        }
    }
}
