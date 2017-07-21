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

        [Required(ErrorMessage = "Please write the NAME")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please write the SURNAME")]
        [DisplayName("Фамилия")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Please write the EMAIL")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please write the PHONENUMBER")]
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please write DETAILS")]
        [DisplayName("Описание")]
        public string Details { get; set; }

    }
}