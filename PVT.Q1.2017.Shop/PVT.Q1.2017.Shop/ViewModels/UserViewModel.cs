namespace PVT.Q1._2017.Shop.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Users last name
        /// </summary>
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено обязательно")]

        /// <summary>
        /// Users login (nickname)
        /// </summary>
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено обязательно")]

        [DataType(DataType.Password)]

        /// <summary>
        /// Users password
        /// </summary>
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]

        [DataType(DataType.Password)]

        /// <summary>
        /// Confirm password
        /// </summary>
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]

        /// <summary>
        /// Users e-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Users sex
        /// </summary>
        public string Sex { get; set; }

        [DataType(DataType.Date)]

        /// <summary>
        /// Users birth date
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Users country
        /// </summary>
        public string Country { get; set; }

        [DataType(DataType.PhoneNumber)]

        /// <summary>
        /// Users phone
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}