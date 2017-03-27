namespace Shop.Common.Validators
{
    using System;
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// Defines rules for user registration
    /// </summary>
    public class UserRegistrationValidator : AbstractValidator<UserViewModel>
    {

        /// <summary>
        /// 
        /// </summary>
        //private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        public UserRegistrationValidator()
        {
           // this._userService = new UserService();

            RuleFor(u => u.FirstName).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");

            RuleFor(u => u.LastName).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");

            RuleFor(u => u.Login).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.Login).Matches("^[a-zA-Z0-9_.-]*$")
                .WithMessage("Только буквы латинского алфавита, цифры и знак подчеркивания");
            //RuleFor(u => u.Login)
            //    .Must(login => !_userService.IsUserExist(login));
                //SetValidator(new UniqueUserIdentityValidator(this._userRepository, "Пользователь с таким логином уже существует"));

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
            //RuleFor(u => u.Email)
            //    .Must(email => !_userService.IsUserExist(email));
                //.SetValidator(new UniqueUserIdentityValidator(this._userRepository, "Пользователь с таким адресом электронной почты уже существует"));

            RuleFor(u => u.BirthDate).InclusiveBetween(DateTime.Today.AddYears(-80), DateTime.Today)
                .WithMessage("Дата рождения выбрана неверно");

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$")
                .WithMessage("Некорректный номер");
        }
    }
}
