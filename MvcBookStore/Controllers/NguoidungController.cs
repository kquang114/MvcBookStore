using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;

namespace MvcBookStore.Controllers
{
    public class NguoidungController : Controller
    {
        dbQLBanSachDataContext db = new dbQLBanSachDataContext();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["DiachiKH"];
            var email = collection["Email"];
            var dienthoai = collection["DienthoaiKH"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Ho ten khach hang khong duoc de trong";
            }

            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phai nhap ten dang nhap";
            }

            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phai nhap mat khau";
            }

            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "phai nhap lai mat khau";
            }

            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email khong duoc bo trong";
            }

            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phai nhap dien thoai";
            }

            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi7"] = "Phai nhap dia chi";
            }
            else
            {
                kh.HoTenKH = hoten;
                kh.TenDN = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }

        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "PHẢI NHẬP TÊN ĐĂNG NHẬP";
            }

            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "PHẢI NHẬP MẬT KHẨU";
            }

            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TenDN == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    //ViewBag.Thongbao = "CHÚC MỪNG ĐÃ ĐĂNG NHẬP THÀNH CÔNG";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Giohang", "Giohang");
                }

                else
                    ViewBag.Thongbao = "TÊN ĐĂNG NHẬP HOẶC MẬT KHẨU KHÔNG ĐÚNG";
            }
            return View();
        }

    }
}