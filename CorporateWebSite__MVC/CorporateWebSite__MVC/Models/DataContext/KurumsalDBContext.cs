﻿using CorporateWebSite__MVC.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.DataContext
{
    //DBcontext ten miras almış
    public class KurumsalDBContext:DbContext
    {
        //ctor->constructor ile geliyor
        //web confıg için base ile name aynı olmalıdır
        //bağlantı sağlamak için önemli!!!!

        public KurumsalDBContext():base("KurumsalDB")
        { }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
        public DbSet<Iletisim> Iletisim{ get; set; }
        public DbSet<Kimlik> Kimlik { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Yorum> Yorum { get; set; }


    }
}