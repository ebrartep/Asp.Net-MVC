﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CorporateWebSite__MVC.Models.DataContext;
using CorporateWebSite__MVC.Models.Model;

namespace CorporateWebSite__MVC.Controllers
{
    public class IletisimController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();

        // GET: Iletisim
        [Route("yonetimpaneli/andropia/iletisim")]

        public ActionResult Index()
        {
            return View(db.Iletisim.ToList());
        }

        // GET: Iletisim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // GET: Iletisim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Iletisim/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iletisimId,adres,telefon,fax,whatsapp,facebook,twitter,instagram")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                db.Iletisim.Add(iletisim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iletisim);
        }

        // GET: Iletisim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // POST: Iletisim/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iletisimId,adres,telefon,fax,whatsapp,facebook,twitter,instagram")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                //değisen alanları güncelle değişmeyenlere dokunma demek
                db.Entry(iletisim).State = EntityState.Modified;
                db.SaveChanges();
                //index metoduna git    
                return RedirectToAction("Index");
            }
            return View(iletisim);
        }

        // GET: Iletisim/Delete/5
        public ActionResult Delete(int? id)
        {
            //bu kısımda id si eşit olanı bulduuruyor hepsinde böyle sonra action işlemine başlıyor
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // POST: Iletisim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //find ile bulduruyor
            Iletisim iletisim = db.Iletisim.Find(id);
            db.Iletisim.Remove(iletisim);
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
