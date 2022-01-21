﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace DoAnDT.Models
{
    public class DonhangKHModel
    {
        public DonHangKH donHang;
        public AspNetUser nguoiMua;
        public String nguoiNhan;
        public String tinhTrangDH;
        public List<DonhangKHModel> Xemdonhang(string makh)
        {
            using (DBDTConnect db = new DBDTConnect())
            {
                List<DonhangKHModel> listDh = new List<DonhangKHModel>();
                db.DonHangKHs.AsNoTracking();
                var danhsach = from p in db.DonHangKHs where p.MaKH == makh select p;
                foreach (var temp in danhsach.ToList())
                {
                    AspNetUser users = (from p in db.AspNetUsers where p.Id == makh select p).FirstOrDefault();

                    listDh.Add(new DonhangKHModel()
                    {
                        donHang = temp,
                        nguoiMua = users,
                        tinhTrangDH = gettinhTrangDH(temp.TinhTrangDH)
                    });
                }
                return listDh;
            }
        }

        private string gettinhTrangDH(int? nullable)
        {
            switch (nullable)
            {
                case 0:
                    {
                        return "Chưa giao";
                    }
                case 1:
                    {
                        return "Đang duyệt";
                    }
                case 2:
                    {
                        return "Đang giao hàng";
                    }
                case 3:
                    {
                        return "Đã giao";
                    }
                case 4:
                    {
                        return "Đã hủy";
                    }
            }
            return "Đang duyệt";
        }
        public bool HuyDH(string maDH)
        {
            try
            {
                using (DBDTConnect db = new DBDTConnect())
                {
                    string query = "update DonHangKH set TinhTrangDH = '4' where MaDH ='" + maDH + "'";
                    db.Database.ExecuteSqlCommand(query);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public int TotalSub()
        {
            DBDTConnect db = new DBDTConnect();
            List<AspNetUser> lst = db.AspNetUsers.ToList();
            return lst.Count;
        }
        public int TotalCmt()
        {
            DBDTConnect db = new DBDTConnect();
            List<BinhLuan> lst = db.BinhLuans.ToList();
            return lst.Count;
        }
        public int TotalOrder()
        {
            DBDTConnect db = new DBDTConnect();
            List<DonHangKH> lst = db.DonHangKHs.Where(m => m.TinhTrangDH == 3).ToList();
            return lst.Count;
        }
        public Double TotalSales()
        {
            DBDTConnect db = new DBDTConnect();
            Double total = 0;
            List<DonHangKH> lst = db.DonHangKHs.Where(m => m.TinhTrangDH == 3).ToList();
            foreach(var item in lst)
            {
                total += Double.Parse(item.Tongtien.ToString());
            }
            return total;
        }
        public AspNetUser Xemttnguoidung(string id)
        {
            using (DBDTConnect db = new DBDTConnect())
            {
                AspNetUser users = (from p in db.AspNetUsers where p.Id == id select p).FirstOrDefault();
                return users;
            }
        }
        public void Luudonhang(Donhangtongquan a, string maKH, Giohang giohang)
        {
            try
            {
                using (DBDTConnect db = new DBDTConnect())
                {
                    DonHangKH dhkh = new DonHangKH();
                    dhkh.MaDH = RandomMa();
                    dhkh.MaKH = maKH;

                    dhkh.Diachi = a.address;
                    dhkh.Dienthoai = a.phoneNumber;
                    dhkh.Ghichu = a.Note;
                    dhkh.NgayDatMua = DateTime.Now;
                    dhkh.TinhTrangDH = 1;
                    dhkh.Tongtien = giohang.TinhtongtienCart();
                    dhkh.PhiVanChuyen = 0;

                    dhkh = db.DonHangKHs.Add(dhkh);
                    db.SaveChanges();

                    Luuchitietdonhang(giohang, db, dhkh.MaDH);
                }
            }
            catch (Exception e) { }
        }

        private void Luuchitietdonhang(Giohang giohang, DBDTConnect db, string maDH)
        {
            foreach (var temp in giohang.getGiohang())
            {
                ChiTietDonHang chiTiet = new ChiTietDonHang()
                {
                    MaDH = maDH,
                    MaSP = temp.sanPham.MaSP,
                    SoLuong = temp.Soluong,
                    ThanhTien = (decimal)temp.Thanhtien
                };
                db.ChiTietDonHangs.Add(chiTiet);

            }
            db.SaveChanges();
        }
        public string RandomMa()
        {
            string maID;
            Random rand = new Random();
            do
            {
                maID = "";
                for (int i = 0; i < 5; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID));
            return maID;
        }

        private bool KiemtraID(string maID)
        {
            using (DBDTConnect db = new DBDTConnect())
            {
                var temp = db.DonHangKHs.Find(maID);
                if (temp == null)
                    return true;
                return false;
            }
        }

        internal IQueryable<DonHangKH> TimDonHang(string key, string mobile, DateTime? date, int? status)
        {
            DBDTConnect db = new DBDTConnect();

            IQueryable<DonHangKH> lst = db.DonHangKHs;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.MaDH.Contains(key));
            if (!string.IsNullOrEmpty(mobile))
                lst = lst.Where(m => m.Dienthoai.Contains(mobile));
            if (status != null)
                lst = lst.Where(m => m.TinhTrangDH == status);
            if (date != null)
                lst = lst.Where(m => m.NgayDatMua.Value.Year == date.Value.Year && m.NgayDatMua.Value.Month == date.Value.Month && m.NgayDatMua.Value.Day == date.Value.Day);
            return lst;

        }

        internal bool UpdateTinhTrang(string madh, int? tt)
        {
            if (tt == null) return false;
            try
            {
                DBDTConnect db = new DBDTConnect();
                DonHangKH dh = db.DonHangKHs.Find(madh);
                if (dh.TinhTrangDH == 4 || dh.TinhTrangDH == 3)
                    return false;
                if (dh.TinhTrangDH == 1)
                    if (tt == 2 || tt == 3)
                    {
                        foreach (var item in dh.ChiTietDonHangs)
                        {
                            SanPhamModel spm = new SanPhamModel();
                            spm.UpdateSL(item.MaSP, item.SoLuong, false);
                        }
                    }
                if (dh.TinhTrangDH == 2)
                {
                    if (tt == 4)
                    {
                        foreach (var item in dh.ChiTietDonHangs)
                        {
                            SanPhamModel spm = new SanPhamModel();
                            spm.UpdateSL(item.MaSP, item.SoLuong, true);
                        }
                    }
                    if (tt == 1) return false;
                }
                string query = "update DonHangKH set TinhTrangDH = " + tt + " where MaDH ='" + madh + "'";
                db.Database.ExecuteSqlCommand(query);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal IQueryable<ChiTietDonHang> ChiTietDonHang(string maDH)
        {
            DBDTConnect db = new DBDTConnect();
            return db.ChiTietDonHangs.Where(m => m.MaDH.Contains(maDH));
        }

        internal IQueryable<object> ThongKeDoanhThu(DateTime? froms, DateTime? tos)
        {           
            DBDTConnect db = new DBDTConnect();
            var s = from p in db.DonHangKHs
                    where p.TinhTrangDH == 3 && p.NgayDatMua >= froms && p.NgayDatMua <= tos
                    group p by EntityFunctions.TruncateTime(p.NgayDatMua) into gro
                    select new { ngaymua = gro.Key.Value, tongtien = gro.Sum(r => r.Tongtien) };
            return s;
        }


        internal IQueryable<object> ThongKeTiTrong(DateTime? froms, DateTime? tos)
        {
            DBDTConnect db = new DBDTConnect();
            var s = from p in db.ChiTietDonHangs
                    where p.DonHangKH.TinhTrangDH == 3 && p.DonHangKH.NgayDatMua >= froms && p.DonHangKH.NgayDatMua <= tos
                    group p by p.SanPham.TenSP into gro
                    select new { TenSP = gro.Key, SL = gro.Sum(r => r.SoLuong) };
            return s;
        }

    }
}