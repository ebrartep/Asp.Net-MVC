using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.Model
{
    [Table("Iletisim")]
    public class Iletisim
    {
        [Key]
        
        public int iletisimId { get; set; }

        [StringLength(250,ErrorMessage ="250 karakter olmalıdır!")]
        public string adres { get; set; }
        [StringLength(250, ErrorMessage = "250 karakter olmalıdır!")]

        public string telefon { get; set; }
        [StringLength(250, ErrorMessage = "250 karakter olmalıdır!")]

        public string fax { get; set; }
        [StringLength(250, ErrorMessage = "250 karakter olmalıdır!")]

        public string whatsapp { get; set; }
        [StringLength(250, ErrorMessage = "250 karakter olmalıdır!")]

        public string facebook { get; set; }
        [StringLength(250, ErrorMessage = "250 karakter olmalıdır!")]

        public string twitter { get; set; }
        [StringLength(250, ErrorMessage = "250 karakter olmalıdır!")]

        public string instagram { get; set; }



    }
}