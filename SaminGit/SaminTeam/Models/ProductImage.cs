using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaminProject.Models
{
    public class ProductImage
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Image { get; set; }

        [NotMapped]
        public string Base64
        {
            get
            {
                if (Image != null)
                    return Convert.ToBase64String(Image);
                else
                    return string.Empty;
            }
        }

        public virtual Product Product { get; set; }

        [ForeignKey("Product")]
        public int? ProductID { get; set; }

    }
}