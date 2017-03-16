namespace Shop.Common.Validators
{
    using FluentValidation;
    using PVT.Q1._2017.Shop.ViewModels;

    /// <summary>
    /// The user validator
    /// </summary>
    public class UserValidator : AbstractValidator<UserViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator"/> class.
        /// </summary>
        public UserValidator()
        {
            RuleFor(u => u.Login).NotEmpty()
                .WithMessage("Поле должно быть заполнено обязательно");
            RuleFor(u => u.Login).Matches("^[a-zA-Z0-9_.-]*$")
                .WithMessage("Только буквы латинского алфавита, цифры и знак подчеркивания");

            RuleFor(u => u.Password).NotEmpty()
                .WithMessage("Поле должно быть заполнено обязательно");
            RuleFor(u => u.Password)
                .Matches(@"^(?=.*\w)(?=.*\d)(?=.*[!-*]).[^\s]*$")
                .WithMessage("Пароль должен содержать символы латинского алфавита, цифры и спецсимволы");
            RuleFor(u => u.Password).Length(7, 50)
                .WithMessage("Пароль должен содержать не менее 7 символов");

            RuleFor(u => u.ConfirmPassword)
                    .Equal(u => u.Password)
                    .WithMessage("Пароли не совпадают");

            RuleFor(u => u.Email).EmailAddress()
                .WithMessage("Адрес введен некорректно");
            RuleFor(u => u.Email).NotEmpty()
                .WithMessage("Поле должно быть заполнено обязательно");

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$")
                .WithMessage("Некорректный номер");
        }
    }
}