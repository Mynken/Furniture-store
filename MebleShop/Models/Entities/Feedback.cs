using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MebleShop.Models.Entities
{
    public class Feedback
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int FeedId { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите фамилию")]
        [DisplayName("Фамилия")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите номер телефона")]
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        [DisplayName("Описание")]
        public string Details { get; set; }
        public bool IsRead { get; set; } = false;

    }
}