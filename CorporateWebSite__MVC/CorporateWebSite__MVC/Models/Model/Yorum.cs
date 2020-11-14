using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorporateWebSite__MVC.Models.Model
{
    [Table("Yorum")]
    public class Yorum
    {
        [Key]
        public int yorumId { get; set; }

        [Required,StringLength(50,ErrorMessage="50 karakter olabilir")]
        public string yorumAdSoyad { get; set; }


        public string yorumEposta { get; set; }

        [DisplayName("Yorumunuz")]
        public string yorumIcerik { get; set; }

        public bool yorumOnay { get; set; }
        public int? blogId { get; set; }
        public Blog blog { get; set; }

    }
}