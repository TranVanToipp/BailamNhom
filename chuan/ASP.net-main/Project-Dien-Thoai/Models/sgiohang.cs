using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Dien_Thoai.Models
{
    public class sgiohang
    {
        public int id { get; set; }
        public string makh { get; set; }
        public string masp { get; set; }
        public string tensp { get; set; }
        public string hinhsp { get; set; }
        public int soluong { get; set; }
        public double dongia { get; set; }
        public double thanhtien
        {
            get { return soluong * dongia; }
        }
    }
}