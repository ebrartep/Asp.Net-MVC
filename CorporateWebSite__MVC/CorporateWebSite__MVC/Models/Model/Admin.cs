using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.Model
{
    [Table("Admin")]
    //bunu yazmasak bile veritabanından class ismi yani Admin olarak gözükür
    public class Admin
    {
        [Key]
        public int adminId { get; set; }

        //required zorunlu
        [Required,StringLength(50,ErrorMessage ="50 karakter olmalıdır!")]
        public string eposta { get; set; }
        
        [Required, StringLength(50, ErrorMessage = "50 karakter olmalıdır!")]
        public string sifre { get; set; }
        public string yetki { get; set; }
    }
}