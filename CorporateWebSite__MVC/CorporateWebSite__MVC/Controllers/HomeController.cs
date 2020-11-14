using CorporateWebSite__MVC.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using CorporateWebSite__MVC.Models.Model;

namespace CorporateWebSite__MVC.Controllers
{
    public class HomeController : Controller
    {

        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Home
        [Route("")]

        [Route("Anasayfa")]

        public ActionResult Index()

        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.hizmetId);
            
            return View();
        }


        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x => x.sliderId));
        }

        public ActionResult HizmetPartial()
        {
            //orderby tersten sıralar
            return View(db.Hizmet.ToList().OrderByDescending(x => x.hizmetId));
        }

        [Route("Hakkimizda")]

        public ActionResult Hakkimizda()
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            //orderby tersten sıralar
            return View(db.Hakkimizda.SingleOrDefault());
        }

        [Route("Hizmetlerimiz")]

        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Hizmet.ToList().OrderByDescending(x=>x.hizmetId));
        }

        [Route("Iletisim")]
public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Iletisim.SingleOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(string adsoyad=null,string email=null,string konu=null,string mesaj=null)
        {
            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "kurumsalweba@gmail.com";
                WebMail.Password = "titanik555";
                WebMail.SmtpPort = 587;
                WebMail.Send("kurumsalweb01@gmail.com", konu, email + "-" + mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
                Response.Redirect("/Iletisim");

            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();
        }

        [Route("BlogPost")]

        public ActionResult Blog(int sayfa=1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.blogId).ToPagedList(sayfa,5));
        }


        [Route("BlogKategori/{kategoriAd}-{id:int}")]

        public ActionResult KategoriBlog( int id,int sayfa=1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            var b = db.Blog.Include("Kategori").OrderByDescending(x=>x.blogId).Where(x => x.kategori.kategoriId == id).ToPagedList(sayfa,5);
            return View(b);
        }

        public ActionResult BlogKategoriPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x=>x.kategoriAd));
        }

        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderByDescending(x => x.blogId));
        }



        [Route("BlogPost/{blogBaslik}-{id:int}")]

        public ActionResult BlogDetay(int id)
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            var b = db.Blog.Include("Kategori").Include("Yorums").Where(x => x.blogId == id).SingleOrDefault();
            return View(b);
        }

        public JsonResult YorumYap(string yorumAdSoyad,string yorumEposta,string yorumIcerik,int blogid)
        {
            if(yorumIcerik == null) {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum
            {
                yorumAdSoyad = yorumAdSoyad,
                yorumEposta = yorumEposta,
                yorumIcerik = yorumIcerik,
                blogId = blogid,
                yorumOnay = false

            }) ;
            
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.hizmetId);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.blogId);
            return PartialView();
        }   
     
    }
}