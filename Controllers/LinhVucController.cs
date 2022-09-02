using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiKOL.Models;
namespace QuanLiKOL.Controllers
{
    public class LinhVucController : Controller
    {
        KOLDbContext db = new KOLDbContext();
        // GET: LinhVuc
        //public ActionResult LoadKOLLV()
        //{
            
        //    return View(db.KOLs.ToList());
        //}
        public ActionResult LoadKOLLV(string s)
        {

            if (s != "ALL")
            {
                Session["qwer"] = s;
                var result = db.KOLs.Where(x => x.malv.Contains(s) == true && x.trangthai == 1).ToList();
                return View(result);
            }
            Session["qwer"] = "ALL";
            return View(db.KOLs.ToList());
           
        }

    }
}