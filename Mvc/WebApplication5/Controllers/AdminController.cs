using Core.Model;
using Data;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication5.Controllers
{
    public class AdminController : Controller
    {
        Repository<AppUser> repository = new Repository<AppUser>();



       
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Create()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Create(AppUser appUser)
        {
            
            repository.Save(appUser);
           

            return RedirectToAction("Login", "projectable");
        }
       

    }
}