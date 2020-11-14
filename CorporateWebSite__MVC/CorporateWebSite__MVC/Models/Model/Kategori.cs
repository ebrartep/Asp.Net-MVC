using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.Model
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key]
        public int kategoriId { get; set; }

        [Required,StringLength(50,ErrorMessage ="50 karakter olmalıdır!")]
        public string kategoriAd { get; set; }


        public string kategoriAciklama { get; set; }

        public ICollection<Blog> Blogs { get; set; }

    }
}