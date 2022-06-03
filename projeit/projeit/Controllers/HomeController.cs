using Core.Dto;
using Core.Models;
using Core.Services;
using Data;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projeit.Models;
using System.Diagnostics;

namespace projeit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected RoleManager<AppRole> _roleManager { get; set; }

        private readonly AppDbContext _db;

        public readonly IRoleService _roleService;
        protected UserManager<AppUser> _userManager { get; }
        protected SignInManager<AppUser> _signInManager { get; }

        private readonly IPersonService _personService;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, AppDbContext db, IRoleService roleService, IPersonService personService)
        {
            _db = db;

            _logger = logger;

            _userManager = userManager;

            _signInManager = signInManager;

            _roleManager = roleManager;

            _roleService = roleService;

            _personService = personService;
        }

 



        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

      
      

        [Authorize(Roles = "Personel")]
        public IActionResult Personel()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(string ReturnUrl)
        {

            TempData["ReturnUrl"] = ReturnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto UserLogin)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(UserLogin.Email);

                if (user != null)
                {
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınız Bir Süreligine Kitlenmiştir. Lütfen Daha Sonra Tekrar Deneyiniz");

                        return View(UserLogin);
                    }




                    // benim yazdıgım cooki varsa onu siler

                    await _signInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, UserLogin.Password, UserLogin.RememberMe, false);

                    //var sehend = _db.UserRoles.FirstOrDefault(x => x.UserId == user.Id);



                    // var sehend1 = _db.Roles.Where(x => x.Id ==sehend.RoleId).FirstOrDefault();


                    var sehend1 = _roleService.RoleChoose(user);






                    if (result.Succeeded && sehend1.Name == "Admin" )
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);


                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return RedirectToAction("Index", "Admin");
                    }
                    if (result.Succeeded && sehend1.Name == "Personel")
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);


                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return RedirectToAction("Index", "Personel");
                    }

                    if (result.Succeeded && sehend1.Name =="Boş")
                    {
                        
                        return RedirectToAction("AccessDenied");
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);

                        int Fail = await _userManager.GetAccessFailedCountAsync(user);

                        ModelState.AddModelError("", $"{Fail} Kez Başarısız Giriş.");

                        if (Fail == 3)
                        {
                            await _userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(DateTime.Now.AddMinutes(20)));

                            ModelState.AddModelError("", "Hesabınız 3 Başarısız Girişten Dolayı 20 Dakkika Süre İle Kitlenmiştir Lütfen Daha Sonra Sonra Tekrar Deneyiniz");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Geçersiz Email Adresi Veya Şifresi");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Bu Email'e Kayıtlı Kulanıcı Bulunamamıştır");
                }
            }

            return View(UserLogin);
        }




        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();

                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                IdentityResult result = await _userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(userViewModel);
        }
        public  IActionResult ResetPassword()
        {

         

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(PasswordDto passwordDto )
        {
            AppUser user = _userManager.FindByEmailAsync(passwordDto.Email).Result;


            if (user !=null)
            {
                string passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;

                await _userManager.ResetPasswordAsync(user, passwordResetToken, passwordDto.PasswordNew);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Hata");
            }

            
        }

        public IActionResult Hata()
        {



            return View();
        }

    }
}