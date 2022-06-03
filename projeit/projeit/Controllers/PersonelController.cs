using Core.Models;
using Core.Services;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace projeit.Controllers
{
    public class PersonelController : Controller
    {
        private readonly AppDbContext _db;

        private readonly IPersonService _personService;

        public readonly IService<Contact> _service;

        public readonly IRoleService _roleService;

        protected SignInManager<AppUser> _signInManager { get; }

        public PersonelController(AppDbContext db , IService<Contact> service, IPersonService personService, SignInManager<AppUser> signInManager, IRoleService roleService)
        {
            _signInManager = signInManager;

            _service = service;

            _db = db;

            _personService = personService;

            _roleService = roleService;
        }

        public IActionResult DogruAd()
        {



            return View();
        }
        [Authorize(Roles = "Personel")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Personel")]
        public IActionResult PersonelGiris()
        {

        

            return View();
        }



        [Authorize(Roles = "Personel")]
        [HttpPost]
        public IActionResult PersonelGiris(Contact UserName1)
        {

            var person = _personService.GetByPerson(UserName1);



            var sehend1 = _roleService.RoleChoosePersonelGiris(UserName1);

            if (person != null && sehend1.Name=="Personel")
            {
                var sehend = UserName1.UserName;

                HttpContext.Session.SetString("UserName", sehend);

                return RedirectToAction("PersonelMessageBox");
                
            }
            else
            {
                return RedirectToAction("DogruAd");
            }
         
        }

        [Authorize(Roles = "Personel")]
        public IActionResult PersonelMessageBox()
        {


            var content = HttpContext.Session.GetString("UserName");
              

            var model = _db.contacts.Where(x => x.UserName == content).ToList();

            return View(model);
        }

        [Authorize(Roles = "Personel")]
        public IActionResult SendMailPersonel()
        {


            return View();
        }

        [HttpPost]
        public IActionResult SendMailPersonel(Contact contact, IFormFile EklenenData)
        {
            if (EklenenData != null && EklenenData.Length > 0)
            {




                if ((EklenenData.Length) < 25000000)
                {
                    var FileName = Guid.NewGuid().ToString() + Path.GetExtension(EklenenData.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BaseData", FileName);

                    using (var steam = new FileStream(path, FileMode.Create))
                    {
                        EklenenData.CopyTo(steam);

                        contact.data = "/BaseData/" + FileName;
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }



            }



            _service.Save(contact);

         

            return RedirectToAction("PersonelMessageBox");
        }

        public IActionResult PersonelLogout()
        {

            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Personel")]
        public async Task<IActionResult> PersonelStatus(int id)
        {

            var contact = await _service.GetByIdAsync((id));

            HttpContext.Session.SetInt32("age", id);

            return View(contact);
        }
        [HttpPost]
        public IActionResult PersonelStatus(Contact contact)
        {
            var model = HttpContext.Session.GetInt32("age");

            _personService.ChangeStatus(contact, (int)model);

            return RedirectToAction("PersonelMessageBox");
        }

        public async Task<IActionResult> GetContactPersonel(int id)
        {
            var model = await _service.GetByIdAsync(id);

            return View(model);
        }

    }
}
