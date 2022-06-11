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
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }


        EMailRepository  mailRepository = new EMailRepository();

        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
        

           
          


                return RedirectToAction("Liste","projectable");
           
           

        }


        public ActionResult Liste(string p)
        {



            var model = mailRepository.GetAll();

            var degerler = from d in model select d;

            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.Message.Contains(p));
            }

            return View(degerler.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Create(List<EMailTable>  eMailTable)
        {
            mailRepository.Save(eMailTable[0]);

            return RedirectToAction("Liste");
        }

        public ActionResult Delete(int id)
        {


            mailRepository.Delete(id);

            return RedirectToAction("Liste");
        }





    }
}