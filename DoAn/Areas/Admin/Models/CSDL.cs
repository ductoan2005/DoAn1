using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace DoAn.Areas.Admin.Models
{
    public class CSDLContext:DbContext
    {
        public DbSet<Laptop> Laptop { get; set; }
        public DbSet<ThuongHieu> ThuongHieu { get; set; }
        public DbSet<AdminAccount> AdminAccount { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public CSDLContext()
        {
            SqlConnectionStringBuilder sqlb = new SqlConnectionStringBuilder();
            sqlb.DataSource = "DESKTOP-449F4SP";
            sqlb.InitialCatalog = "DoAn1";
            sqlb.IntegratedSecurity = true;

            Database.Connection.ConnectionString = sqlb.ConnectionString;
        }
    }
    public class Laptop
    {
        [Key]
        public int LaptopID { get; set; }
        public string TenLaptop { get; set; }
        public int Giatien { get; set; }
        public string ThongSo { get; set; }
        public string Image { get; set; }
        public Nullable<int> MaTH { get; set; }
        public virtual ThuongHieu ThuongHieu { get; set; }
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
    }
    public class ThuongHieu
    {
        [Key]
        public int MaTH { get; set; }
        public string TenThuongHieu { get; set; }
    }
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string DiaChiKH { get; set; }
        public string DienThoaiKH { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
    public class HoaDon
    {
        [Key]
        public int MaHD { get; set; }
        public DateTime Ngay { get; set; }
        public int SoLuong { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual Laptop Laptops { get; set; }

    }
    public class AdminAccount
    {
        [Key]
        public int AdminID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}