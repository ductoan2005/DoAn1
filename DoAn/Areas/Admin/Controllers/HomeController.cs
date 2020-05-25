using DoAn.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        
        private CSDLContext context = new CSDLContext();
        // GET: Admin/Home
        public ActionResult Index(int? id)
        {
            int k = 0;
            if (id != null)
            {
                k = id.GetValueOrDefault() * 5;
            }
            List<Laptop> lt = context.Laptop.OrderBy(s => s.LaptopID).Skip(k).Take(5).ToList();
            ViewBag.ds = lt;
            ViewBag.count = context.Laptop.Count() / 5;

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult RequestLogin(string username, string password)
        {
            bool Key = false;
            int Count;
            Count = context.AdminAccount.Where(s => s.Username == username && s.Password == password).Count();
            if (Count >0) Key = true;
            else Key = false;
            if (Key == true)
            {
                ViewBag.Key = "Welcome Admin";
                ViewBag.Key1 = Key;
            }
            else
            {
                ViewBag.Key1 = Key;
                ViewBag.Key = "Nhập lại tài khoản, mật khẩu";
            }             
            return View();
        }
        public ActionResult Add(Laptop laptop)
        {
            ViewBag.MaTH = new SelectList(context.ThuongHieu, "MaTH", "TenThuongHieu",laptop.MaTH);
            return View(laptop);
        }
        [HttpPost]
        public ActionResult AddResult(string ten, int giatien, string thongso, int MaTH, HttpPostedFileBase hinhanh)
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            if(hinhanh.ContentLength>0)
            {
                string _filename = Path.GetFileName(hinhanh.FileName);
                string _path = Path.Combine(Server.MapPath("~/Areas/Content/img/"), _filename);

                hinhanh.SaveAs(_path);

                    
            }
            int tmp = ch.themsp(ten, giatien, thongso, MaTH, hinhanh.FileName);
            if (tmp != 0) ViewBag.Message = "Success";
            else ViewBag.Message = "Failed";
            
            return View();
        }
        //Tìm
        //public ActionResult TimTheoTen(string tensp)
        //{
        //    CuaHangBanLaptop ch = new CuaHangBanLaptop();
        //    List<Laptop> ds = ch.timtheoten(tensp);
        //    ViewBag.ds = ds; 
        //    return View();
        //}
        public ActionResult TimGanDung(string tensp)
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            List<Laptop> ds = ch.timgandung(tensp);
            ViewBag.ds = ds;
            return View();
        }
        //Admin Profile
        public ActionResult AdminProfile()
        {
            AdminDetail admin = new AdminDetail();
            List<AdminAccount> detail = admin.DSAdmin();
            ViewBag.detail = detail;
            return View();
        }
        public ActionResult Xoa(int id)
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            int tmp = ch.xoa(id);
            if (tmp != 0) ViewBag.Message = "Success";
            else ViewBag.Message = "Failed";
            return View();
        }
        public ActionResult Sua(int id)
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            Laptop lt = ch.tim(id);
            ViewBag.lt = lt;
            return View();
        }
        public ActionResult ResultSua(int id, string tensp, int giatien, string thongso, int MaTH)
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            int tmp = ch.sua(id, tensp, giatien, thongso,MaTH);
            if (tmp != 0) ViewBag.Message = "Success";
            else ViewBag.Message = "Failed";
            return View();
        }
        public ActionResult SXTen()
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            List<Laptop> lt = ch.SXTen();
            ViewBag.ds = lt;
            return View();
        }
        public ActionResult SXGiaTien()
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            List<Laptop> lt = ch.SXGiaTienTang();
            ViewBag.ds = lt;
            return View();
        }
        public ActionResult SXGiaTienGiam()
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            List<Laptop> lt = ch.SXGiaTienGiam();
            ViewBag.ds = lt;
            return View();
        }
    }
}