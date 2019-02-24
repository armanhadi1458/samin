using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SaminProject.Library
{
    public class Utility
    {
        public static string GetShamsiDate(DateTime dt)
        {
            try
            {
                string year = "", month = "", day = "";
                PersianCalendar persianCalendar = new PersianCalendar();
                year = persianCalendar.GetYear(dt).ToString();
                month = persianCalendar.GetMonth(dt).ToString();
                if (month.Length == 1)
                    month = "0" + month;
                day = persianCalendar.GetDayOfMonth(dt).ToString();
                if (day.Length == 1)
                    day = "0" + day;
                return (year + "/" + month + "/" + day).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}