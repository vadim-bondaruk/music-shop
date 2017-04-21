using Shop.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserEditView
    {
        public int Id { get; set; }
        /// <summary>
        /// Users first name
        /// </summary>
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Users last name
        /// </summary>
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Users login (nickname)
        /// </summary>
        [Display(Name = "Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Users e-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Пол")]
        public string Sex { get; set; }

        /// <summary>
        /// Users birth date
        /// </summary>
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Users role in this shop
        /// </summary>
        [Display(Name = "Роль")]
        public UserRoles UserRole { get; set; }

        /// <summary>
        /// Users country
        /// </summary>
        [Display(Name = "Страна")]
        public string Country { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Verification of email confirmation
        /// </summary>
        [Display(Name = "Подтверждение Email")]
        public bool ConfirmedEmail { get; set; }
    }
}
