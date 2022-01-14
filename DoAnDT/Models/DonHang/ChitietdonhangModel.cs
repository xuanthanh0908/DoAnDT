﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnDT.Models
{
    public class ChitietdonhangModel
    {
        public string MaDH { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> ThanhTien { get; set; }
        public SanPham SanPham { get; set; }

        public List<ChitietdonhangModel>getChiTietDonHang(string maDH)
        {
            using(DBDTConnect db = new DBDTConnect())
            {
                List<ChitietdonhangModel> danhSachChiTiet = new List<ChitietdonhangModel>();
                var ds = (from p in db.ChiTietDonHangs where p.MaDH == maDH select p).ToList();
                foreach(var temp in ds)
                {
                    
                    ChitietdonhangModel tam = new ChitietdonhangModel()
                    {
                        MaDH = temp.MaDH,
                        SoLuong = temp.SoLuong,
                        ThanhTien = temp.ThanhTien,
                        SanPham = temp.SanPham
                    };
                    string tt = temp.SanPham.GiaTien.ToString();
                    danhSachChiTiet.Add(tam);
                }
                return danhSachChiTiet;
            }
        }
    }
}