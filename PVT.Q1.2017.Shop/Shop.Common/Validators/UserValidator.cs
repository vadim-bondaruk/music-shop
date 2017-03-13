namespace Shop.Common.Validators
{
    using FluentValidation;
    using Models;

    /// <summary>
    /// The user validator
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator"/> class.
        /// </summary>
        public UserValidator()
        {
            RuleFor(u => u.IdentityKey).NotNull().NotEmpty().Matches(@"^\S+$");
            RuleFor(u => u.CurrencyId).GreaterThan(0);
            RuleFor(u => u.PriceLevelId).GreaterThan(0);
        }
    }
}