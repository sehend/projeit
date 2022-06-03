using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPersonService
    {
        AppUser GetByPerson(Contact contact);

        void ChangeStatus(Contact contact, int id);

        

        
    }
}
