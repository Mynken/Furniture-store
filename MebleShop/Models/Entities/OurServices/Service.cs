using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MebleShop.Models.Entities.OurServices
{
    public class Service
    {
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public virtual ICollection<FileServiceDetail> FileServiceDetails { get; set; }
    }
}