using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.Model
{
    [Table("Hakkimizda")]
    public class Hakkimizda
    {
        [Key]
        public int hakkimizdaId { get; set; }

        //Bir karakter kısıtlaması getirmedik
        [Required]
        //Açıklama alanı böyle gözüksün diye 
        [DisplayName("Hakkımızda Açıklama")]
        public string hakkimizdaAciklama { get; set; }

    }
}