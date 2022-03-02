using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Windows.Input;

namespace Project_Dien_Thoai.Models
{
    [Table("TaiKhoans")]
    public class TaiKhoan
    {
        [Key]
        public string TenDN { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string LoaiTaiKhoan { get; set; }
    }
}