using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Dien_Thoai.Models
{
    [Table("HoaDons")]
    public class HoaDon
    {
        [Key]
        public int ID { get; set; }
        public string MaHoaDon { get; set; }
        public string Id_GioHang { get; set; }
        public string MaSP { get; set; }
        public string MaKH { get; set; }
        public int SoLuongXuat { get; set; }
        public string TrangThaiXuat { get; set; }
        public DateTime NgayHD { get; set; }
        public double ThanhTien { get; set; }
    }
}