using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;

using PagedList;
using PagedList.Mvc;
using System.IO;

namespace MvcBookStore.Controllers
{
    public class AdminController : Controller
    {
        dbQLBanSachDataContext db = new dbQLBanSachDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sach(int ? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;

            return View(db.SACHes.ToList().OrderBy(n=>n.Masach).ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult Themmoisach()
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
           
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisach(SACH sach,HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "vui long chon anh";
                return View();
            }

            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "hinh anh da ton tai";
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sach.Hinhminhhoa = fileName;
                    db.SACHes.InsertOnSubmit(sach);
                    db.SubmitChanges();
                }
                return RedirectToAction("Sach");
            }
     }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Chitietsach(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }


        [HttpGet]
        public ActionResult Xoasach(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View(sach);
        }

        [HttpPost,ActionName("Xoasach")]
        public ActionResult XacnhanXoa(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SACHes.DeleteOnSubmit(sach);
            db.SubmitChanges();
            return RedirectToAction("Sach");
  
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasach(SACH sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            //if (fileupload == null)
            //{
            //    ViewBag.Thongbao = "vui long chon anh";
            //    return View();
            //}

            //else
           // {
                if (ModelState.IsValid)
                {
                    //var fileName = Path.GetFileName(fileupload.FileName);
                    //var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    //if (System.IO.File.Exists(path))
                    //    ViewBag.Thongbao = "hinh anh da ton tai";
                    //else
                    //{
                    //    fileupload.SaveAs(path);
                    //}
                    //sach.Hinhminhhoa = fileName;
                    var s = db.SACHes.SingleOrDefault( n => n.Masach == sach.Masach);
                    s.Tensach = sach.Tensach;
                    s.Dongia = sach.Dongia;
                    s.Donvitinh = sach.Donvitinh;
                    s.Hinhminhhoa = sach.Hinhminhhoa;
                    s.CHUDE = sach.CHUDE;
                    s.NHAXUATBAN = sach.NHAXUATBAN;
                    s.Ngaycapnhat = sach.Ngaycapnhat;
                    s.Mota = sach.Mota;
                    s.Soluongban = sach.Soluongban;
                    s.solanxem = sach.solanxem;
                
                    db.SACHes.InsertOnSubmit(sach);
                    db.SubmitChanges();


                }
                return RedirectToAction("Sach");
            
        }
        [HttpGet]
        public ActionResult Suasach(int id)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "phai nhap ten dang nhap";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "phai nhap mat khau";
            }
            else
            {
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "ten dang nhap hoac mat khau khong dung";
            }
            return View();
        }
    
     
    }
}