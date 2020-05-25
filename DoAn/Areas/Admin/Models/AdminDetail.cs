using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn.Areas.Admin.Models
{
    public class AdminDetail
    {
        public List<AdminAccount> DSAdmin()
        {
            List<AdminAccount> list = new List<AdminAccount>();
            using (CSDLContext db = new CSDLContext())
            {
                list = db.AdminAccount.ToList();
            }
            return list;
        }
        //public bool Login(string username, string password)
        //{
        //    bool login;
        //    using (CSDLContext db = new CSDLContext())
        //    {
        //        db.AdminAccount.Where(p=>p.Username == username && p.Password==password);

        //    }
        //    return login;
        //}
    }
}