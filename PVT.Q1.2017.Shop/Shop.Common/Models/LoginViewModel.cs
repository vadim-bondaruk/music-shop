namespace Shop.Common.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "Электронная почта или логин")]
        public string UserIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}