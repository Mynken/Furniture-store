using MebleShop.Models.Entities.OurWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MebleShop.Models.Entities.OurWorks
{
    public class Work
    {
        public int WorkId { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public virtual ICollection<FileWorkDetail> FileWorkDetails { get; set; }
    }
}