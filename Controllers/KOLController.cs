using HtmlAgilityPack;
using QuanLiKOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using QuanLiKOL.Models.Common;
namespace QuanLiKOL.Controllers
{
    public class KOLController : Controller
    {
        KOLDbContext db = new KOLDbContext();
        // GET: KOL
        public static int GetFlTiktok(string link)
        {
            int ketqua = -1;

            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };

            HtmlDocument document = htmlWeb.Load(link);
            var fl = document.DocumentNode.SelectSingleNode("//*[@id=\"app\"]/div[2]/div[2]/div/div[1]/h2[1]/div[2]/strong").InnerText;
            //var fl = document.DocumentNode.SelectSingleNode("//*[@id=\"react - root\"]/section/main/div/header/section/ul/li[2]/div/span").InnerText;
            if (fl != null)
            {
                ketqua = Convert.ToInt32(fl);
            }

            return ketqua;
        }
        public int loadfl(string id)
        {
            string link = db.KOLs.Where(x => x.id == id).FirstOrDefault().id;
            int fl = GetFlTiktok(link);
            return fl;
        }
        public int loaddiemKOL(string id)
        {
            int diem = 0;
            diem = (db.fls.Where(x => x.idkol == id).Count());
            diem += loadfl(id);
            return diem;
        }
        public ActionResult KOLHome()
        {
            return View();
        }
        public ActionResult HoSoKOL(string id)
        {
            KOL d = db.KOLs.Where(x => x.id == id).FirstOrDefault();
            return View(d);
        }

        public ActionResult EditHoSo()
        {
            string b = Session["id"].ToString();
            KOL kol = db.KOLs.Where(c => c.id == b).FirstOrDefault();
            return View(kol);
        }
        [HttpPost]
        public ActionResult EditHoSo(FormCollection form)
        {
            string id1 = form["id"].ToString();
            try
            {
                var kol = db.KOLs.Where(x => x.id == id1).FirstOrDefault();
                kol.nickname = form["nickname"].ToString();
                kol.ten = form["ten"].ToString();
                kol.diachi = form["diachi"].ToString();
                kol.sdt = form["sdt"].ToString();
                string ngay = form["ngaysinh"].ToString();
                kol.ngaysinh = DateTime.Parse(ngay);
                kol.gmail = form["gmail"];
                kol.avatar = form["avatar"];

                var lv1 = db.linhvucKOLs.Where(x => x.IdKOL == id1).ToList();
                db.linhvucKOLs.RemoveRange(lv1);
                db.SaveChanges();
                if (form["linhvuc"] != null)
                {
                    string linhvuc = form["linhvuc"].ToString();
                    string[] lv = new string[6];
                    for (int i = 0; i < 6; i++)
                    {
                        lv[i] = "1";
                    }
                    if (linhvuc.Contains("amthuc") == true)
                        lv[1] = "AT";
                    if (linhvuc.Contains("thethao") == true)
                        lv[2] = "TT";
                    if (linhvuc.Contains("amnhac") == true)
                        lv[3] = "AN";
                    if (linhvuc.Contains("thuongmai") == true)
                        lv[4] = "TM";
                    if (linhvuc.Contains("thoitrang") == true)
                        lv[5] = "TTr";
                    if (linhvuc.Contains("mypham") == true)
                        lv[0] = "MP";
                    List<linhvucKOL> lvkol = new List<linhvucKOL>();
                    for (int i = 0; i < 6; i++)
                    {
                        if (lv[i] != "1")
                        {
                            linhvucKOL temp2 = new linhvucKOL();
                            temp2.IdKOL = id1;
                            temp2.malv = lv[i];
                            lvkol.Add(temp2);
                        }
                    }
                    string malv = "";
                    foreach (var c in lvkol)
                    {
                        malv += c.malv + " ";
                    }
                    kol.malv = malv;
                    db.linhvucKOLs.AddRange(lvkol);

                    db.SaveChanges();
                }
                else
                {
                    var lvx = db.linhvucKOLs.Where(x => x.IdKOL == id1).ToList();
                    db.linhvucKOLs.RemoveRange(lvx);
                    db.SaveChanges();
                }



                return RedirectToAction("HoSoKOL", new { id = id1 });
            }
            catch (Exception ex)
            {
                return RedirectToAction("HoSoKOL", new { id = id1 });
            }

        }
        public ActionResult TrangCaNhan(string id)
        {

            Connect cn = new Connect();
            cn.kol = db.KOLs.Where(x => x.id == id).FirstOrDefault();
            cn.anh = cn.kol.Anhduocdangs.ToList();
            return View(cn);
        }
        public ActionResult Post()
        {
            string id = Session["id"].ToString();
            return View(db.BaiVietKOLs.Where(x => x.KOL.id == id).ToList());
        }
        [HttpPost]
        public ActionResult Post(FormCollection frm)
        {
            try
            {
                DateTime ngay = DateTime.Now;
                BaiVietKOL bv = new BaiVietKOL();
                bv.Id = Session["id"].ToString();
                bv.TieuDe = frm["tieude"].ToString();
                bv.anh = frm["anh"].ToString();
                bv.noidung = frm["noidung"].ToString();
                bv.ngaydang = ngay;

                String mabv = Session["id"].ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();


                bv.MaBV = mabv;
                db.BaiVietKOLs.Add(bv);
                db.SaveChanges();
                return RedirectToAction("LoadBVKOL2", new { id = bv.Id });
            }
            catch (Exception ex)
            {
                string id = Session["id"].ToString();
                return RedirectToAction("LoadBVKOL2", new { id = id });
            }

        }
        [HttpPost]
        public ActionResult thaydoilinhvuc(FormCollection frm)
        {
            string linhvuc = frm["linhvuc"].ToString();
            string[] lv = new string[6];
            for (int i = 0; i < 6; i++)
            {
                lv[i] = "1";
            }
            if (linhvuc.Contains("amthuc") == true)
                lv[1] = "AT";
            if (linhvuc.Contains("thethao") == true)
                lv[2] = "TT";
            if (linhvuc.Contains("amnhac") == true)
                lv[3] = "AN";
            if (linhvuc.Contains("thuongmai") == true)
                lv[4] = "TM";
            if (linhvuc.Contains("thoitrang") == true)
                lv[5] = "TTr";
            if (linhvuc.Contains("mypham") == true)
                lv[0] = "MP";
            List<linhvucKOL> lvkol = new List<linhvucKOL>();
            for (int i = 0; i < 6; i++)
            {
                if (lv[i] != "1")
                {
                    linhvucKOL temp2 = new linhvucKOL();
                    temp2.IdKOL = Session["id"].ToString();
                    temp2.malv = lv[i];
                    lvkol.Add(temp2);
                }
            }
            string id = Session["id"].ToString();
            var result = db.linhvucKOLs.Where(x => x.IdKOL == id).ToList();
            db.linhvucKOLs.RemoveRange(result);
            db.linhvucKOLs.AddRange(lvkol);
            db.SaveChanges();
            return RedirectToAction("KOLHome");
        }

