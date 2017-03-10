namespace Shop.Common.Validators
{
    using FluentValidation;
    using Models;

    /// <summary>
    /// The currency validator
    /// </summary>
    public class CurrencyValidator : AbstractValidator<Currency>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyValidator"/> class.
        /// </summary>
        public CurrencyValidator()
        {
            this.RuleFor(c => c.ShortName).Length(3).Matches("^[A-Z]&");
            this.RuleFor(c => c.Code).GreaterThanOrEqualTo(100).LessThanOrEqualTo(999);
        }
    }
}