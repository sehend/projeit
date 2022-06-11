using Core.Model;
using Data;
using Data.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication5.Controllers
{
    public class projectableController : Controller
    {


        LoginRepository login = new LoginRepository();

        PersonelRepository repository = new PersonelRepository();

        UserRepository UserApp = new UserRepository();

        Repository<Sponsor> Sponsor = new Repository<Sponsor>();

        Repository<HastaUzmanlık> HastaUzmanlık = new Repository<HastaUzmanlık>();

        Repository<KanserTürleri> KanserTürleri = new Repository<KanserTürleri>();

        Repository<MateryalTipi> MateryalTipi = new Repository<MateryalTipi>();

        Repository<TüpCinsi> TüpCinsi = new Repository<TüpCinsi>();

    
        // GET: projectable
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {


            TempData["Veri"] = appUser.Email;


            TempData["Veri1"] = appUser.Name;

            var model = login.FirstOrDefaultGiris(appUser);
          


            if ( model!=null && model.AdminRole == "Admin")
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);

               
                return RedirectToAction("Liste");
            }

            if (model != null && model.AdminRole == "Personel")
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);





                return RedirectToAction("PersonelAnaSayfa");
            }
            else
            {
                return RedirectToAction("Login");
            }



        }



        [Authorize(Roles="Admin")]

        public ActionResult Liste( string p ,int sayfa=1 )
        {
            var sponsors2 = Sponsor.GetAll();

            ViewData["sponsors1"] = sponsors2;

            var HastaUzmanlıks = HastaUzmanlık.GetAll();

            ViewData["HastaUzmanlık"] = HastaUzmanlıks;

            var KanserTürleris = KanserTürleri.GetAll();

            ViewData["KanserTürleri"] = KanserTürleris;

            var MateryalTipis = MateryalTipi.GetAll();

            ViewData["MateryalTipi"] = MateryalTipis;

            var TüpCinsis = TüpCinsi.GetAll();

            ViewData["TüpCinsi"] = TüpCinsis;


         

                var model = repository.GetAll();

                var degerler = from d in model select d;

                if (!string.IsNullOrEmpty(p))
                {
                degerler = repository.WhereAdmin(degerler,p);
                }

                return View(degerler.ToList().ToPagedList(sayfa, 5));
               
        
           
               
       



            
            
        }
        [HttpGet]
        public ActionResult Create()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult Create(AnaTable projectable)
        {
            repository.Save(projectable);

            return RedirectToAction("Liste");
        }

        public ActionResult Delete(int id)
        {
           

            repository.Delete(id);

            return RedirectToAction("Liste");
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int id)
        {
            var sponsors2 = Sponsor.GetAll();

            ViewData["sponsors1"] = sponsors2;

            var HastaUzmanlıks = HastaUzmanlık.GetAll();

            ViewData["HastaUzmanlık"] = HastaUzmanlıks;

            var KanserTürleris = KanserTürleri.GetAll();

            ViewData["KanserTürleri"] = KanserTürleris;

            var MateryalTipis = MateryalTipi.GetAll();

            ViewData["MateryalTipi"] = MateryalTipis;

            var TüpCinsis = TüpCinsi.GetAll();

            ViewData["TüpCinsi"] = TüpCinsis;

         
            var model = repository.GetBookByID(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(AnaTable  projectable)
        {

            repository.Update(projectable);

            

            return RedirectToAction("Liste");
            
          
        }

        [Authorize(Roles = "Admin")]

        public ActionResult SponsorListe(int sayfa=1)
        {
            var model = Sponsor.GetAll().ToPagedList(sayfa, 5);

            return View(model);
        }
        [HttpGet]
        public ActionResult SponsorCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult SponsorCreate(Sponsor  sponsor)
        {
            Sponsor.Save(sponsor);

            return RedirectToAction("SponsorListe");
        }

        public ActionResult SponsorDelete(int id)
        {


            Sponsor.Delete(id);

            return RedirectToAction("SponsorListe");
        }
        [Authorize(Roles = "Admin")]

        public ActionResult SponsorEdit(int id)
        {
            var model = Sponsor.GetBookByID(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult SponsorEdit(Sponsor sponsor)
        {

            Sponsor.Update(sponsor);



            return RedirectToAction("SponsorListe");


        }










        [Authorize(Roles = "Admin")]


        public ActionResult HastaUzmanlıkListe(int sayfa = 1)
        {
            var model = HastaUzmanlık.GetAll().ToPagedList(sayfa, 5); 

            return View(model);
        }
        [HttpGet]
        public ActionResult HastaUzmanlıkCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult HastaUzmanlıkCreate(HastaUzmanlık hastaUzmanlık)
        {
            HastaUzmanlık.Save(hastaUzmanlık);

            return RedirectToAction("HastaUzmanlıkListe");
        }

        public ActionResult HastaUzmanlıkDelete(int id)
        {


            HastaUzmanlık.Delete(id);

            return RedirectToAction("HastaUzmanlıkListe");
        }
        [Authorize(Roles = "Admin")]

        public ActionResult HastaUzmanlıkEdit(int id)
        {
            var model = HastaUzmanlık.GetBookByID(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult HastaUzmanlıkEdit(HastaUzmanlık  hastaUzmanlık)
        {

            HastaUzmanlık.Update(hastaUzmanlık);



            return RedirectToAction("HastaUzmanlıkListe");


        }










        [Authorize(Roles = "Admin")]

        public ActionResult KanserTürleriListe(int sayfa=1)
        {
            var model = KanserTürleri.GetAll().ToPagedList(sayfa, 5);

            return View(model);
        }
        [HttpGet]
        public ActionResult KanserTürleriCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult KanserTürleriCreate(KanserTürleri  kanserTürleri)
        {
            KanserTürleri.Save(kanserTürleri);

            return RedirectToAction("KanserTürleriListe");
        }

        public ActionResult KanserTürleriDelete(int id)
        {


            KanserTürleri.Delete(id);

            return RedirectToAction("KanserTürleriListe");
        }
        [Authorize(Roles = "Admin")]

        public ActionResult KanserTürleriEdit(int id)
        {
            var model = KanserTürleri.GetBookByID(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult KanserTürleriEdit(KanserTürleri  kanserTürleri)
        {

            KanserTürleri.Update(kanserTürleri);



            return RedirectToAction("KanserTürleriListe");


        }











        [Authorize(Roles = "Admin")]

        public ActionResult MateryalTipiListe(int sayfa=1)
        {
            var model = MateryalTipi.GetAll().ToPagedList(sayfa, 5);

            return View(model);
        }
        [HttpGet]
        public ActionResult MateryalTipiCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult MateryalTipiCreate(MateryalTipi  materyalTipi)
        {
            MateryalTipi.Save(materyalTipi);

            return RedirectToAction("MateryalTipiListe");
        }

        public ActionResult MateryalTipiDelete(int id)
        {


            MateryalTipi.Delete(id);

            return RedirectToAction("MateryalTipiListe");
        }
        [Authorize(Roles = "Admin")]

        public ActionResult MateryalTipiEdit(int id)
        {
            var model = MateryalTipi.GetBookByID(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult MateryalTipiEdit(MateryalTipi materyalTipi)
        {

            MateryalTipi.Update(materyalTipi);



            return RedirectToAction("MateryalTipiListe");


        }









        [Authorize(Roles = "Admin")]

        public ActionResult TüpCinsiListe(int sayfa=1)
        {
            var model = TüpCinsi.GetAll().ToPagedList(sayfa, 5);

            return View(model);
        }
        [HttpGet]
        public ActionResult TüpCinsiCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult TüpCinsiCreate(TüpCinsi  tüpCinsi)
        {
            TüpCinsi.Save(tüpCinsi);

            return RedirectToAction("TüpCinsiListe");
        }

        public ActionResult TüpCinsiDelete(int id)
        {


            TüpCinsi.Delete(id);

            return RedirectToAction("TüpCinsiListe");
        }
        [Authorize(Roles = "Admin")]

        public ActionResult TüpCinsiEdit(int id)
        {
            var model = TüpCinsi.GetBookByID(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult TüpCinsiEdit(TüpCinsi  tüpCinsi)
        {

            TüpCinsi.Update(tüpCinsi);



            return RedirectToAction("TüpCinsiListe");


        }

















        [HttpGet]
        public ActionResult PersonelCreate()
        {
            

          

            return View();
        }
        [HttpPost]
        public ActionResult PersonelCreate(AnaTable projectable)
        {
            repository.Save(projectable);

            return RedirectToAction("Liste");
        }

        [Authorize(Roles = "Personel")]
        public ActionResult PersonelAnaSayfa(string p, int sayfa=1)
        {
            var sponsors2 = Sponsor.GetAll();

            ViewData["sponsors1"] = sponsors2;

            var HastaUzmanlıks = HastaUzmanlık.GetAll();

            ViewData["HastaUzmanlık"] = HastaUzmanlıks;

            var KanserTürleris = KanserTürleri.GetAll();

            ViewData["KanserTürleri"] = KanserTürleris;

            var MateryalTipis = MateryalTipi.GetAll();

            ViewData["MateryalTipi"] = MateryalTipis;

            var TüpCinsis = TüpCinsi.GetAll();

            ViewData["TüpCinsi"] = TüpCinsis;

            

            var name = TempData["Veri1"];


            var model = repository.GetAll();

            var degerler = from d in model select d;
            AppDbContext appDb = new AppDbContext();
            if (!string.IsNullOrEmpty(p))
            {
                degerler = appDb.anaTables.Where(m => m.Tarih == p && m.Alıcı == name);
            }

            return View(degerler.ToList().ToPagedList(sayfa, 5));




        }



        public ActionResult PersonelDelete(int id)
        {


            repository.Delete(id);

            return RedirectToAction("PersonelAnaSayfa", "projectable");
        }


        [Authorize(Roles = "Personel")]

        public ActionResult PersonelEdit(int id)
        {
            var sponsors2 = Sponsor.GetAll();

            ViewData["sponsors1"] = sponsors2;

            var HastaUzmanlıks = HastaUzmanlık.GetAll();

            ViewData["HastaUzmanlık"] = HastaUzmanlıks;

            var KanserTürleris = KanserTürleri.GetAll();

            ViewData["KanserTürleri"] = KanserTürleris;

            var MateryalTipis = MateryalTipi.GetAll();

            ViewData["MateryalTipi"] = MateryalTipis;

            var TüpCinsis = TüpCinsi.GetAll();

            ViewData["TüpCinsi"] = TüpCinsis;


            var model = repository.GetBookByID(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult PersonelEdit(AnaTable projectable)
        {

            repository.Update(projectable);


            return RedirectToAction("PersonelAnaSayfa", "projectable");


        }

        [Authorize(Roles = "Admin")]


        public ActionResult KulanıcıListele(string p, int sayfa = 1)
        {

            var model = UserApp.GetAll();

            var degerler = from d in model select d;

            if (!string.IsNullOrEmpty(p))
            {
                degerler = UserApp.WhereUser(degerler, p);
            }

            return View(degerler.ToList().ToPagedList(sayfa, 5));

           


        }

        [HttpGet]
        public ActionResult KulanıcıCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult KulanıcıCreate(AppUser projectable)
        {
            UserApp.Save(projectable);

            return RedirectToAction("KulanıcıListele");
        }

        public ActionResult KulanıcıEdit(int id)
        {
        


            var model = repository.GetBookByID(id);

            return View(model);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public ActionResult KulanıcıEdit(AppUser projectable)
        {

            UserApp.Update(projectable);



            return RedirectToAction("KulanıcıListele");


        }
        public ActionResult KulanıcıDelete(int id)
        {


            UserApp.Delete(id);

            return RedirectToAction("KulanıcıListele");
        }

        [Authorize(Roles = "Personel")]
        public ActionResult AdminyetkisizSayfa()
        {


            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PersonelyetkisizSayfa()
        {


            return View();
        }

    }
}