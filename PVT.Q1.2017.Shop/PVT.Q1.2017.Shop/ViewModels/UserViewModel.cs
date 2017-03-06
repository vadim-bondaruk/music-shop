namespace PVT.Q1._2017.Shop.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class UserViewModel
    {
        [Display(Name = "Фамилия")]

        /// <summary>
        /// Users first name
        /// </summary>
        public string FirstName { get; set; }

        [Display(Name = "Имя")]

        /// <summary>
        /// Users last name
        /// </summary>
        public string LastName { get; set; }

        [Display(Name = "Логин")]

        [Required(ErrorMessage = "Поле должно быть заполнено обязательно")]

        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "Только буквы латинского алфавита, цифры и знак подчеркивания")]

        /// <summary>
        /// Users login (nickname)
        /// </summary>
        public string Login { get; set; }

        [Display(Name = "Пароль")]

        [Required(ErrorMessage = "Поле должно быть заполнено обязательно")]

        [DataType(DataType.Password)]

        [StringLength(20, ErrorMessage = "Пароль должен содержать не менее {2} символов.", MinimumLength = 7)]

        /// <summary>
        /// Users password
        /// </summary>
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]

        [DataType(DataType.Password)]

        /// <summary>
        /// Confirm password
        /// </summary>
        public string ConfirmPassword { get; set; }

        [Display(Name = "Адрес почты")]

        [DataType(DataType.EmailAddress)]

        [Required(ErrorMessage = "Поле должно быть заполнено обязательно")]

        /// <summary>
        /// Users e-mail
        /// </summary>
        public string Email { get; set; }

        [Display(Name = "Пол")]

        /// <summary>
        /// Users sex
        /// </summary>
        public string Sex { get; set; }

        [Display(Name = "Дата рождения")]

        [DataType(DataType.Date)]

        /// <summary>
        /// Users birth date
        /// </summary>
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Страна")]

        /// <summary>
        /// Users country
        /// </summary>
        public string Country { get; set; }

        [Display(Name = "Номер телефона")]

        [DataType(DataType.PhoneNumber)]

        [RegularExpression(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$", ErrorMessage = "Некорректный номер")]

        /// <summary>
        /// Users phone
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}