using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Dien_Thoai.Models
{
    [Table("GioHangs")]
    public class GioHang
    {
        [Key]
        public int Id_GioHang { get; set; }
        public string MaKH { get; set; }
        public string MaSP { get; set; }
        public int SoLuongHang { get; set; }
    }
}