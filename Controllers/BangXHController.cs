using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiKOL.Models;
using QuanLiKOL.Models.Common;
namespace QuanLiKOL.Controllers
{
    public class BangXHController : Controller
    {
        // GET: BangXH
        public ActionResult Index()
        {
            return View();
        }
        KOLDbContext db = new KOLDbContext();
        // GET: BangXH
        public ActionResult All(string lv)
        {
            List<KOL> ds = new List<KOL>();
            if (lv == "All" || lv=="HDg" || lv=="tiktok")
            {
                if(lv == "All")
                {
                    ds = db.KOLs.Where(x => x.trangthai == 1).OrderByDescending(x => x.kolcore).ToList();
                    Session["dkxh"] = "kolcore";
                }    
                if (lv == "HDg")
                {
                    ds = db.KOLs.Where(x => x.trangthai == 1).OrderByDescending(x => x.hopdongs.Where(c => c.trangthai == 4 || c.trangthai == 5).Count()).ToList();
                    Session["dkxh"] = "slHD";
                }   
                if (lv == "tiktok")
                {
                    ds = db.KOLs.Where(x => x.trangthai == 1).OrderByDescending(x => x.fltiktok).ToList();
                    Session["dkxh"] = "tiktok";
                }    
            }    
            else
            {
                ds = db.KOLs.Where(x => x.malv.Contains(lv) == true && x.trangthai == 1).OrderByDescending(x => x.kolcore).ToList();
            }
            var max = ds.FirstOrDefault();
            KOL nhi = new KOL();
            KOL ba = new KOL();
            int dem = 0;
            foreach (KOL d in ds)
            {
                nhi = d;
                dem++;
                if (dem == 2)
                {
                    break;
                }

            }
            dem = 0;
            foreach (KOL d in ds)
            {
                ba = d;
                dem++;
                if (dem == 3)
                {
                    break;
                }

            }
            Session["max"] = max.avatar;
            Session["ten1"] = max.nickname;
            Session["nhi"] = nhi.avatar;
            Session["ten2"] = nhi.nickname;
            Session["ba"] = ba.avatar;
            Session["ten3"] = ba.nickname;
            //List<DsKOL> ds = db.DsKOLs.OrderByDescending(x => x.diem).ToList();
            return View(ds);
        }
       
        public ActionResult ChiTiet(string ID)
        {
            KOL x = db.KOLs.FirstOrDefault(a => a.id == ID);
            return View(x);
        }
       

    }
}