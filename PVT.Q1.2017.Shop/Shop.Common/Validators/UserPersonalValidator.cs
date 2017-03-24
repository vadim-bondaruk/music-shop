namespace Shop.Common.Validators
{
    using System;
    using FluentValidation;
    using PVT.Q1._2017.Shop.ViewModels;

    /// <summary>
    /// Defines rules for user registration
    /// </summary>
    public class UserPersonalValidator : AbstractValidator<UserPersonalViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserPersonalValidator()
        {            
            RuleFor(u => u.BirthDate).InclusiveBetween(DateTime.Today.AddYears(-80), DateTime.Today)
                .WithMessage("Дата рождения выбрана неверно");

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$")
                .WithMessage("Некорректный номер");
        }
    }
}
