using DoAn.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Areas.Admin.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        
        private CSDLContext context = new CSDLContext();
        // GET: Admin/Home
      
        public ActionResult Index(int? id)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
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
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                int Count;
                Count = context.AdminAccount.Where(s => s.Username == username && s.Password == password).Count();
                
                if (Count > 0)
                {
                    Session["DisplayName"] = context.AdminAccount.FirstOrDefault().DisplayName;
                    Session["AdminID"] = context.AdminAccount.FirstOrDefault().AdminID;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Login Failed";
                    return RedirectToAction("Login");
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult Add(Laptop laptop)
        {
            ViewBag.MaTH = new SelectList(context.ThuongHieu, "MaTH", "TenThuongHieu",laptop.MaTH);
            return View(laptop);
        }
        public ActionResult DoiMatKhau(int id)
        {
            AdminDetail admin = new AdminDetail();
            AdminAccount ac = admin.Tim(id);
            ViewBag.ac = ac;
            return View();
        }
        [HttpPost]
        public ActionResult ResultDoiMatKhau(int id, string password)
        {
            AdminDetail admin = new AdminDetail();
            int tmp = admin.DoiMatKhau(id, password);
            if (tmp != 0) ViewBag.Message = "Success";
            else ViewBag.Message = "Failed";
            return View();
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
            //CuaHangBanLaptop ch = new CuaHangBanLaptop();
            //List<Laptop> ds = ch.timgandung(tensp);
            //ViewBag.ds = ds;
            
           var c =context.Laptop.Where(s => s.TenLaptop.Contains(tensp)).ToList();
            ViewBag.c = c;
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