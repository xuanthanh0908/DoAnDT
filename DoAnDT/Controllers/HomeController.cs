using DoAnDT.Models;
using DoAnDT.Models.NganLuongAPI;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DoAnDT.Controllers
{
    public class HomeController : Controller
    {
        public static List<Thanhviennhom> Ds_Group;
        private string MerchantId = "46838";
        private string MerchantPassword = "b2d2112d7533f970d70c2e13842e215a";
        private string MerchantEmail = "ngoctoan89@gmail.com";
        private string CurrentLink = WebConfigurationManager.AppSettings["CurrentLink"];
        DonhangKHModel dhmodel = new DonhangKHModel();
        public ActionResult Index()
        {
            ManagerObiect.getIntance();
            return View();
        }
        public ActionResult Thongtinnhom()
        {
            if (Ds_Group == null)
            {
                Ds_Group = new List<Thanhviennhom>();
                Ds_Group.Add(new Thanhviennhom
                {
                    MSSV = "19810310151",
                    Hoten = "NGUYỄN XUÂN THÀNH",
                    LinkFacebook = "https://www.facebook.com/xuanthanh962742"
                });
                Ds_Group.Add(new Thanhviennhom
                {
                    MSSV = "19810310170",
                    Hoten = "NGUYỄN THỊ THANH VÂN",
                    LinkFacebook = "https://www.facebook.com/vanntt02.55"
                });
            }
            return View(Ds_Group);
        }
        public ActionResult Cart()
        {
            return View(ManagerObiect.getIntance().giohang);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        //Đơn hàng
        public ActionResult Xemdonhang(string maKH)
        {
            List<DonhangKHModel> temp = new List<DonhangKHModel>();
            if (maKH.Length != 0)
            {
                DonhangKHModel dh = new DonhangKHModel();
                temp = dh.Xemdonhang(maKH);
            }
            return View(temp);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        public ActionResult Huydonhang(string maDH)
        {
            DonhangKHModel dh = new DonhangKHModel();
            dh.HuyDH(maDH);
            var donhang = dh.Xemdonhang(User.Identity.GetUserId());
            return View(donhang);
        }
        public ActionResult Checkout()
        {
            if (Request.IsAuthenticated)
            {
                DonhangKHModel dh = new DonhangKHModel();
                dh.nguoiMua = dh.Xemttnguoidung(User.Identity.GetUserId());
                Donhangtongquan dhtq = new Donhangtongquan()
                {
                    buyer = dh.nguoiMua.HoTen,
                    seller = dh.nguoiMua.HoTen,
                    phoneNumber = dh.nguoiMua.PhoneNumber,
                    address = dh.nguoiMua.DiaChi
                };
                return View(dhtq);
            }
            else
            {
                return RedirectToAction("Authentication", "Account", new { returnUrl = "/Home/Checkout" });
            }
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        [HttpPost]
        public ActionResult Checkout(Donhangtongquan dh)
        {
            if (User.Identity.IsAuthenticated)
            {
                var giohang = ManagerObiect.getIntance().giohang;
                string ma = dhmodel.RandomMa();
                if (dh.PaymentStatus == "CASH")
                {
                    dhmodel.Luudonhang(dh, User.Identity.GetUserId(), giohang, ma);
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    RequestInfo info = new RequestInfo();
                    info.Merchant_id = MerchantId;
                    info.Merchant_password = MerchantPassword;
                    info.Receiver_email = MerchantEmail;

                    info.cur_code = "vnd";
                    info.bank_code = dh.bankcode;

                    info.Order_code = ma;
                    info.Total_amount = giohang.TinhtongtienCart().ToString();
                    info.fee_shipping = "0";
                    info.Discount_amount = "0";
                    info.order_description = "Thanh toán đơn hàng tại Thành Vân Shop";
                    info.return_url = CurrentLink + "/Home/ConfirmOrder/"+ma;
                    info.cancel_url = CurrentLink + "/Home/CancelOrder/"+ma;

                    info.Buyer_fullname = dh.buyer;
                    info.Buyer_email = "vovanvainoi@gmail.com";
                    info.Buyer_mobile = dh.seller;

                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    ResponseInfo result = objNLChecout.GetUrlCheckout(info, dh.PaymentStatus);
                    if (result.Error_code == "00")
                    {
                        dhmodel.Luudonhang(dh, User.Identity.GetUserId(), giohang, ma);
                        return Redirect(result.Checkout_url);
                    }
                    else  return Content("<script language='javascript' type='text/javascript'>alert('"+ result.Description + "');</script>");
                }
            }
            else
            {
                return RedirectToAction("Checkout", "Home");
            }
        }

        public ActionResult MainMenu()
        {
            MainMenuModel mnmodel = new MainMenuModel();
            var menulist = mnmodel.GetMenuList();
            return PartialView("_MainMenuPartial", menulist);
        }
        public ActionResult SPNoiBat(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPHot();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPMoiNhap(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPMoiNhap();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPKhuyenMai(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPKhuyenMai();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPBanChay()
        {
            SanPhamModel sp = new SanPhamModel();
            IQueryable<SanPham> splist = sp.SPBanChay(7);
            if (splist.Any())
                return PartialView("_BestSellerPartial", splist.ToList());
            else
                return null;
        }
        public ActionResult SPMoiXem()
        {
            return PartialView("_RecentlyViewPartial", ManagerObiect.getIntance().Laydanhsachsanphammoixem());
        }
        public ActionResult ConfirmOrder(int id)
        {
            string token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = MerchantId;
            info.Merchant_password = MerchantPassword;
            info.Token = token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if (result.errorCode == "00")
            {
                using (DBDTConnect db = new DBDTConnect())
                {
                    dhmodel.UpdateTinhTrang(id.ToString(), 5);
                }
                ViewBag.IsSuccess = true;
                ViewBag.Result = "Thanh toán thành công. Chúng tôi sẽ liên hệ lại sớm nhất.";
            }
            else
            {
                ViewBag.IsSuccess = false;
                ViewBag.Result = "Có lỗi xảy ra. Vui lòng liên hệ admin.";
            }
            return View();
        }
        public ActionResult CancelOrder(int id)
        {
            using (DBDTConnect db = new DBDTConnect())
            {
                dhmodel.UpdateTinhTrang(id.ToString(),4);
            }
            return View();
        }
    }
}
