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
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int sliderId { get; set; }

        [DisplayName("Slider Başlık")]
        [Required, StringLength(30, ErrorMessage = "30 karakter olmalıdır!")]
        public string sliderBaslik { get; set; }

 

        [DisplayName("Slider Kısa Açıklama")]
        [Required, StringLength(150, ErrorMessage = "150 karakter olmalıdır!")]
        public string sliderGozAciklama { get; set; }

        [DisplayName("Slider Açıklama")]
        public string sliderBAciklama { get; set; }

        [DisplayName("Slider Resim")]
        public string logoURL { get; set; }

       
    }

}