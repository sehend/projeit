using Core.Models;
using Core.Services;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _db;

        public RoleService(AppDbContext db)
        {
            _db = db;
        }

        public AppRole RoleChoose(AppUser appUser)
        {
            var sehend = _db.UserRoles.FirstOrDefault(x => x.UserId == appUser.Id);



           

            if (sehend != null)
            {
                var sehend1 = _db.Roles.Where(x => x.Id == sehend.RoleId).FirstOrDefault();

                return sehend1;
            }
            else
            {
                AppRole appRole = new AppRole();

                appRole.Name = "Boş";




                return appRole;
            }
            
          

          
        }

        public AppRole RoleChoosePersonelGiris(Contact contact)
        {

            var sehend = _db.Users.FirstOrDefault(x => x.UserName == contact.UserName && x.Email==contact.SendMail);

            if (sehend!=null)
            {
                var sehend1 = _db.UserRoles.FirstOrDefault(x => x.UserId == sehend.Id);
                var sehend2 = _db.Roles.FirstOrDefault(x => x.Id == sehend1.RoleId);

                return sehend2;
            }
            else
            {

                AppRole appRole = new AppRole();

                appRole.Name = "Boş";

               


                return appRole;
            }
         



           
        }
    }
}
