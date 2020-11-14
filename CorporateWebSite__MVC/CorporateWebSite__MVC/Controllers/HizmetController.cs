using CorporateWebSite__MVC.Models.DataContext;
using CorporateWebSite__MVC.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CorporateWebSite__MVC.Controllers
{
    public class HizmetController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Hizmet
        [Route("yonetimpaneli/andropia/hizmet")]

        public ActionResult Index()
        {
            return View(db.Hizmet.ToList());
        }
        public ActionResult Create(){
            return View();

        }

        [HttpPost]
        //request ile alakalı bir tehlike algılarsa
        [ValidateInput(false)]
        public ActionResult Create(Hizmet hizmet,HttpPostedFileBase hizmetResimURL)
        {
            if (ModelState.IsValid)
            {
                if (hizmetResimURL != null)
                {
                 
                    //resim ekleme için webImage kütüphanesi
                    WebImage img = new WebImage(hizmetResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(hizmetResimURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    //~ ana dizine çık ordan klasörü bul ekle
                    img.Save("~/Uploads/Hizmet/" + logoname);

                    hizmet.hizmetResimURL = "/Uploads/Hizmet/" + logoname;
                }



                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(hizmet);

        }


        //boş olabilir id diye intin yanına soru işareti
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari="Güncellenecek hizmet bulunamadı.";
            }
            var hizmet = db.Hizmet.Find(id);
            if(hizmet == null)
            {
                return HttpNotFound();
            }
           
            return View(hizmet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id,Hizmet hizmet,HttpPostedFileBase hizmetResimURL)
        {
            if (ModelState.IsValid)
            {

                var h = db.Hizmet.Where(x => x.hizmetId == id).SingleOrDefault();

                if (hizmetResimURL != null)
                {
                    //daha önce kaydettiğimiz bir resim var mı varsa sil
                    if (System.IO.File.Exists(Server.MapPath(h.hizmetResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.hizmetResimURL));

                    }
                    //resim ekleme için webImage kütüphanesi
                    WebImage img = new WebImage(hizmetResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(hizmetResimURL.FileName);

                    string hizmetname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    //~ ana dizine çık ordan klasörü bul ekle
                    img.Save("~/Uploads/Hizmet/" + hizmetname);

                    h.hizmetResimURL = "/Uploads/Hizmet/" + hizmetname;
                }

                h.hizmetBaslik = hizmet.hizmetBaslik;
                h.hizmetAciklama = hizmet.hizmetAciklama;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();

        }

     

        // POST: Iletisim/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();

            }
            var h = db.Hizmet.Find(id);
            if(h == null)
            {
                return HttpNotFound();

            }


            db.Hizmet.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}