using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Dien_Thoai.Models
{
    [Table("LoaiSanPhams")]
    public class LoaiSanPham
    {
        [Key]
        public string LoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
    }
}