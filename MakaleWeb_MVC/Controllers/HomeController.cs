using MakaleBLL;
using MakaleEntities;
using MakaleEntities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakaleWeb_MVC.Controllers
{
    public class HomeController : Controller
    {
        MakaleYonet my = new MakaleYonet();
        KategoriYonet ky = new KategoriYonet();
        KullaniciYonet kuly = new KullaniciYonet();
        public ActionResult Index()
        {
            // Test test = new Test();
            //  test.EkleTest();
            // test.UpdateTest();
            // test.DeleteTest();
            // test.YorumTest();         
          
            return View(my.Listele());
        }

        public ActionResult Kategori(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Kategori kat = ky.KategoriBul(id.Value);
            
            return View("Index",kat.Makaleler);
        }

        public ActionResult EnBegenilenler()
        {
            return View("Index",my.Listele().OrderByDescending(x=>x.BegeniSayisi).ToList());
        }

        public ActionResult SonYazilanlar()
        {
            return View("Index", my.Listele().OrderByDescending(x => x.DegistirmeTarihi).ToList());
        }

        public ActionResult Hakkımızda()
        {
            return View();
        }

      
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(LoginModel model)
        {
            if(ModelState.IsValid)
            {
              MakaleBLLSonuc<Kullanici> sonuc=kuly.LoginKontrol(model);

                if(sonuc.hatalar.Count>0)
                {
                    sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);  
                }

                Session["login"] = sonuc.nesne;
                return RedirectToAction("Index");

            }         

            return View(model);
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(RegisterModel model)
        {         

            if(ModelState.IsValid)
            {
              MakaleBLLSonuc<Kullanici> sonuc=kuly.KullaniciKaydet(model);

                if(sonuc.hatalar.Count>0)
                {
                    //ModelState.AddModelError("", "Bu kullanıcı adı yada email kayıtlı");
                    sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);  
                }
                else
                {

                    return RedirectToAction("KayitBasarili");
                }
               
          
            }
            return View(model);
        }

        public ActionResult KayitBasarili()
        {
            return View();
        }

        public ActionResult HesapAktiflestir(Guid id)
        {
            MakaleBLLSonuc<Kullanici> sonuc=kuly.ActivateUser(id);
            if(sonuc.hatalar.Count>0)
            {
                TempData["hatalar"] = sonuc.hatalar;
                return RedirectToAction("ActivateError");
            }
          
            return View();  
        }

        public ActionResult Cikis()
        {
            Session["login"] = null;
            return RedirectToAction("Index");  
        }

        //public PartialViewResult kategoriPartial()
        //{
        //    KategoriYonet ky = new KategoriYonet();
        //    List<Kategori> liste = ky.Listele();
        //    return PartialView("_PartialPagekat2", liste);
        //}
    }
}