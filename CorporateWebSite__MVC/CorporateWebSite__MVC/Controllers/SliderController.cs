using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CorporateWebSite__MVC.Models.DataContext;
using CorporateWebSite__MVC.Models.Model;

namespace CorporateWebSite__MVC.Controllers
{
    public class SliderController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();

        // GET: Slider
        [Route("yonetimpaneli/andropia/slider")]

        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        // GET: Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "sliderId,sliderBaslik,sliderGozAciklama,sliderBAciklama,logoURL")] Slider slider,HttpPostedFileBase logoURL)
        {
            if (ModelState.IsValid)
            {

                if (logoURL != null)
                {

                    //resim ekleme için webImage kütüphanesi
                    WebImage img = new WebImage(logoURL.InputStream);
                    FileInfo imginfo = new FileInfo(logoURL.FileName);

                    string slidername = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(1024, 360);
                    //~ ana dizine çık ordan klasörü bul ekle
                    img.Save("~/Uploads/Slider/" + slidername);

                    slider.logoURL = "/Uploads/Slider/" + slidername;
                }






                db.Slider.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "sliderId,sliderBaslik,sliderGozAciklama,sliderBAciklama,logoURL")] Slider slider, HttpPostedFileBase logoURL,int id)
        {
            if (ModelState.IsValid)
            {
                var s = db.Slider.Where(x => x.sliderId == id).SingleOrDefault();

                if (logoURL != null)
                {
                    //daha önce kaydettiğimiz bir resim var mı varsa sil
                    if (System.IO.File.Exists(Server.MapPath(s.logoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.logoURL));

                    }
                    //resim ekleme için webImage kütüphanesi
                    WebImage img = new WebImage(logoURL.InputStream);
                    FileInfo imginfo = new FileInfo(logoURL.FileName);

                    string sliderimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(1024, 360);
                    //~ ana dizine çık ordan klasörü bul ekle
                    img.Save("~/Uploads/Slider/" + sliderimgname); 
                    s.logoURL = "/Uploads/Slider/" + sliderimgname;
                }




                s.sliderBaslik = slider.sliderBaslik;
                s.sliderBAciklama = slider.sliderBAciklama;
                s.sliderGozAciklama = slider.sliderGozAciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }

         

            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);


            if (System.IO.File.Exists(Server.MapPath(slider.logoURL)))

            {
                System.IO.File.Delete(Server.MapPath(slider.logoURL));

            }
            db.Slider.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
