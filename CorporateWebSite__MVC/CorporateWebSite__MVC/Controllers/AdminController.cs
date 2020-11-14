using CorporateWebSite__MVC.Models;
using CorporateWebSite__MVC.Models.DataContext;
using CorporateWebSite__MVC.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CorporateWebSite__MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //datafirst yöntemi
        // KurumsalDBEntities db = new KurumsalDBEntities();


        //veritabanından çekilen codefirst yöntemi
        //KurumsalDB db = new KurumsalDB();

        //aradaki bağlantıyı sağlayan ne ? 
        //KurumsalDbContext old için onu kullanıyoruz

        KurumsalDBContext db = new KurumsalDBContext();

        [Route("yonetimpaneli/andropia")]

        public ActionResult Index()
        {

            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay = db.Kategori.Count();
            ViewBag.HizmetSay = db.Hizmet.Count();
            ViewBag.YorumSay = db.Yorum.Count();

            ViewBag.YorumOnay = db.Yorum.Where(x => x.yorumOnay == false).Count();
            var sorgu = db.Kategori.ToList();
            return View(sorgu);
        }
        [Route("yonetimpaneli/andropia/giris")]

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x => x.eposta == admin.eposta).SingleOrDefault();

            if (login.eposta != admin.eposta || login.sifre != Crypto.Hash(admin.sifre, "MD5"))
            {
                ViewBag.Uyari = "Kullanıcı adı yada şifre yanlış";
            }


            else if(login.eposta == admin.eposta && login.sifre == Crypto.Hash(admin.sifre, "MD5"))
            {

                Session["adminid"] = login.adminId;
                Session["eposta"] = login.eposta;
                Session["yetki"] = login.yetki;
                return RedirectToAction("Index", "Admin");

            }
            ViewBag.Uyari = "Kullanıcı adı yada şifre yanlış";

            return View(admin);


        }

        public ActionResult LogOut()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string eposta)
        {
            var mail = db.Admin.Where(x => x.eposta == eposta).SingleOrDefault();
            if (mail != null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();

                Admin admin = new Admin();
                mail.sifre = Crypto.Hash(Convert.ToString(yenisifre), "MD5");
                db.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;

                WebMail.UserName = "kurumsalweba@gmail.com";
                WebMail.Password = "titanik555"; 
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Admin Panel Giriş Şifreniz", "Şifreniz :" + yenisifre);
                ViewBag.Uyari = "Şifreniz Başarı ile gönderilmiştir";


            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();

        }
        public ActionResult Adminler()
        {
            return View(db.Admin.ToList());
        }

        public ActionResult AdminCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminCreate(Admin admin,string sifre,string eposta)
        {
            if (ModelState.IsValid)
            {
                admin.sifre = Crypto.Hash(sifre,"MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }

            return View(admin);
        }

        public ActionResult Edit(int id)
        {
            var a = db.Admin.Where(x => x.adminId == id).SingleOrDefault();

            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(int id,Admin admin,string sifre,string eposta)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.adminId == id).SingleOrDefault();
                a.sifre = Crypto.Hash(sifre, "MD5");
                a.eposta = admin.eposta;
                a.yetki = admin.yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }



        [HttpPost]
        // GET: Iletisim/Delete/5
        public ActionResult Delete(int? id)
        {
            //bu kısımda id si eşit olanı bulduuruyor hepsinde böyle sonra action işlemine başlıyor
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Iletisim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //find ile bulduruyor
            Admin admin = db.Admin.Find(id);
            db.Admin.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}