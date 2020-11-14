using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int blogId { get; set; }

        public string blogBaslik { get; set; }


        public string blogIcerik{ get; set; }


        public string blogResimURL { get; set; }

        public int? kategoriId { get; set; }
        public Kategori kategori{ get; set; }

        public ICollection<Yorum> yorums { get; set; }
    }
}