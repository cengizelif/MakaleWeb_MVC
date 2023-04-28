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
            return View();
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(RegisterModel model)
        {
            KullaniciYonet ky = new KullaniciYonet();
                      
            //Kayıt işlemi yapılacak
            //Aktivasyon maili gönderilecek

            if(ModelState.IsValid)
            {
              MakaleBLLSonuc<Kullanici> sonuc=ky.KullaniciBul(model);

                if(sonuc.hatalar.Count>0)
                {
                    //ModelState.AddModelError("", "Bu kullanıcı adı yada email kayıtlı");
                    sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);  
                }
                else
                {
                    //database kaydet
                    return RedirectToAction("Giris");
                }

               
                //Kullanıcı adı ve email varmı kontrolu
            }
            return View(model);
        }




        //public PartialViewResult kategoriPartial()
        //{
        //    KategoriYonet ky = new KategoriYonet();
        //    List<Kategori> liste = ky.Listele();
        //    return PartialView("_PartialPagekat2", liste);
        //}
    }
}