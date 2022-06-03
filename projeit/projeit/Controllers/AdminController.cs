using Core.Dto;
using Core.Models;
using Core.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace projeit.Controllers
{
    public class AdminController : Controller
    {
       

        protected UserManager<AppUser> _userManager { get; }
        protected SignInManager<AppUser> _signInManager { get; }

        public readonly IService<Contact>  _service;

        protected RoleManager<AppRole> _roleManager { get; }

        public AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IService<Contact> service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _service = service;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Roles()
        {
            return View(_roleManager.Roles.ToList());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminMessageBox()
        {

            var model= await _service.GetAllAsync();


            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetContactDetial(int id)
        {
            var model= await _service.GetByIdAsync(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SendMail()
        {


            return View();
        }

        [HttpPost]
        public  IActionResult SendMail(Contact  contact,IFormFile EklenenData)
        {

            if (EklenenData!=null&& EklenenData.Length>0)
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

            return RedirectToAction("AdminMessageBox");
        }

        



      
        public IActionResult RolCreat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RolCreat(RoleDto roleViewModels)
        {

            AppRole Role = new AppRole();

            Role.Name = roleViewModels.Name;

            IdentityResult result = _roleManager.CreateAsync(Role).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }
            else
            {

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(roleViewModels);
        }



        public IActionResult RoleDelete(string id)
        {
            AppRole role = _roleManager.FindByIdAsync(id).Result;

            if (role != null)
            {
                IdentityResult result = _roleManager.DeleteAsync(role).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    ViewBag.error = "Bir Hata Meydana Geldi";
                }
            }

            return View();
        }

        public IActionResult RolApdate(string id)
        {

            AppRole role = _roleManager.FindByIdAsync(id).Result;

            return View(role.Adapt<RoleDto>());

        }

        [HttpPost]
        public IActionResult RolApdate(RoleDto roleViewModels)
        {

            AppRole role = _roleManager.FindByIdAsync(roleViewModels.Id).Result;

            if (role != null)
            {
                role.Name = roleViewModels.Name;

                IdentityResult result = _roleManager.UpdateAsync(role).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {


                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Günceleme İşlemi Başarısız oldu");
            }

            return View(roleViewModels);
        }

        public IActionResult RoleAssign(string id)
        {
            TempData["userId"] = id;

            AppUser user = _userManager.FindByIdAsync(id).Result;

            ViewBag.userName = user.UserName;

            IQueryable<AppRole> roles = _roleManager.Roles;

            /*  kest işlemi as ile başlayayan  */

            List<string> userRoles = _userManager.GetRolesAsync(user).Result as List<string>;

            List<RoleAssignDto> roleAssignViewModels = new List<RoleAssignDto>();

            foreach (var role in roles)
            {
                RoleAssignDto r = new RoleAssignDto();
                r.RoleId = role.Id;
                r.RoleName = role.Name;
                if (userRoles.Contains(role.Name))
                {

                    r.Exist = true;
                }
                else
                {

                    r.Exist = false;
                }

                roleAssignViewModels.Add(r);
            }

            return View(roleAssignViewModels);

        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignDto> roleAssignViewModels)
        {
            AppUser user = _userManager.FindByIdAsync(TempData["userId"].ToString()).Result;

            foreach (var item in roleAssignViewModels)
            {
                if (item.Exist)

                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }

            return RedirectToAction("Users");
        }



        public IActionResult Users()
        {



            return View(_userManager.Users.ToList());
        }

        public IActionResult UsersAdmin()
        {



            return View(_userManager.Users.ToList());
        }

        public IActionResult AdminLogout()
        {

            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


    }
}
