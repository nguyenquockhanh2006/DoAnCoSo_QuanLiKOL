using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLiKOL.Models;
namespace QuanLiKOL.Models.Common
{
    public class bieudocot
    {
        public long amnhac, amthuc, mypham, tmdt, thethao, thoitrang, max, nut;

        KOLDbContext db = new KOLDbContext();
        
        public bieudocot(DateTime tu, DateTime den)
        {
            this.amnhac = 0; this.amthuc = 0; this.mypham = 0; this.tmdt = 0; this.thethao = 0; this.thoitrang = 0;

            List<hopdong> an = (from c in db.hopdongs where c.linhvuc == "AN" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (an.Count > 0)
            {
                foreach (var c in an)
                    this.amnhac += (long)c.trigia;
            }

            List<hopdong> at = (from c in db.hopdongs where c.linhvuc == "AT" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (at.Count > 0)
            {
                foreach (var c in at)
                    this.amthuc += (long)c.trigia;
            }

            List<hopdong> mp = (from c in db.hopdongs where c.linhvuc == "MP" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (mp.Count > 0)
            {
                foreach (var c in mp)
                    this.mypham += (long)c.trigia;
            }

            List<hopdong> thmdt = (from c in db.hopdongs where c.linhvuc == "TM" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (thmdt.Count > 0)
            {
                foreach (var c in thmdt)
                    this.tmdt += (long)c.trigia;
            }

            List<hopdong> tt = (from c in db.hopdongs where c.linhvuc == "TT" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (tt.Count > 0)
            {
                foreach (var c in tt)
                    this.thethao += (long)c.trigia;
            }

            List<hopdong> ttr = (from c in db.hopdongs where c.linhvuc == "TTr" && c.ngaybatdau.Value >= tu && c.ngaybatdau.Value <= den && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (ttr.Count > 0)
            {
                foreach (var c in ttr)
                    this.thoitrang += (long)c.trigia;
            }

            this.max = timmax(amnhac, timmax(amthuc, timmax(tmdt, timmax(thethao, timmax(mypham, thoitrang))))) + 5000;
        }
        public bieudocot(string nam)
        {
            this.amnhac = 0; this.amthuc = 0; this.mypham = 0; this.tmdt = 0; this.thethao = 0; this.thoitrang = 0;

            List<hopdong> an = (from c in db.hopdongs where c.linhvuc == "AN" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if(an.Count > 0)
            {
                foreach (var c in an)
                    this.amnhac += (long)c.trigia;
            }

            List<hopdong> at = (from c in db.hopdongs where c.linhvuc == "AT" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (at.Count > 0)
            {
                foreach (var c in at)
                    this.amthuc += (long)c.trigia;
            }

            List<hopdong> mp = (from c in db.hopdongs where c.linhvuc == "MP" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (mp.Count > 0)
            {
                foreach (var c in an)
                    this.mypham += (long)c.trigia;
            }

            List<hopdong> thmdt = (from c in db.hopdongs where c.linhvuc == "TM" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (thmdt.Count > 0)
            {
                foreach (var c in an)
                    this.tmdt += (long)c.trigia;
            }

            List<hopdong> tt = (from c in db.hopdongs where c.linhvuc == "TT" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (tt.Count > 0)
            {
                foreach (var c in tt)
                    this.thethao += (long)c.trigia;
            }

            List<hopdong> ttr = (from c in db.hopdongs where c.linhvuc == "TTr" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (ttr.Count > 0)
            {
                foreach (var c in ttr)
                    this.thoitrang += (long)c.trigia;
            }

            this.max = timmax(amnhac, timmax(amthuc, timmax(tmdt, timmax(thethao, timmax(mypham, thoitrang)))))+5000;
            
        }
        public bieudocot(string nam, string id)
        {
            this.amnhac = 0; this.amthuc = 0; this.mypham = 0; this.tmdt = 0; this.thethao = 0; this.thoitrang = 0;

            List<hopdong> an = (from c in db.hopdongs where c.linhvuc == "AN" && c.ngaybatdau.Value.Year.ToString() == nam && c.KOL.id == id  && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (an.Count > 0)
            {
                foreach (var c in an)
                    this.amnhac += (long)c.trigia;
            }

            List<hopdong> at = (from c in db.hopdongs where c.linhvuc == "AT" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (at.Count > 0)
            {
                foreach (var c in at)
                    this.amthuc += (long)c.trigia;
            }

            List<hopdong> mp = (from c in db.hopdongs where c.linhvuc == "MP" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (mp.Count > 0)
            {
                foreach (var c in an)
                    this.mypham += (long)c.trigia;
            }

            List<hopdong> thmdt = (from c in db.hopdongs where c.linhvuc == "TM" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (thmdt.Count > 0)
            {
                foreach (var c in an)
                    this.tmdt += (long)c.trigia;
            }

            List<hopdong> tt = (from c in db.hopdongs where c.linhvuc == "TT" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (tt.Count > 0)
            {
                foreach (var c in tt)
                    this.thethao += (long)c.trigia;
            }

            List<hopdong> ttr = (from c in db.hopdongs where c.linhvuc == "TTr" && c.ngaybatdau.Value.Year.ToString() == nam && (c.trangthai == 4 || c.trangthai == 5) select c).ToList();
            if (ttr.Count > 0)
            {
                foreach (var c in ttr)
                    this.thoitrang += (long)c.trigia;
            }

            this.max = timmax(amnhac, timmax(amthuc, timmax(tmdt, timmax(thethao, timmax(mypham, thoitrang))))) + 5000;

        }
        public bieudocot()
        {
            this.amnhac = 0; this.amthuc = 0; this.mypham = 0; this.tmdt = 0; this.thethao = 0; this.thoitrang = 0;
        }

        public long timmax(long a, long b)
        {
            if (a > b)
                return a;
            return b;
        }

    }
}