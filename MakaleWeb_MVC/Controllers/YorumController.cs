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
    }
}