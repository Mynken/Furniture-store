using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MebleShop.Models.Entities.OurServices
{
    public class FileServiceDetail
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}