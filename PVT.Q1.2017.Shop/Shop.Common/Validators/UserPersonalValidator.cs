namespace Shop.Common.Validators
{
    using System;
    using FluentValidation;
    using PVT.Q1._2017.Shop.ViewModels;

    /// <summary>
    /// Defines rules for UserPersonalViewModel (change personal data in account)
    /// </summary>
    public class UserPersonalValidator : AbstractValidator<UserPersonalViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserPersonalValidator()
        {
            RuleFor(u => u.FirstName).Matches("^[a-zA-Zа-яА-Я_.-]*$")
               .WithMessage("Используйте только буквы и знак подчеркивания");

            RuleFor(u => u.LastName).Matches("^[a-zA-Zа-яА-Я_.-]*$")
                .WithMessage("Используйте только буквы и знак подчеркивания");

            RuleFor(u => u.BirthDate).Must(c => c <= DateTime.Now.AddYears(-5))
                .WithMessage("Дата рождения выбрана некорректно (вам должно быть минимум 5 лет))");

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){12,14}(\s*)?$")
                .WithMessage("Некорректный номер");
        }
    }
}
