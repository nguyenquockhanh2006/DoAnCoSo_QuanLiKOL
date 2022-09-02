using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiKOL.Models;
using QuanLiKOL.Models.Common;
namespace QuanLiKOL.Controllers
{
    public class KhachHangController : Controller
    {
        KOLDbContext db = new KOLDbContext();
        // GET: KhachHang
        public ActionResult Index()
        {
            return RedirectToAction("LoadBVQuanTam");
        }
        [HttpPost]
        public ActionResult thaydoilinhvuc(FormCollection frm)
        {
            try
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
                List<linhvucKhach> lvkol = new List<linhvucKhach>();
                for (int i = 0; i < 6; i++)
                {
                    if (lv[i] != "1")
                    {
                        linhvucKhach temp2 = new linhvucKhach();
                        temp2.idkhach = Session["id"].ToString();
                        temp2.malv = lv[i];
                        lvkol.Add(temp2);
                    }
                }
                string id = Session["id"].ToString();
                var result = db.linhvucKhaches.Where(x => x.idkhach == id).ToList();
                db.linhvucKhaches.RemoveRange(result);
                db.linhvucKhaches.AddRange(lvkol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("trave404");
            }
        }
        public ActionResult Followkol(string id)
        {
            fl f = new fl();
            f.idkol = id;
            f.idkhach = Session["id"].ToString();
            f.TG = DateTime.Now;
            db.fls.Add(f);
            db.SaveChanges();
            return RedirectToAction("TrangCaNhan", "KOL", new { id = id });
        }
        public ActionResult DeFollow(string id)
        {
            string k = Session["id"].ToString();
            var result = db.fls.Where(x => x.idkhach == k && x.idkol == id).FirstOrDefault();

            db.fls.Remove(result);
            db.SaveChanges();
            return RedirectToAction("TrangCaNhan", "KOL", new { id = id });
        }
        [HttpPost]
        public ActionResult GoiYKOL(FormCollection frm)
        {
            try
            {
                string linhvuc = frm["linhvuc"].ToString();
                string loaihd = frm["loaihd"].ToString();
                string trigia = frm["trigia"].ToString();
                string ma = "ma1";
                decimal tg = Convert.ToDecimal(trigia);
                var result = db.KOLs.Where(x => x.malv.Contains(linhvuc) == true && x.trangthai == 1).ToList();
                var ct = db.cautrucs.Where(x => x.ma == 1).FirstOrDefault();
                if (tg > ct.kol10005000.Value)
                    ma = "ma2";
                if (tg > ct.kol515.Value)
                    ma = "ma3";
                if (tg > ct.kolt15k.Value)
                    ma = "ma4";
                List<KOL> result1 = new List<KOL>();
                if (ma == "ma1")
                    result1 = result.Where(x => x.kolcore <= 1000).ToList();
                if (ma == "ma2")
                    result1 = result.Where(x => x.kolcore > 1000).ToList();
                if (ma == "ma3")
                    result1 = result.Where(x => x.kolcore > 5000).ToList();
                if (ma == "ma4")
                    result1 = result.Where(x => x.kolcore > 15000).ToList();
                return View(result1);
            }catch(Exception ex)
            {
                return View(db.KOLs.ToList());
            }
        }
        public ActionResult GoiYKOL()
        {

            return View(db.KOLs.ToList());
        }
        public ActionResult BookKOL(string id)
        {

            return View(db.KOLs.Where(x => x.id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult BookKOL(FormCollection frm)
        {
            hopdong hd = new hopdong();
            string cn = frm["chapnhan"].ToString();
            if (cn != "YES")
                return RedirectToAction("Index");
            else
            {
                string nbd = Session["nbdbook"].ToString();
                DateTime ngay = DateTime.Now;
                string nkt = Session["ngaykthd"].ToString();
                hd.mahd = frm["mahd"].ToString();
                hd.tenhd = frm["tenhd"].ToString();
                hd.bena = frm["idkol"].ToString();
                hd.benb = frm["idkhach"].ToString();
                hd.AnhHD = frm["sanpham"].ToString();
                hd.ngayyeucau = ngay;
                hd.trangthai = 1;
                hd.trigia = decimal.Parse(frm["trigia"].ToString());
                hd.linhvuc = frm["linhvuc"].ToString();
                //hd.trigia = 
                hd.ngaybatdau = DateTime.Parse(nbd);
                hd.ngayketthuc = DateTime.Parse(nkt);
                hd.dieukhoan = frm["dieukhoanhd"].ToString();
                db.hopdongs.Add(hd);
                db.SaveChanges();

                SendGmail sendkol = new SendGmail();
                string kolnhan = frm["idkol"].ToString();
                KOL nhan = db.KOLs.Where(x => x.id == kolnhan).FirstOrDefault();
                
                string sub2 = "Thông báo có hợp đồng mới!";
                string body2 = "Bạn có một hợp đồng mới! ,xin duyệt sớm trước ngày hợp đồng bắt đầu!\n Xin cảm ơn!";
                sendkol.send(nhan.gmail, sub2, body2);

                SendGmail send = new SendGmail();
                string nguoinhan = db.khaches.Where(x => x.id == hd.benb).FirstOrDefault().gmail.ToString();
                string sub = "Thông báo book kol";
                string body = "Đã đăng kí hợp đồng thành công , bạn có thể quan sát tiến độ duyệt hợp đồng trên trang cá nhân, chú ý theo dõi và thanh toán đúng hạn nhé,\n Xin cảm ơn!";
                send.send(nguoinhan, sub, body);

                return RedirectToAction("CTHopDong", "KhachHang", new { id = hd.mahd });
            }
        }
        public ActionResult CTHopDong(string id)
        {
            hopdong hd = db.hopdongs.Where(x => x.mahd == id).FirstOrDefault();
            return View(hd);
        }
        public ActionResult LoaiHD(string id)
        {

            return View(db.KOLs.Where(x => x.id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult LoaiHD(FormCollection frm)
        {
            try
            {
                string id = frm["idkol"].ToString();
                string nbd = frm["nbd"].ToString();
                string nkt = frm["bkt"].ToString();
                string loai = frm["loaihd"].ToString();
                var Model = db.KOLs.Where(x => x.id == id).FirstOrDefault();
                DateTime ngaybd = DateTime.Parse(nbd);
                DateTime ngaykt = DateTime.Parse(nkt);
                TimeSpan value = ngaykt.Subtract(ngaybd);
                int ngay = value.Days;
                Session["nbdbook"] = nbd;
                Session["ngaykthd"] = nkt;
                var ct = db.cautrucs.Where(x => x.ma == 1).FirstOrDefault();
                decimal? t1ngay = ct.koln1000;
                if (Model.kolcore > 1000)
                { t1ngay = ct.kol515; }
                if (Model.kolcore > 5000)
                { t1ngay = ct.kol515; }
                if (Model.kolcore > 15000)
                { t1ngay = ct.kolt15k; }
                decimal? nhieungay = t1ngay * ngay;
                if (loai == "NH")
                    Session["trigia"] = nhieungay.ToString();
                else
                {
                    decimal? temp = (nhieungay / 100) * 80;
                    Session["trigia"] = temp.ToString();
                }
                return RedirectToAction("BookKOL", "KhachHang", new { id = id });
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        public ActionResult LoadBVQuanTam()
        {
            try
            {
                List<BaiVietKOL> trave = new List<BaiVietKOL>();
                List<BaiVietKOL> trave1 = new List<BaiVietKOL>();
                List<BaiVietKOL> trave2 = new List<BaiVietKOL>();
                var result = db.BaiVietKOLs.ToList();
                string id = Session["id"].ToString();
                foreach (var c in result)
                {
                    if (kiemtra(id, c.Id) == 1)
                        trave.Add(c);
                }
                trave.OrderByDescending(x => x.ngaydang).ToList();
                foreach (var c in trave)
                {
                    if (kiemtrafl(id, c.Id) == 1)
                        trave1.Add(c);
                    else
                        trave2.Add(c);
                }
                foreach (var c in trave2)
                    trave1.Add(c);

                return View(trave1);
            }
            catch (Exception ex)
            {

                return RedirectToAction("trave404");
            }
        }
        public ActionResult trave404()
        {
            return View();
        }
        public int kiemtra(string id, string kol)
        {
            var result = db.linhvucKhaches.Where(x => x.idkhach == id).ToList();
            var linhvuckol = db.linhvucKOLs.Where(x => x.IdKOL == kol).ToList();
            foreach (var c in result)
            {
                foreach (var i in linhvuckol)
                {
                    if (c.malv == i.malv)
                        return 1;
                }
            }
            return 0;
        }
        public int kiemtrafl(string id, string kol)
        {
            var result = db.fls.Where(x => x.idkhach == id && x.idkol == kol).FirstOrDefault();
            if (result != null)
                return 1;
            return 0;
        }
        public ActionResult loadhdtheodk(string trang)
        {
            int trangthai = Convert.ToInt32(trang);
            string id = Session["id"].ToString();
            var result = db.hopdongs.Where(x => x.benb == id && x.trangthai.Value == trangthai).ToList();
            return View(result);
        }
        public ActionResult CTfollow()
        {
            string id = Session["id"].ToString();
            var temp = db.fls.Where(x => x.idkhach == id).OrderByDescending(x => x.TG).ToList();
            List<KOL> lkol = new List<KOL>();
            foreach (var c in temp)
            {
                KOL kol = new KOL();
                kol = db.KOLs.Where(x => x.id == c.idkol).FirstOrDefault();
                lkol.Add(kol);
            }
            return View(lkol);
        }
        [HttpPost]
        public ActionResult BLBVKOL(FormCollection frm)
        {
            BinhLuanBVKOL bl = new BinhLuanBVKOL();
            bl.IdKhach = Session["id"].ToString();
            bl.MaBV = frm["mabv"].ToString();
            bl.ngaybl = DateTime.Now;
            bl.Noidung = frm["noidung"].ToString();
            db.BinhLuanBVKOLs.Add(bl);
            db.SaveChanges();
            return RedirectToAction("LoadBVQuanTam");
        }
        public ActionResult Like(string mabv)
        {
            LuotThichBVKOL like = new LuotThichBVKOL();
            like.MaBV = mabv;
            like.idkhach = Session["id"].ToString();
            like.ngaythich = DateTime.Now;
            db.LuotThichBVKOLs.Add(like);
            db.SaveChanges();
            return RedirectToAction("LoadBVQuanTam");
        }
        public ActionResult DisLike(string mabv)
        {
            string id = Session["id"].ToString();
            var result = db.LuotThichBVKOLs.Where(x => x.MaBV == mabv && x.idkhach == id).FirstOrDefault();
            db.LuotThichBVKOLs.Remove(result);
            db.SaveChanges();
            return RedirectToAction("LoadBVQuanTam");
        }
        public ActionResult ReportKOL(string id)
        {
            var result = db.KOLs.Where(x => x.id == id).FirstOrDefault();
            return View(result);
        }
        [HttpPost]
        public ActionResult ReportKOL(FormCollection frm)
        {
            ToCao report = new ToCao();
            report.Idtocao = Session["id"].ToString();
            report.idbitocao = frm["id"].ToString();
            report.Lido = frm["lido"].ToString();
            report.thoigian = DateTime.Now;
            report.Trangthai = 1;
            db.ToCaos.Add(report);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult TrangCaNhanKhach(string id)
        {

            return View(db.khaches.Where(x => x.id == id).FirstOrDefault());
        }
        public ActionResult EditHoSo(string id)
        {
            return View(db.khaches.Where(x => x.id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditHoSo(FormCollection form)
        {
            string id = Session["id"].ToString();
            try
            {
                var kol = db.khaches.Where(x => x.id == id).FirstOrDefault();
                kol.nickname = form["nickname"].ToString();
                kol.ten = form["ten"].ToString();
                kol.diachi = form["diachi"].ToString();
                kol.sdt = form["sdt"].ToString();
                string ngay = form["ngaysinh"].ToString();
                kol.ngaysinh = DateTime.Parse(ngay);
                kol.gmail = form["gmail"];
                kol.avatar = form["avatar"];
                db.SaveChanges();
                return RedirectToAction("TrangCaNhanKhach", "KhachHang", new { id = id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("TrangCaNhanKhach", "KhachHang", new { id = id });
            }
        }
        public ActionResult LoadDSFollowing(string id)
        {
            var result = db.fls.Where(x => x.idkhach == id).ToList();
            return View(result);
        }
        public ActionResult XoaHD(string id)
        {
            var result = db.hopdongs.Where(x => x.mahd == id).FirstOrDefault();
            db.hopdongs.Remove(result);
            db.SaveChanges();
            return RedirectToAction("loadhdtheodk", "KhachHang", new { trang = 1 });
        }
        public ActionResult TimKiemKOL()
        {
            return View(db.KOLs.OrderByDescending(x=>x.nickname).ToList());
        }
        [HttpPost]
        public ActionResult TimKiemKOL(FormCollection frm)
        {
            string id = frm["nik"].ToString();
            return View(db.KOLs.Where(x=>x.nickname.Contains(id) == true).ToList());
        }
    }
}