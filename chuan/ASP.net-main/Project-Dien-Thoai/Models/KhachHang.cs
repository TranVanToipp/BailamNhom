using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Dien_Thoai.Models
{
    [Table("KhachHangs")]
    public class KhachHang
    {
        [Key]
        public string MaKH { get; set; }
        
        public string TenDN { get; set; }
        public string TenKH { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChiKH { get; set; }
        public int SODTKH { get; set; }
        public string EmailKH { get; set; }
    }
}