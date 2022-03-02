using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Dien_Thoai.Models
{
    [Table("MaXacNhans")]
    public class MaXacNhan
    {
        [Key]
        public int Id { get; set; }
        public string TenDN { get; set; }
        public string Maxacnhan { get; set; }
    }
}