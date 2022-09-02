using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiKOL.Models.Common
{
    public class KiemTra
    {
        public static int KiemTraHoa(string s)
        {
            int dem = 0;
            char[] chuoi = new char[s.Length];
            int p = 0;
            foreach (char c in s)
            {
                chuoi[p] = c;
                p++;
            }
            for (int i = 0; i < chuoi.Length; i++)
            {
                if (chuoi[i] >= 'A' && chuoi[i] <= 'Z')
                    dem++;
            }
            if (dem == 0)
                return 0;
            return 1;
        }
        public static int KiemTraThuong(string s)
        {
            int dem = 0;
            char[] chuoi = new char[s.Length];
            int p = 0;
            foreach(char c in s)
            {
                chuoi[p] = c;
                p++;
            }
            for (int i = 0; i < chuoi.Length; i++)
            {
                if (chuoi[i] >= 'a' && chuoi[i] <= 'z')
                    dem++;
            }
            if (dem == 0)
                return 0;
            return 1;
        }

        public static int KiemTraSo(string s)
        {
            int dem = 0;
            char[] chuoi = new char[s.Length];
            int p = 0;
            foreach (char c in s)
            {
                chuoi[p] = c;
                p++;
            }
            for (int i = 0; i < chuoi.Length; i++)
            {
                if (chuoi[i] >= '0' && chuoi[i] <= '9')
                    dem++;
            }
            if (dem == 0)
                return 0;
            return 1;
        }

        public static int KiemTraSDT(string s)
        {
            decimal kt;
            try
            {
                kt = Convert.ToDecimal(s);
                if (s.Length < 9 || s.Length > 12)
                    return 0;
                return 1;
            }catch(Exception ex)
            {
                return 0;
            }
        }

        public static int KiemTraKTDB(string s)
        {
            int dem = 0;
            char[] chuoi = new char[s.Length];
            int p = 0;
            foreach (char c in s)
            {
                chuoi[p] = c;
                p++;
            }
            for (int i = 0; i < chuoi.Length; i++)
            {
                if ((chuoi[i] >= 'A' && chuoi[i] <= 'Z') || (chuoi[i] >= 'a' && chuoi[i] <= 'z') || (chuoi[i] >= '0' && chuoi[i] <= '9'))
                    dem++;
            }
            if (dem == s.Length)
                return 0;
            return 1;
        }

        
        public static string KiemTraPass(string pass)
        {
            string loi = "";
            if(pass== "" || pass == null)
            {
                loi += "Mật khẩu không được để trống\n";
            }
            else
            {
                if(pass.Length < 8 )
                {
                    loi += "Mật khẩu phải từ 8 kí tự trởi lên\n";
                }
                else
                {
                    loi += "Mật khẩu phải bao gồm ";
                    if (KiemTraHoa(pass) == 0)
                    {
                        loi += "kí tự hoa\n";
                    }
                    if (KiemTraThuong(pass) == 0)
                    {
                        loi += "kí tự thường\n";
                    }
                    if (KiemTraSDT(pass) == 0)
                    {
                        loi += "kí tự số\n";
                    }
                    if (KiemTraKTDB(pass) == 0)
                    {
                        loi += "kí tự đặc biệt\n";
                    }

                }
            }
            return loi;
        }
    }
}