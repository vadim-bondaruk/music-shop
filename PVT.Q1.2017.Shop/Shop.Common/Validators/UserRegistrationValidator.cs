namespace Shop.Common.Validators
{
    using System;
    using System.Web.Mvc;
    using FluentValidation;
    using Infrastructure;
    using ViewModels;

    /// <summary>
    /// Defines rules for user registration
    /// </summary>
    public class UserRegistrationValidator : AbstractValidator<UserViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserRegistrationValidator()
        {
            var validator = DependencyResolver.Current.GetService<IUserValidator>();

            RuleFor(u => u.FirstName).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.FirstName).Matches("^[a-zA-Zа-яА-Я_.-]*$")
                .WithMessage("Используйте только буквы");

            RuleFor(u => u.LastName).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.LastName).Matches("^[a-zA-Zа-яА-Я_.-]*$")
                .WithMessage("Используйте только буквы");

            RuleFor(u => u.Login).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.Login).Matches("^[a-zA-Z0-9_.-]*$")
                .WithMessage("Только буквы латинского алфавита, цифры и знак подчеркивания");
            RuleFor(u => u.Login).Must(login => !validator.IsUserExist(login))
                .WithMessage("Пользователь с таким логином уже существует");

            RuleFor(u => u.Password).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.Password)
                .Matches(@"^(?=.*\w)(?=.*\d)(?=.*[!-*]).[^\s]*$")
                .WithMessage("Пароль должен содержать символы латинского алфавита, цифры и спецсимволы");
            RuleFor(u => u.Password).Length(7, 50)
                .WithMessage("Пароль должен содержать не менее 7 символов");

            RuleFor(u => u.ConfirmPassword)
                    .Equal(u => u.Password)
                    .WithMessage("Пароли не совпадают. Повторите попытку");

            RuleFor(u => u.Email).EmailAddress()
                .WithMessage("Адрес введен некорректно");
            RuleFor(u => u.Email).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.Email).Must(email => !validator.IsUserExist(email))
                .WithMessage("Пользователь с таким адресом электронной почты уже существект");

            RuleFor(u => u.BirthDate).ExclusiveBetween(DateTime.Today.AddYears(-80), DateTime.Today.AddYears(-5))
                .WithMessage("Дата рождения выбрана некорректно");

            RuleFor(u => u.Country).Matches("^[a-zA-Zа-яА-Я_.-]*$")
                .WithMessage("Используйте только буквы");

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$")
                .WithMessage("Некорректный номер");
        }
    }
}
