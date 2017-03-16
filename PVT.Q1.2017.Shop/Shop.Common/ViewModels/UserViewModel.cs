namespace PVT.Q1._2017.Shop.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FluentValidation;
    using global::Shop.Common.Validators;

    /// <summary>
    /// 
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(UserRegistrationValidator))]
    public class UserViewModel
    {
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
        /// Users login (nickname)
        /// </summary>
        [Display(Name = "Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Users password
        /// </summary>
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Users e-mail
        /// </summary>
        [Display(Name = "Адрес почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Users sex
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
        /// Users country
        /// </summary>
        [Display(Name = "Страна")]
        public string Country { get; set; }

        /// <summary>
        /// Users phone
        /// </summary>
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}