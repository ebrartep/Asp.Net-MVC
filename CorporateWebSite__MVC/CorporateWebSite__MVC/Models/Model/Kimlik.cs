using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.Model
{
    //public olması önemli
    [Table("Kimlik")]
    public class Kimlik
    {
        [Key]      
        public int kimlikId { get; set; }

        [DisplayName("Site Başlık")]
        [Required,StringLength(50,ErrorMessage ="100 karakter olmalıdır!")]
        public string title { get; set; }

        [DisplayName("Anahtar Kelimeler")]
        [Required, StringLength(300, ErrorMessage = "300 karakter olmalıdır!")]
        public string keywords { get; set; }

        [DisplayName("Site Açıklama")]
        [Required, StringLength(300, ErrorMessage = "300 karakter olmalıdır!")]
        public string description { get; set; }

        [DisplayName("Site Logo")]
        public string logoURL { get; set; }

        [DisplayName("Site Ünvan")]
        public string unvan { get; set; }
    }

}