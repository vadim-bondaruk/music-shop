namespace Shop.Common.Validators
{
    using System;
    using FluentValidation;
    using ViewModels;
    using System.Web.Mvc;
    using Infrastructure;

    /// <summary>
    /// Defines rules for UserEditModel (change personal data in account)
    /// </summary>
    public class UserEditValidator : AbstractValidator<UserEditView>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserEditValidator()
        {
           
            RuleFor(u => u.FirstName).Matches("^[a-zA-Zа-яА-Я]*$")
               .WithMessage("Используйте только буквы");

            RuleFor(u => u.LastName).Matches("^[a-zA-Zа-яА-Я]*$")
                .WithMessage("Используйте только буквы");

            RuleFor(u => u.BirthDate).Must(c => c <= DateTime.Now.AddYears(-5))
                .WithMessage("Дата рождения выбрана некорректно (вам должно быть минимум 5 лет))");

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){12,14}(\s*)?$")
                .WithMessage("Некорректный номер");

        }
    }
}
