using Core.Models;
using Core.Services;
using Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _db;

        protected UserManager<AppUser> _userManager { get; set; }

        public PersonService(AppDbContext db, UserManager<AppUser> userManager)
        {
            _userManager=userManager;

            _db = db;
        }

        public void ChangeStatus(Contact contact,int id)
        {
            var model2 = _db.contacts.FirstOrDefault(x => x.ContactId == id);

            model2.Statusu = contact.Statusu;

            _db.Update(model2);

            _db.SaveChanges();
        }

        public AppUser GetByPerson(Contact contact)
        {
            var person = _db.Users.FirstOrDefault(x => x.UserName == contact.UserName);

            return person;
        }

       
    }
}
