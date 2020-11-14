using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.Model
{
    [Table("Hizmet")]
    public class Hizmet
    {
        [Key]
        public int hizmetId { get; set; }

        [Required,StringLength(150,ErrorMessage ="150 karakter olabilir!")]
        [DisplayName("Hizmet Başlık")]

        public string hizmetBaslik { get; set; }

        [DisplayName("Hizmet Açıklama")]

        public string hizmetAciklama { get; set; }

        [DisplayName("Hizmet Resim")]

        public string hizmetResimURL{ get; set; }

    }
}