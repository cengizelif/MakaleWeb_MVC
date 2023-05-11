using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MakaleBLL;
using MakaleEntities;

namespace MakaleWeb_MVC.Controllers
{
    public class YorumController : Controller
    {
        // GET: Yorum
        public ActionResult YorumGoster(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MakaleYonet my = new MakaleYonet();

            Makale makale=my.MakaleBul(id.Value);

            if (makale == null) 
            { 
               return HttpNotFound();   
            }

            return PartialView("_PartialPageYorumlar",makale.Yorumlar);
        }

        public ActionResult YorumGuncelle(int? id,string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            YorumYonet yy = new YorumYonet();
            Yorum yorum=yy.YorumBul(id.Value);

            if (yorum == null) 
            {
                return HttpNotFound();
            }

            yorum.Text= text;   

            if(yy.YorumUpdate(yorum)>0)
            {
                return Json(new { hata = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { hata = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            YorumYonet yy = new YorumYonet();
            Yorum yorum = yy.YorumBul(id.Value);

            if (yorum == null)
            {
                return HttpNotFound();
            }

            if (yy.yorumSil(yorum) > 0)
            {
                return Json(new { hata = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { hata = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumEkle(Yorum nesne,int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            YorumYonet yy = new YorumYonet();
            Yorum yorum = yy.YorumBul(id.Value);

            if (yorum == null)
            {
                return HttpNotFound();
            }

            if (yy.yorumEkle(yorum) > 0)
            {
                return Json(new { hata = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { hata = true }, JsonRequestBehavior.AllowGet);
        }


    }
}