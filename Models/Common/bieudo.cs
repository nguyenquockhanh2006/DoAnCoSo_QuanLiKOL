using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiKOL.Models.Common
{
    public class bieudo
    {
        KOLDbContext db = new KOLDbContext();
        public bieudotron bdt;
        public bieudocot bdc;
        public List<hopdong> hd;
        public bieudo()
        {

            int nam = DateTime.Now.Year;
            hd = db.hopdongs.Where(x => x.ngaybatdau.Value.Year == nam && (x.trangthai == 4 || x.trangthai == 5)).ToList();

            bdt = new bieudotron();
            bdc = new bieudocot();
        }
        public bieudo(string nam)
        {
            int n = Convert.ToInt32(nam);
            hd = db.hopdongs.Where(x => x.ngaybatdau.Value.Year == n && (x.trangthai == 4 || x.trangthai == 5)).ToList();
            bdt = new bieudotron(nam);
            bdc = new bieudocot(nam);
        }
        public bieudo(DateTime tu, DateTime den)
        {
            hd = db.hopdongs.Where(x => x.ngaybatdau.Value >= tu && x.ngaybatdau <= den  && (x.trangthai == 4 || x.trangthai == 5)).ToList();
            bdt = new bieudotron(tu,den);
            bdc = new bieudocot(tu,den);
        }
    }
}