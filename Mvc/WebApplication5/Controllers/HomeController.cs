using Core.Model;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {

       

        LoginRepository login = new LoginRepository();

        PersonelRepository repository = new PersonelRepository();

        Repository<AnaTable> ana = new Repository<AnaTable>();

        Repository<Sponsor> Sponsor = new Repository<Sponsor>();

        Repository<HastaUzmanlık> HastaUzmanlık = new Repository<HastaUzmanlık>();

        Repository<KanserTürleri> KanserTürleri = new Repository<KanserTürleri>();

        Repository<MateryalTipi> MateryalTipi = new Repository<MateryalTipi>();

        Repository<TüpCinsi> TüpCinsi = new Repository<TüpCinsi>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       

        [HttpGet]
        public ActionResult PersonelCreate()
        {


            return View();
        }
        

        [HttpPost]
        public ActionResult PersonelCreate(AnaTable projectable)
        {
            ana.Save(projectable);

            return RedirectToAction("PersonelAnaSayfa", "projectable");
        }


        public ActionResult YetkinYok()
        {
           

            return View();
        }


    }
}