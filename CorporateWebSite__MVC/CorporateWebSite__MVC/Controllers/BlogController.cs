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
    public class BlogController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Blog
        [Route("yonetimpaneli/andropia/blog")]

        public ActionResult Index()
        {
            //kategori adının gelebilmesi için bu işlemi yapıp kategoriyi ınclude ettik
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blog.Include("Kategori").ToList().OrderByDescending(x=>x.blogId));
        }
        public ActionResult Create()
        {
            ViewBag.kategoriId = new SelectList(db.Kategori, "kategoriId","kategoriAd");



            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog,HttpPostedFileBase blogResimURL)
        {
            if (ModelState.IsValid)
            {
                if (blogResimURL != null)
                {

                    //resim ekleme için webImage kütüphanesi
                    WebImage img = new WebImage(blogResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(blogResimURL.FileName);

                    string blogname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(600, 400);
                    //~ ana dizine çık ordan klasörü bul ekle
                    img.Save("~/Uploads/Blog/" + blogname);

                    blog.blogResimURL = "/Uploads/Blog/" + blogname;
                }



                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(blog);

        }

        //boş olabilir id diye intin yanına soru işareti
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var blog = db.Blog.Where(x=>x.blogId==id).SingleOrDefault();
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.kategoriId = new SelectList(db.Kategori, "kategoriId", "kategoriAd",blog.kategoriId);


            return View(blog);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Blog blog, HttpPostedFileBase blogResimURL)
        {
            if (ModelState.IsValid)
            {

                var b = db.Blog.Where(x => x.blogId == id).SingleOrDefault();

                if (blogResimURL != null)
                {
                    //daha önce kaydettiğimiz bir resim var mı varsa sil
                    if (System.IO.File.Exists(Server.MapPath(b.blogResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.blogResimURL));

                    }
                    //resim ekleme için webImage kütüphanesi
                    WebImage img = new WebImage(blogResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(blogResimURL.FileName);

                    string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(600, 400);
                    //~ ana dizine çık ordan klasörü bul ekle
                    img.Save("~/Uploads/Blog/" + blogimgname);

                    b.blogResimURL = "/Uploads/Blog/" + blogimgname;
                }

                b.blogBaslik = blog.blogBaslik;
                b.blogIcerik = blog.blogIcerik;
               b.kategoriId = blog.kategoriId;

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(blog);

        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            var b = db.Blog.Find(id);
            if (b == null)
            {
                return HttpNotFound();

            }
            if (System.IO.File.Exists(Server.MapPath(b.blogResimURL)))

            {
                System.IO.File.Delete(Server.MapPath(b.blogResimURL));

            }

            db.Blog.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }







    }

}