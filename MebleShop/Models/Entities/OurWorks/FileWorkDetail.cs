using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MebleShop.Models.Entities.OurWorks
{
    public class FileWorkDetail
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int WorkId { get; set; }
        public virtual Work Work { get; set; }
    }
}