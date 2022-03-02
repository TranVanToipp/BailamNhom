using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_Dien_Thoai.Models
{
    public class Project_Dien_ThoaiDBContexxt : DbContext
    {
        public Project_Dien_ThoaiDBContexxt() : base("name=ChuoiKN") { }
        public DbSet<GioHang>GioHangs{get; set;}
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<MaXacNhan> MaXacNhans { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
    }
}