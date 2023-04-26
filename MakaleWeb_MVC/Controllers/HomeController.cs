using MakaleBLL;
using MakaleEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakaleWeb_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Test test = new Test();
            //  test.EkleTest();
            // test.UpdateTest();
            // test.DeleteTest();
            // test.YorumTest();

            MakaleYonet my=new MakaleYonet();
          
            return View(my.Listele());
        }

        public PartialViewResult kategoriPartial()
        {
            KategoriYonet ky = new KategoriYonet();
            List<Kategori> liste = ky.Listele();
            return PartialView("_PartialPagekat2", liste);
        }
    }
}