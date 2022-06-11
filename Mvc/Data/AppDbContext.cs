using Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
             : base("IdentityConnection")
        {
        }
        public DbSet<AppUser> Users { get; set; }

        public DbSet<AnaTable>   anaTables { get; set; }

        public DbSet<EMailTable>  eMailTables { get; set; }

        public DbSet<Sponsor>  Sponsors { get; set; }

        public DbSet<HastaUzmanlık>  HastaUzmanlıks { get; set; }

        public DbSet<KanserTürleri>  kanserTürleris { get; set; }

        public DbSet<MateryalTipi>  materyalTipis { get; set; }

        public DbSet<TüpCinsi>  tüpCinsis { get; set; }


    }
}
