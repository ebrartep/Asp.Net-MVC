using CorporateWebSite__MVC.Models.DataContext;
using CorporateWebSite__MVC.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CorporateWebSite__MVC.Controllers
{
    public class HakkimizdaController : Controller
    {
        //bağlantı sayfası
        KurumsalDBContext db = new KurumsalDBContext();
        // GET: Hakkimizda
        [Route("yonetimpaneli/andropia/hakkimizda")]
        public ActionResult Index()
        {

            var h = db.Hakkimizda.ToList();
            return View(h);
        }
        public ActionResult Edit(int id)
        {

            var h = db.Hakkimizda.Where(x=> x.hakkimizdaId ==id).FirstOrDefault();

            return View(h);
        }
        [HttpPost]
        //gönderme işlemende forgery token ile gödneriyor bizde bununlakarşılıyoruz
        //güvenlik için önemli
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id,Hakkimizda hakkimizda)
        {
            if (ModelState.IsValid)
            {
                var h = db.Hakkimizda.Where(x => x.hakkimizdaId == id).SingleOrDefault();

                h.hakkimizdaAciklama = hakkimizda.hakkimizdaAciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hakkimizda);
        }

    }
}