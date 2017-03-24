﻿namespace Shop.Common.Validators
{
    using FluentValidation;
    using PVT.Q1._2017.Shop.ViewModels;

    /// <summary>
    /// 
    /// </summary>
    public class LoginViewValidator : AbstractValidator<LoginViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginViewValidator()
        {
            RuleFor(u => u.UserIdentity).NotEmpty()
                .WithMessage("Поле не должно быть пустым");

            RuleFor(u => u.Password).NotEmpty()
                .WithMessage("Поле не должно быть пустым");
        }
    }
}
