using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MebleShop.Models.Entities.Comment
{
    public class Coment
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ComentId { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите номер телефона")]
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        [DisplayName("Описание")]
        public string Details { get; set; }

        public DateTime TimeCreated { get; set; }
    }
}