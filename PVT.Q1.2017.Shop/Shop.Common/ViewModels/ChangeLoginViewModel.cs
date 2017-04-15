namespace Shop.Common.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common.Validators;

    /// <summary>
    /// 
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(ChangeLoginValidator))]
    public class ChangeLoginViewModel
    {

        /// <summary>
        /// Users login (nickname)
        /// </summary>
        [Display(Name = "Логин")]
        [Remote("IsLoginUnique", "Account", "User", ErrorMessage = "Такой логин уже существует")]
        public string Login { get; set; }
       
    }
}