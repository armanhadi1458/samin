using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
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

        public static string GetPersianDateString(DateTime dt)
        {
            try
            {
                PersianCalendar PerCal = new PersianCalendar();
                string str="";
                str += " " + GetNameDay(PerCal.GetDayOfWeek(dt.Date).ToString());
                str += " " + PerCal.GetDayOfMonth(dt.Date).ToString();
                str += " " + GetNameMonth(PerCal.GetMonth(dt.Date));
                str += " " + PerCal.GetYear(dt.Date).ToString();
                return str;
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }

        public static string GetNameMonth(int num)
        {
            if (num == 1)
                return "فروردین";
            else if (num == 2)
                return "اردیبهشت";
            else if (num == 3)
                return "خرداد";
            else if (num == 4)
                return "تیر";
            else if (num == 5)
                return "مرداد";
            else if (num == 6)
                return "شهریور";
            else if (num == 7)
                return "مهر";
            else if (num == 8)
                return "آبان";
            else if (num == 9)
                return "آذر";
            else if (num == 10)
                return "دی";
            else if (num == 11)
                return "بهمن";
            else if (num == 12)
                return "اسفند";
            else
                return "";
        }

        /// <summary>
        /// بدست آوردن این هفته به فارسی
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string GetNameDay(string Name)
        {
            if (Name.Equals("Saturday"))
                return "شنبه";
            else if (Name.Equals("Sunday"))
                return "یک شنبه";
            else if (Name.Equals("Monday"))
                return "دو شنبه";
            else if (Name.Equals("Tusday"))
                return "سه شنبه";
            else if (Name.Equals("Wednesday"))
                return "چهار شنبه";
            else if (Name.Equals("Thursday"))
                return "پنج شنبه";
            else if (Name.Equals("Friday"))
                return "جمعه";
            else
                return "";

        }


        public static byte[] GetResizedImage(HttpPostedFileBase img, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            Image image = Image.FromStream(img.InputStream, true, true);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                destImage.Save(ms, GetImageFormat(img.FileName));
                return ms.ToArray();
            }
        }

        public static ImageFormat GetImageFormat(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }
    }
}