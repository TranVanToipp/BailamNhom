using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Dien_Thoai.Models
{
    [Table("SanPhams")]
    public class SanPham
    {
        [Key]
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuongSP { get; set; }
        public string DonViTinh { get; set; }
        public string MaNCC { get; set; }
        public double GiaDauVao { get; set; }
        public double GiaDauRa { get; set; }
        public string ThongTinSP { get; set; }
        public string HinhSP { get; set; }
        public string LoaiSP { get; set; }
    }
}