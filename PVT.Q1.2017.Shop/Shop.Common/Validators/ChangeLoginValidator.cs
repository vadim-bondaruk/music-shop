namespace Shop.Common.Validators
{
    using FluentValidation;
    using Infrastructure;
    using System.Web.Mvc;
    using ViewModels;

    /// <summary>
    /// 
    /// </summary>
    public class ChangeLoginValidator : AbstractValidator<ChangeLoginViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public ChangeLoginValidator()
        {
            var validator = DependencyResolver.Current.GetService<IUserValidator>();

            RuleFor(u => u.Login).NotEmpty()
                .WithMessage("Поле обязательно должно быть заполнено");
            RuleFor(u => u.Login).Matches("^[a-zA-Z0-9_.-]*$")
                .WithMessage("Только буквы латинского алфавита, цифры и знак подчеркивания");
            RuleFor(u => u.Login).Length(4, 50)
               .WithMessage("Логин должен содержать не менее 4 символов");
            RuleFor(u => u.Login).Must(l => !validator.IsUserExist(l))
                .WithMessage("Пользователь с таким логином уже существует");
        }
    }
}
