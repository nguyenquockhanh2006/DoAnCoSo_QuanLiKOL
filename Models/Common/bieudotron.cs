using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLiKOL.Models;
namespace QuanLiKOL.Models.Common
{
    public class bieudotron
    {
        KOLDbContext db = new KOLDbContext();
        public double amnhac;
        public double amthuc;
        public double mypham;
        public double thuongmaidt;
        public double thethao;
        public double thoitrang;

        public int slamnhac;
        public int slamthuc;
        public int slmypham;
        public int slthuongmaidt;
        public int slthethao;
        public int slthoitrang;

        public string mang1 = "Music";
        public string mang2 = "Culinary";
        public string mang3 = "Cosmetics";
        public string mang4 = "Ecommerce";
        public string mang5 = "Sport";
        public string mang6 = "Fashion";
        public bieudotron(DateTime tu, DateTime den)
        {
            int an = (from c in db.hopdongs where c.linhvuc == "AN" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int at = (from c in db.hopdongs where c.linhvuc == "AT" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int mp = (from c in db.hopdongs where c.linhvuc == "MP" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int tmdt = (from c in db.hopdongs where c.linhvuc == "TM" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int tt = (from c in db.hopdongs where c.linhvuc == "TT" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int ttr = (from c in db.hopdongs where c.linhvuc == "TTr" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int tong = an + at + mp + tmdt + tt + ttr;
            if (tong == 0)
            {
                this.amnhac = 0;
                this.amthuc = 0;
                this.mypham = 0;
                this.thuongmaidt = 0;
                this.thethao = 0;
                this.thoitrang = 0;

                this.slamnhac = 0;
                this.slamthuc = 0;
                this.slmypham = 0;
                this.slthuongmaidt = 0;
                this.slthethao = 0;
                this.slthoitrang = 0;
            }
            else
            {
                if (an != 0)
                {
                    this.amnhac = an * 100 / tong;
                    this.slamnhac = an;
                }

                else
                {
                    this.amnhac = 0;
                    this.slamnhac = 0;
                }
                if (at != 0)
                {
                    this.amthuc = at * 100 / tong;
                    this.slamthuc = at;
                }
                else
                {
                    this.amthuc = 0;
                    this.slamthuc = 0;
                }

                if (mp != 0)
                {
                    this.mypham = mp * 100 / tong;
                    this.slmypham = mp;
                }
                else
                {
                    this.mypham = 0;
                    this.slmypham = 0;
                }
                if (tmdt != 0)
                {
                    this.thuongmaidt = tmdt * 100 / tong;
                    this.slthuongmaidt = tmdt;
                }
                else
                {
                    this.thuongmaidt = 0;
                    this.slthuongmaidt = 0;
                }

                if (tt != 0)
                {
                    this.thethao = tt * 100 / tong;
                    this.slthethao = tt;
                }
                else
                {
                    this.thethao = 0;
                    this.slthethao = 0;
                }

                if (ttr != 0)
                {
                    this.thoitrang = ttr * 100 / tong;
                    this.slthoitrang = ttr;
                }
                else
                {
                    this.thoitrang = 0;
                    this.slthoitrang = 0;
                }

            }
        }
        public bieudotron(string nam)
        {
            int n = Convert.ToInt32(nam);
            int an = (from c in db.hopdongs where c.linhvuc == "AN" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int at = (from c in db.hopdongs where c.linhvuc == "AT" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int mp = (from c in db.hopdongs where c.linhvuc == "MP" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int tmdt = (from c in db.hopdongs where c.linhvuc == "TM" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int tt = (from c in db.hopdongs where c.linhvuc == "TT" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int ttr = (from c in db.hopdongs where c.linhvuc == "TTr" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList().Count();
            int tong = an + at + mp + tmdt + tt + ttr;
            if (tong == 0)
            {
                this.amnhac = 0;
                this.amthuc = 0;
                this.mypham = 0;
                this.thuongmaidt = 0;
                this.thethao = 0;
                this.thoitrang = 0;
            }
            else
            {
                if (an != 0)
                {
                    this.amnhac = an * 100 / tong;
                    this.slamnhac = an;
                }
                else
                {
                    this.amnhac = 0;
                    this.slamnhac = 0;
                }

                if (at != 0)
                {
                    this.amthuc = at * 100 / tong;
                    this.slamthuc = at;
                }
                else
                {
                    this.amthuc = 0;
                    this.slamthuc = 0;
                }

                if (mp != 0)
                {
                    this.mypham = mp * 100 / tong;
                    this.slmypham = mp;
                }
                else
                {
                    this.mypham = 0;
                    this.slmypham = 0;
                }

                if (tmdt != 0)
                {
                    this.thuongmaidt = tmdt * 100 / tong;
                    this.slthuongmaidt = tmdt;
                }
                else
                {
                    this.thuongmaidt = 0;
                    this.slthuongmaidt = 0;
                }

                if (tt != 0)
                {
                    this.thethao = tt * 100 / tong;
                    this.slthethao = tt;
                }
                else
                {
                    this.thethao = 0;
                    this.slthethao = 0;
                }

                if (ttr != 0)
                {
                    this.thoitrang = ttr * 100 / tong;
                    this.slthoitrang = ttr;
                }
                else
                {
                    this.thoitrang = 0;
                    this.slthoitrang = 0;
                }
            }
        }
        public bieudotron()
        {
            this.amnhac = 0;
            this.amthuc = 0;
            this.mypham = 0;
            this.thuongmaidt = 0;
            this.thethao = 0;
            this.thoitrang = 0;
        }

    }
}
