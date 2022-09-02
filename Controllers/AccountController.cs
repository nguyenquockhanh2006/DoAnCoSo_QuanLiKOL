using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using QuanLiKOL.Models;
using QuanLiKOL.Models.Common;
using System.Windows;
namespace QuanLiKOL.Controllers
{
    public class AccountController : Controller
    {
        //
        KOLDbContext db = new KOLDbContext();
        // GET: Account
        //-------------------------------------------------------
        // login
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                string id = form["taikhoan"].ToString();
                string pass = form["matkhau"].ToString();
                var f_password = GetMD5(pass);

                taikhoan data = db.taikhoans.Where(s => s.id.Equals(id) && s.pass.Equals(f_password)).FirstOrDefault();
                if (data != null)
                {
                    if (data.pq == "KOL")
                    {
                        Session["id"] = data.id;
                        Session["PQ"] = data.pq;
                        return RedirectToAction("KOLHome", "KOL");
                    }
                    if (data.pq == "ADMIN")
                    {
                        Session["id"] = data.id;
                        Session["PQ"] = data.pq;
                        return RedirectToAction("ADminHome", "Admin");
                    }
                    if (data.pq == "KHACH")
                    {
                        Session["id"] = data.id;
                        Session["PQ"] = data.pq;
                        return RedirectToAction("Index", "KhachHang");
                    }
                }

