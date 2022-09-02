using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLiKOL.Models;
namespace QuanLiKOL.Models.Common
{
    public class Connect
    {
        public KOL kol;
        public List<Anhduocdang> anh;
        public List<linhvuc> lv;
        public List<linhvucKOL> lvkol;

        KOLDbContext db = new KOLDbContext();
        public Connect()
        {
            //kol = db.KOLs.ToList();
            //anh = db.Anhduocdangs.Where(x => x.IdKOL == "mocmoc").ToList();
        }
    }
}