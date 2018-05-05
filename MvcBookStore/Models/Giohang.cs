using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBookStore.Models;

namespace MvcBookStore.Models
{
    public class Giohang
    {
        dbQLBanSachDataContext data = new dbQLBanSachDataContext();
        public int iMasach { set; get; }
        public string sTensach { set; get; }
        public string sAnhbia { set; get; }
        public Double dDonggia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhTien
        {
            get { return iSoluong * dDonggia; }
        }

        public Giohang(int Masach)
        {
            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Hinhminhhoa;
            dDonggia = double.Parse(sach.Dongia.ToString());
            iSoluong = 1;
        }
    }
}