                ViewBag.error = "Đăng nhập thất bại!";
                return RedirectToAction("Login");
            }catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        //-------------------------------------------------------
        // register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            string a = form["PQ"];
            if (a == "KOL")
                return RedirectToAction("RegisterKOL");
            return RedirectToAction("RegisterKhach");
        }
        // register admin
        public ActionResult RegisterAdmin()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterAdmin(FormCollection form)
        {
            try
            {
                string id, pass, nickname, name, diachi, SDT, gmail;
                DateTime ngaysinh;
                // get data
                id = form["id"];
                pass = form["pass"];
                nickname = form["nickname"];
                name = form["name"];
                diachi = form["diachi"];
                SDT = form["SDT"];
                gmail = form["gmail"];
                string temp = form["ngaysinh"];
                ngaysinh = DateTime.Parse(temp);
                string linhvuc = form["linhvuc"];
                // create object KOL
                admin kol = new admin();
                kol.id = id;
                kol.pass = GetMD5(pass);
                kol.nickname = nickname;
                kol.ten = name;
                kol.diachi = diachi;
                kol.sdt = SDT;
                kol.gmail = gmail;
                kol.ngaysinh = ngaysinh;
                // create object Accout
                taikhoan tk = new taikhoan();
                tk.id = id;
                tk.pass = kol.pass;
                tk.pq = "ADMIN";
                // add object
                db.admins.Add(kol);
                db.taikhoans.Add(tk);
                // save db
                db.SaveChanges();
                // send gmail
                return RedirectToAction("AdminHome", "Admin");
            }catch(Exception ex)
            {
                return RedirectToAction("AdminHome", "Admin");
            }
        }
        //-------------------------------------------------------
        // register KOL
        public ActionResult RegisterKOL()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterKOL(FormCollection form)
        {
            try
            {
                string id, pass, nickname, name, linktiktok, diachi, SDT, gmail, gioithieu, avatar;
                DateTime ngaysinh;

                // get data
                id = form["id"];
                pass = form["pass"];
                nickname = form["nickname"];
                name = form["name"];
                linktiktok = form["linktiktok"].ToString();
                diachi = form["diachi"];
                SDT = form["SDT"];
                gmail = form["gmail"];
                
                string temp = form["ngaysinh"];
                ngaysinh = DateTime.Parse(temp);
                avatar = form["avatar"];
                string linhvuc = form["linhvuc"];
                // create object KOL
                KOL kol = new KOL();
                kol.id = id;
                kol.pass = GetMD5(pass);
                kol.nickname = nickname;
                kol.ten = name;
                kol.linktiktok = linktiktok;
                kol.diachi = diachi;
                kol.sdt = SDT;
               
                kol.gmail = gmail;
                kol.avatar = avatar;
                kol.ngaysinh = ngaysinh;
                kol.fltiktok = -1;
                kol.kolcore = -1;
                //kol.fltiktok = GetFlTiktok(linktiktok);
                // create object linhvuc kol 
                string[] lv = new string[6];
                for (int i = 0; i < 6; i++)
                {
                    lv[i] = "1";
                }
                if (linhvuc.Contains("AT") == true)
                    lv[1] = "AT";
                if (linhvuc.Contains("TT") == true)
                    lv[2] = "TT";
                if (linhvuc.Contains("AN") == true)
                    lv[3] = "AN";
                if (linhvuc.Contains("TM") == true)
                    lv[4] = "TM";
                if (linhvuc.Contains("TTr") == true)
                    lv[5] = "TTr";
                if (linhvuc.Contains("MP") == true)
                    lv[0] = "MP";
                List<linhvucKOL> lvkol = new List<linhvucKOL>();
                for (int i = 1; i < 6; i++)
                {
                    if (lv[i] != "1")
                    {
                        linhvucKOL temp2 = new linhvucKOL();
                        temp2.IdKOL = id;
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
                // create object Accout
                taikhoan tk = new taikhoan();
                tk.id = id;
                tk.pass = kol.pass;
                tk.pq = "KOL";
                // add object
                db.KOLs.Add(kol);
                db.linhvucKOLs.AddRange(lvkol);
                db.taikhoans.Add(tk);
                // save db
                db.SaveChanges();
                // send gmail
                string sub = "Thông tin đăng kí tài khoản KOL";
                string body = "Cảm ơn bạn đã tin tưởng và đăng kí sử dụng dịch vụ của chúng tôi!\n Thông tin tài khoản của bạn:\nTên đăng nhập: " + id + "\nMật khẩu: " + pass;
                SendGmail sendgmail = new SendGmail();
                sendgmail.send(gmail, sub, body);
                return RedirectToAction("Login");
            }catch(Exception ex)
            {
                return View();
            }
        }
        //-------------------------------------------------------
        // register Khach
        public ActionResult RegisterKhach()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterKhach(FormCollection form)
        {
            try
            {
                string id, pass, nickname, name, diachi, SDT, gmail, avatar;
                DateTime ngaysinh;

                // get data
                id = form["id"];
                pass = form["pass"];
                nickname = form["nickname"];
                name = form["name"];
                diachi = form["diachi"];
                SDT = form["SDT"];
                gmail = form["gmail"];
                string temp = form["ngaysinh"];
                ngaysinh = DateTime.Parse(temp);
                avatar = form["avatar"];
                string linhvuc = form["linhvuc"];

                khach kol = new khach();
                kol.id = id;
                kol.pass = GetMD5(pass);
                kol.nickname = nickname;
                kol.ten = name;
                kol.diachi = diachi;
                kol.sdt = SDT;
                kol.gmail = gmail;
                kol.avatar = avatar;
                kol.ngaysinh = ngaysinh;

                // create object linhvuc kol 
                string[] lv = new string[6];
                for (int i = 0; i < 6; i++)
                {
                    lv[i] = "1";
                }
                if (linhvuc.Contains("AT") == true)
                    lv[1] = "AT";
                if (linhvuc.Contains("TT") == true)
                    lv[2] = "TT";
                if (linhvuc.Contains("AN") == true)
                    lv[3] = "AN";
                if (linhvuc.Contains("TM") == true)
                    lv[4] = "TM";
                if (linhvuc.Contains("TTr") == true)
                    lv[5] = "TTr";
                if (linhvuc.Contains("MP") == true)
                    lv[0] = "MP";
                List<linhvucKhach> lvkol = new List<linhvucKhach>();
                for (int i = 1; i < 6; i++)
                {
                    if (lv[i] != "1")
                    {
                        linhvucKhach temp2 = new linhvucKhach();
                        temp2.idkhach = id;
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
                // create object Accout
                taikhoan tk = new taikhoan();
                tk.id = id;
                tk.pass = kol.pass;
                tk.pq = "KOL";
                // add object
                db.khaches.Add(kol);
                db.linhvucKhaches.AddRange(lvkol);
                db.taikhoans.Add(tk);
                // save db
                db.SaveChanges();
                // send gmail
                string sub = "Thông tin đăng kí tài khoản KOL";
                string body = "Cảm ơn bạn đã tin tưởng và đăng kí sử dụng dịch vụ của chúng tôi!\n Thông tin tài khoản của bạn:\nTên đăng nhập: " + id + "\nMật khẩu: " + pass;
                SendGmail sendgmail = new SendGmail();
                sendgmail.send(gmail, sub, body);
                return RedirectToAction("Login");
            }catch(Exception ex)
            {
                return View();
            }
        }
        //-------------------------------------------------------
        // logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        //-------------------------------------------------------
        // trand MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        // Get fowllow
        public static int GetFlTiktok(string link)
        {
            int ketqua = -1;
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  
            };
            HtmlDocument document = htmlWeb.Load(link);
          

            if (document != null)
            {
                var fl1 = document.DocumentNode.SelectSingleNode("//div[@id='app']/div[2]/div[2]/div/div/h2/div[2]/strong");
                
                var fl = fl1.InnerText;
                string s = "";
                if (fl != null)
                {
                    int cham = 0;
                    foreach (char c in fl)
                    {
                        if (c >= '0' && c <= '9')
                            s += c;
                        if (c == '.')
                            cham++;
                        if (c == 'k' || c == 'K')
                            s += "000";
                    }
                    ketqua = Convert.ToInt32(s);
                    if (cham != 0)
                    {
                        ketqua = ketqua / 10;
                    }
                }
            }
            

            return ketqua;
        }
        public ActionResult Quenmk()
        {
            return View();
        }
    }
}