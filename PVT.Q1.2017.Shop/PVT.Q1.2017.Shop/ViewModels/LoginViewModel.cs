namespace PVT.Q1._2017.Shop.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Loging view model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Remember me
        /// </summary>
        [Display(Name = "Запомнить")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Return url
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}