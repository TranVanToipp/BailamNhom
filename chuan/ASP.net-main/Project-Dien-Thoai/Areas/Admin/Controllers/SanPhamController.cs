using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;

namespace Project_Dien_Thoai.Areas.Admin.Controllers
{
    public class SanPhamController : BaseController
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();

        // GET: Admin/SanPham
        public ActionResult Index()
        {
            return View(db.SanPhams.ToList());
        }
        public bool Trash(string rd)
        {
            var trash = from k in db.SanPhams where k.MaSP==rd select k;
            if (trash.Any())
            {
                return true;
            }
            else return false;
        }
        public string Randum()
        {
            Random rd = new Random();
            string R;
            string RD;
            do
            {
                R = rd.Next(1, 9999999).ToString();
                RD = "SP_" + R;
            } while (Trash(RD) == true);
            return RD;
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenSP,SoLuongSP,DonViTinh,GiaDauVao,GiaDauRa,ThongTinSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageUpload"];
                sanPham.MaSP = Randum().ToString();
                sanPham.HinhSP = file.FileName;
                file.SaveAs(Path.Combine(Server.MapPath("~/Image"), file.FileName));
                sanPham.LoaiSP = Request.Form["loaisp"];
                sanPham.MaNCC = Request.Form["nhacc"];
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");    
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuongSP,DonViTinh,MaNCC,GiaDauVao,GiaDauRa,ThongTinSP,LoaiSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageUpload"];
                sanPham.HinhSP = file.FileName;
                file.SaveAs(Path.Combine(Server.MapPath("~/Image"), file.FileName));
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