        public ActionResult LoadBVKOL()
        {

            return View(db.BaiVietKOLs.OrderByDescending(x => x.ngaydang).ToList());
        }
        public ActionResult LoadBVKOL2(string id)
        {

            return View(db.BaiVietKOLs.Where(c=>c.Id == id).OrderByDescending(x => x.ngaydang).ToList());
        }
        public ActionResult XoaAnh(string id)
        {
            string idkol = Session["id"].ToString();
            int id1 = Convert.ToInt32(id);
            var result = db.Anhduocdangs.Where(x => x.tutang == id1).FirstOrDefault();
            db.Anhduocdangs.Remove(result);
            db.SaveChanges();
            return RedirectToAction("TrangCaNhan", "KOL", new { id = idkol });
        }
        public ActionResult ThemAnh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemAnh(FormCollection frm)
        {
            string idkol = Session["id"].ToString();
            Anhduocdang anh = new Anhduocdang();
            anh.IdKOL = idkol;
            anh.anh = frm["idanh"].ToString();
            db.Anhduocdangs.Add(anh);
            db.SaveChanges();
            return RedirectToAction("TrangCaNhan", "KOL", new { id = idkol });
        }
        public ActionResult XemFullBV(string id)
        {
            var result = db.baiviets.Where(x => x.mabv == id).FirstOrDefault();
            return View(result);
        }
        public ActionResult XemFL(string id)
        {
            var c = db.KOLs.Where(x => x.id == id).FirstOrDefault();
            return View(c.fls.ToList());
        }
        public ActionResult loadhdtheodk(string trang)
        {
            int trangthai = Convert.ToInt32(trang);
            string id = Session["id"].ToString();
            string quyen = Session["PQ"].ToString();
            List<hopdong> result = new List<hopdong>();
            if(quyen == "KHACH")
                result = db.hopdongs.Where(x => x.khach.id == id && x.trangthai.Value == trangthai).ToList();
            if(quyen == "KOL")
                result = db.hopdongs.Where(x => x.KOL.id == id && x.trangthai.Value == trangthai).ToList();
            return View(result);
        }
        public ActionResult XacNhanHD(string id)
        {
            var result = db.hopdongs.Where(x => x.mahd == id).FirstOrDefault();
            result.trangthai++;
            db.SaveChanges();
            return View("KOLHome");
        }
        public ActionResult ChiTietThuNhapKOL(string id)
        {
            var result = db.hopdongs.Where(x => x.KOL.id == id && (x.trangthai == 4 || x.trangthai == 5)).ToList();
            return View(result);
        }
    }
}