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
            RuleFor(u => u.FirstName).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.FirstName).Matches("^[a-zA-Zа-яА-Я_.-]*$")
               .WithMessage("Используйте только буквы");

            RuleFor(u => u.LastName).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.LastName).Matches("^[a-zA-Zа-яА-Я_.-]*$")
                .WithMessage("Используйте только буквы");

            RuleFor(u => u.BirthDate).ExclusiveBetween(DateTime.Today.AddYears(-80), DateTime.Today.AddYears(-5))
                .WithMessage("Дата рождения выбрана некорректно");

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$")
                .WithMessage("Некорректный номер");
        }
    }
}
