using QuanLiKOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiKOL.Models.Common;
using System.Windows;

namespace QuanLiKOL.Controllers
{
    public class AdminController : Controller
    {
        KOLDbContext db = new KOLDbContext();
        public ActionResult AdminHome()
        {
            return View();
        }

        //  bieu do tron (thong ke hop dong cua tung linh vuc - thoi gian da nhap)

        //  bieu do cot (thong ke doanh thu thang theo nam) 

        //  bieu do mien (thong ke so luong hop dong - thoi gian da nhap)

        public ActionResult LoadBieuDo()
        {
            string nam = DateTime.Now.Year.ToString();
            Session["nam"] = nam;
            bieudo bd = new bieudo(nam);
            return View(bd);
        }
        [HttpPost]
        public ActionResult LoadBieuDo(FormCollection frm)
        {
            string id = "";
            string pq = "";
            if (Session["id"] != null)
            {
                id = Session["id"].ToString();
                pq = Session["PQ"].ToString();
            }

            try
            {
                Session.Clear();
                if (id != "")
                {
                    Session["id"] = id;
                    Session["PQ"] = pq;
                }
                string sntntn = frm["tungay"].ToString();
                string sntndn = frm["denngay"].ToString();

                DateTime ntntn = DateTime.Parse(sntntn);
                DateTime ntndn = DateTime.Parse(sntndn);

                Session["nam"] = ntndn.Year.ToString();
                Session["tu"] = "" + ntntn.Day + "/" + ntntn.Month + "/" + ntntn.Year;
                Session["den"] = "" + ntndn.Day + "/" + ntndn.Month + "/" + ntndn.Year;

                bieudo bd = new bieudo(ntntn, ntndn);

                ViewBag.tungay = sntntn;
                ViewBag.denngay = sntndn;
                return View(bd);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LoadBieuDo");
            }
        }
        public ActionResult LoadKOL()
        {
            //List<KOL> list = new List<KOL>();

            return View(db.KOLs.ToList());
        }
        [HttpPost]
        public ActionResult LoadKOL(FormCollection frm)
        {
            string lv = frm["linhvuc"].ToString();
            if (lv == "All")
                return RedirectToAction("LoadKOL");
            List<KOL> list = new List<KOL>();
            list = (from c in db.KOLs where c.malv.Contains(lv) == true select c).ToList();
            return View(list);
        }
        public ActionResult LoadKOLTB()
        {
            List<KOL> list = new List<KOL>();

            return View();
        }
        //  Quan Li Hop Dong
        public ActionResult LoadHDL()
        {
            return View(db.hopdongs.ToList());
        }
        [HttpPost]
        public ActionResult LoadHDL(FormCollection frm)
        {
            try
            {
                string ma = frm["search"].ToString();
                return View(db.hopdongs.Where(x => x.mahd.Contains(ma) == true).ToList());
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult LoadHDConHan()
        {
            DateTime now = DateTime.Now;
            return View((from c in db.hopdongs where c.ngayketthuc >= now && c.trangthai == 4 select c).ToList());
        }
        public ActionResult LoadHDChoKOL()
        {

            return View((from c in db.hopdongs where c.trangthai == 1 select c).ToList());
        }
        public ActionResult LoadHDChoDuyet()
        {

            return View((from c in db.hopdongs where c.trangthai == 2 select c).ToList());
        }
        public ActionResult LoadHDChoTT()
        {

            return View((from c in db.hopdongs where c.trangthai == 3 select c).ToList());
        }
        public ActionResult LoadHDEnd()
        {
            DateTime ngay = DateTime.Now;
            return View((from c in db.hopdongs where c.trangthai == 5 && c.ngayketthuc <= ngay select c).ToList());
        }
        public ActionResult listKOLTieuBieu()
        {
            DateTime hientai = DateTime.Now;
            var ressult = db.KOLs.Where(x =>x.kolcore > 10000 && x.hopdongs.Where(c => c.ngayketthuc.Value > hientai).Count() >= 6).ToList();

            return View(ressult);
        }
        //public ActionResult listKOLTieuBieu(FormCollection frm)
        //{

        //    return View();
        //}

        public ActionResult listKOLTiemNang()
        {
            DateTime ngay = DateTime.Now;
            var result = db.KOLs.Where(x => x.kolcore >= 5000 && x.hopdongs.Where(c=>c.ngayketthuc.Value > ngay).ToList().Count < 5).ToList();

            return View(result);
        }
        //public ActionResult listKOLTiemNang(FormCollection frm)
        //{
        //    return View();
        //}
        public int xettontai(List<KOL> l, string s)
        {
            foreach (var c in l)
                if (c.id == s)
                    return 1;
            return 0;
        }
        public ActionResult listKOLNguyHiem()
        {
            DateTime ngay = DateTime.Now;

            var result = db.KOLs.Where(x => x.kolcore <= 500 && x.hopdongs.Where(c => c.ngayketthuc.Value >= ngay && c.trangthai == 4).Count() == 0).ToList();


            return View(result);
        }

        public ActionResult RankOfKOL()
        {

            return View(db.KOLs.OrderByDescending(x => x.kolcore).ToList());
        }
        [HttpPost]
        public ActionResult RankOfKOL(FormCollection frm)
        {
            try
            {
                string phuongthuc = frm["phuongthuc"].ToString();
                string lv = frm["lv"].ToString();
                string top = frm["top"].ToString();
                int aa = 0;
                if (top == "TOP100")
                    aa = 100;
                if (top == "TOP20")
                    aa = 20;
                if (top == "TOP10")
                    aa = 10;
                if (top == "TOP5")
                    aa = 5;

                List<KOL> result0 = db.KOLs.OrderByDescending(x => x.kolcore).ToList();
                List<KOL> result1 = new List<KOL>();
                List<KOL> result2 = new List<KOL>();
                List<KOL> result3 = new List<KOL>();
                
                if (phuongthuc == "KOL")
                    result1 = result0;
                if (phuongthuc == "FL")
                    result1 = result0.OrderByDescending(x => x.fls.Count).ToList();
                if (phuongthuc == "TIKTOK")
                    result1 = result0.OrderByDescending(x => x.fltiktok).ToList();
                //--
                result2 = result1.Where(x => x.malv.Contains(lv) == true).ToList();
                //--
                if (aa == 0)
                    result3 = result2;
                else
                {
                    int demne = 0;
                    foreach (var c in result2)
                    {
                        if (demne < aa)
                            result3.Add(c);
                        demne++;
                    }
                }
                //--
                return View(db.KOLs.OrderByDescending(x => x.kolcore).ToList());
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        public ActionResult FeaturedArticles()
        {
            var result = db.BaiVietKOLs.OrderByDescending(x => x.LuotThichBVKOLs.Count).ToArray();
            List<BaiVietKOL> lbv = new List<BaiVietKOL>();
            for (int i = result.Length - 1; i > -1; i--)
                lbv.Add(result[i]);
            List<BaiVietKOL> bv2 = new List<BaiVietKOL>();
            int dem = 0;
            foreach (var c in lbv)
            {
                if (c != null)
                    bv2.Add(c);
                dem++;
                if (dem == 99)
                    break;
            }
            return View(bv2);
        }
        [HttpPost]
        public ActionResult FeaturedArticles(FormCollection frm)
        {
            string thoigian = "", top = "";
            DateTime ngay = DateTime.Now;
            thoigian = frm["time"].ToString();
            top = frm["top"].ToString();

            List<BaiVietKOL> bv = db.BaiVietKOLs.OrderByDescending(x => x.LuotThichBVKOLs.Count).ToList();
            if (thoigian != "ALL")
            {
                if (thoigian == "NAM")
                    bv = bv.Where(x => x.ngaydang.Value.Year == ngay.Year).OrderByDescending(x => x.LuotThichBVKOLs.Count).ToList();
                if (thoigian == "THANG")
                    bv = bv.Where(x => x.ngaydang.Value.Year == ngay.Year && x.ngaydang.Value.Month == ngay.Month).OrderByDescending(x => x.LuotThichBVKOLs.Count).ToList();
            }
            int temp = Convert.ToInt32(top);
            List<BaiVietKOL> bv2 = new List<BaiVietKOL>();
            int dem = 0;
            foreach (var c in bv)
            {
                if (c != null)
                    bv2.Add(c);
                dem++;
                if (dem == temp)
                    break;
            }
            return View(bv2);
        }
        public ActionResult form404()
        {
            return View();
        }
        public ActionResult addtrangthai(string id)
        {
            var result = db.hopdongs.Where(x => x.mahd == id).FirstOrDefault();
            string aa = id;
            string idad = Session["id"].ToString();
            admin ad = db.admins.Where(x => x.id == idad).FirstOrDefault();
            int tt2 = Convert.ToInt32(result.trangthai);
            int tt1 = tt2;
            tt2++;
            result.trangthai = tt2;
            db.SaveChanges();
            if (tt1 == 1)
            {

                return RedirectToAction("LoadHDChoKOL");
            }
            if (tt1 == 2)
            {
                result.benc = ad.id;
                
                
                db.SaveChanges();
                return RedirectToAction("LoadHDChoDuyet");
            }
            if (tt1 == 3)
            {
                return RedirectToAction("LoadHDChoTT");
            }
            if (tt1 == 5)
            {
                db.hopdongs.Remove(result);
                db.SaveChanges();
                return RedirectToAction("LoadHDEnd");
            }
            return RedirectToAction("LoadHDEnd");
        }
        public ActionResult showdetail(string id)
        {
            var result = db.hopdongs.Where(x => x.mahd == id).FirstOrDefault();
            return View(result);
        }
        public ActionResult Deletedb(string id)
        {
            var hd = db.hopdongs.Where(x => x.mahd == id).FirstOrDefault();
            db.hopdongs.Remove(hd);
            db.SaveChanges();
            return RedirectToAction("LoadHDChoKOL");
        }
        public ActionResult showdetailBV(string id)
        {
            var result = db.BaiVietKOLs.Where(x => x.MaBV == id).FirstOrDefault();
            return View(result);
        }
        public ActionResult LoadKhach()
        {
            return View(db.khaches.ToList());
        }
        [HttpPost]
        public ActionResult LoadKhach(string id)
        {
            return View(db.khaches.Where(x=>x.id == id).FirstOrDefault());
        }
        public ActionResult LoadKhachTB()
        {
            var result = db.khaches.OrderByDescending(x => x.hopdongs.Sum(c => c.trigia)).ToList();
            List<khach> result2 = new List<khach>();
            int dem = 0;
            foreach (var c in result)
            {
                dem++;
                result2.Add(c);
                if (dem == 100)
                    break;
            }
            return View(result);
        }
        
        public ActionResult ThongBaoHopDong(string id)
        {
            SendGmail sendgmail = new SendGmail();
            //try
            //{
                var result = db.hopdongs.Where(x => x.mahd == id).FirstOrDefault();
                string ngay = result.ngaybatdau.Value.Day.ToString() + "/" + result.ngaybatdau.Value.Month.ToString() + "/" + result.ngaybatdau.Value.Year.ToString();
                string TieuDe = "Thông báo xác nhận hợp đồng";
                string Body = "Xin chào " + result.KOL.nickname +"\nChúng tôi đến từ bộ phận quản lí KOL" +" !\nHợp đồng mang mã số " + result.mahd + " : " + result.tenhd + " -chưa được xác nhận\n"
                    + "Xin vui lòng xem chi tiết và xác nhận trước ngày " + ngay + "\n" + "Xin cảm ơn!";
                sendgmail.send(result.KOL.gmail, TieuDe, Body);
                //WebMsgBox.Show("Đã thông báo!");
                return RedirectToAction("LoadHDChoKOL");
            //}
            //catch(Exception ex)
            //{
            //    return RedirectToAction("LoadHDChoKOL");
            //    //WebMsgBox.Show("Thất bại, gmail được cung cấp bởi KOL có thể không đúng!");
            //}
        }
        public ActionResult ShowDetailKOL(string id)
        {
            var result = db.KOLs.Where(x => x.id == id).FirstOrDefault();
            return View(result);
        }
        public ActionResult DeleteKOL(string id)
        {
            var result = db.KOLs.Where(x => x.id == id).FirstOrDefault();
            db.KOLs.Remove(result);
            db.SaveChanges();
            return RedirectToAction("listKOLNguyHiem");
        }
        public ActionResult Report()
        {
            var temp = db.ToCaos.OrderByDescending(x => x.thoigian).ToArray();
            List<ToCao> result = new List<ToCao>();

            for (int i = temp.Length - 1; i > -1; i--)
                result.Add(temp[i]);

            return View(result);
        }
        public ActionResult XoaTaiKhoanReport(string idtocao1)
        {
            int idtocao = Convert.ToInt32(idtocao1);
            var tocao = db.ToCaos.Where(x => x.matocao == idtocao).FirstOrDefault();
            string id = tocao.idbitocao; 
            var result = db.taikhoans.Where(x => x.id == id).FirstOrDefault();
            var result2 = db.KOLs.Where(x => x.id == id).FirstOrDefault();
            
            db.taikhoans.Remove(result);
            result2.trangthai = 2;
            tocao.Trangthai = 2;
            
            db.SaveChanges();
            return RedirectToAction("Report");
        }
        public ActionResult CanhCaoTaiKhoanReport(string idtocao1)
        {
            int idtocao = Convert.ToInt32(idtocao1);
            var tocao = db.ToCaos.Where(x => x.matocao == idtocao).FirstOrDefault();
            string id = tocao.idbitocao;
            var result = db.KOLs.Where(x => x.id == id).FirstOrDefault();

            SendGmail send = new SendGmail();
            string tieude = "Xử lí vi phạm";
            string ngay = tocao.thoigian.Value.Day + "/" + tocao.thoigian.Value.Month + "/" + tocao.thoigian.Value.Year;
            string body = "Xin chào " + result.ten + "!\n" + "Chúng tôi đến từ ban uản lí cộng đồng KOL!\n" + "Ngày " + ngay + " chúng tôi nhận được một khiếu nại về bạn với lí do " + tocao.Lido + "\n" + "Vui lòng khắc phục sớm nhất có thể, nếu không tài khoản của bạn có thể sẽ bị xóa vĩnh viễn!";
            send.send(result.gmail, tieude, body);
            
            tocao.Trangthai = 2;
            db.SaveChanges();

            return RedirectToAction("Report");
        }
        public ActionResult BoquaReport(string idtocao1)
        {
            int idtocao = Convert.ToInt32(idtocao1);
            var tocao = db.ToCaos.Where(x => x.matocao == idtocao).FirstOrDefault();

            tocao.Trangthai = 2;

            db.SaveChanges();
            return RedirectToAction("Report");
        }
        [HttpPost]
        public ActionResult admindangbai(FormCollection frm)
        {
            baiviet bv = new baiviet();
            string id = Session["id"].ToString();
            string ma = Session["id"].ToString() + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            var ad = db.admins.Where(x => x.id == id).FirstOrDefault();
            bv.mabv = ma;
            bv.id = ad.id;
            bv.tenbv = frm["tenbaiviet"].ToString();
            bv.anh = frm["anh"].ToString();
            bv.noidung = frm["noidung"].ToString();
            db.baiviets.Add(bv);
            db.SaveChanges();
            return RedirectToAction("AdminHome");

        }
        [HttpPost]
        public ActionResult thaydoicautruc(FormCollection frm)
        {
            cautruc ct = db.cautrucs.Where(x => x.ma == 1).FirstOrDefault();

            //ct.anhnen = frm["anhnen"].ToString();
            ct.ct1 = frm["quangcao"].ToString();
            ct.koln1000 = Convert.ToDecimal(frm["koln1"].ToString());
            ct.kol10005000 = Convert.ToDecimal(frm["kol15"].ToString());
            ct.kol515 = Convert.ToDecimal(frm["kol515"].ToString());
            ct.kolt15k = Convert.ToDecimal(frm["kolt15"].ToString());

            db.SaveChanges();
            return RedirectToAction("AdminHome");
        }
        public ActionResult loaddiemkol()
        {
            var result = db.KOLs.ToList();
            foreach(var c in result)
            {
                int thuong;
                if (c.gioithieu == null || c.gioithieu.Trim() == "")
                    thuong = 0;
                else
                    thuong = Convert.ToInt32(c.gioithieu);
                c.kolcore = c.fltiktok + c.fls.Count + thuong;
                db.SaveChanges();
            }
            return RedirectToAction("AdminHome");
        }
        [HttpPost]
        public ActionResult thuongdiem(FormCollection frm)
        {
            string id = frm["idnhan"].ToString();
            var result = db.KOLs.Where(x => x.id == id).FirstOrDefault();
            result.gioithieu = frm["diem"].ToString();
            db.SaveChanges();
            string lido = frm["lido"].ToString();
            SendGmail send = new SendGmail();
            string body = "Xin chao " + result.nickname + "!\n" + "Bạn nhận được " + frm["diem"].ToString() + " thưởng\n Vì lí do: " + lido;
            send.send(result.gmail, "Thưởng điểm KOL ", body);
            return RedirectToAction("AdminHome");
        }
    }

}