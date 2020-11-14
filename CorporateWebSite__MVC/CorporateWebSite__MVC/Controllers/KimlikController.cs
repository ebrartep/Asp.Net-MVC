using CorporateWebSite__MVC.Models.DataContext;
using CorporateWebSite__MVC.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CorporateWebSite__MVC.Controllers
{
    public class KimlikController : Controller
    {
        KurumsalDBContext db = new KurumsalDBContext();
        // GET: Kimlik
        [Route("yonetimpaneli/andropia/kimlik")]

        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }

        // GET: Kimlik/Details/5
       
       

        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            //entity framework e uygun 
            //id ile kimlikId i karşılaştırıyoruz
            var kimlik = db.Kimlik.Where(x => x.kimlikId == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //ckeditor kayıt işlemi için bunu ekledik ne anlama geldiğini araştır
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik,HttpPostedFileBase logoURL)
        {
            //model doğrulandıysa
            if (ModelState.IsValid)
            {
                //kimlik bilgilerini doğruluyoruz.
                //x öyleki x.kimlikId eşittir dışarıdan gelen id bizlere kimlik bilgisi bu olan değeri getir 
                var kmlik = db.Kimlik.Where(x => x.kimlikId == id).SingleOrDefault();
                //logourl'in name ine bakıyoruz
                //boş değilse
                if(logoURL != null)
                {
                    //daha önce kaydettiğimiz bir resim var mı varsa sil
                    if (System.IO.File.Exists(Server.MapPath(kmlik.logoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(kmlik.logoURL));

                    }
                    //resim ekleme için webImage kütüphanesi
                    WebImage img = new WebImage(logoURL.InputStream);
                    FileInfo imginfo = new FileInfo(logoURL.FileName);

                    string logoname = logoURL.FileName + imginfo.Extension;
                    img.Resize(300, 200);
                    //~ ana dizine çık ordan klasörü bul ekle
                    img.Save("~/Uploads/Kimlik/"+logoname);
                    
                    kmlik.logoURL = "/Uploads/Kimlik/" + logoname;
                 }

                kmlik.title = kimlik.title;
                kmlik.keywords = kimlik.keywords;
                kmlik.description = kimlik.description;
                kmlik.unvan = kimlik.unvan;
                db.SaveChanges();
                //ındexe gitmesi için
                return RedirectToAction("Index");


            }
            return View(kimlik);
        }

       
            
    }
}
