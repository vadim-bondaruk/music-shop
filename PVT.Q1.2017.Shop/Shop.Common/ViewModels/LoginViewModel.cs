namespace Shop.Common.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// 
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(LoginViewValidator))]
    public class LoginViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Электронная почта или логин")]
        public string UserIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
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