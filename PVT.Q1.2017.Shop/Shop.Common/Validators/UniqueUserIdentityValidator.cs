namespace Shop.Common.Validators
{
    using System.Linq;
    using FluentValidation.Validators;
    using Shop.Common.Models;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    /// Custom property validator, check is user already exist
    /// </summary>
    public class UniqueUserIdentityValidator : PropertyValidator
    {
        /// <summary>
        /// Error message if user exist
        /// </summary>
        private readonly string _errorMessage;

        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="errorMesage"></param>
        public UniqueUserIdentityValidator(IRepository<User> userRepository, string errorMesage) : base(errorMesage)
        {
            this._userRepository = userRepository;
            this._errorMessage = errorMesage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var userIdentity = context.PropertyValue as string;
            User user = null;

            if (userIdentity != null)
            {
                user = userIdentity.Contains("@") ? this._userRepository?.GetAll(u => u.Email == userIdentity)?.FirstOrDefault()
                                                  : this._userRepository?.GetAll(u => u.Login == userIdentity)?.FirstOrDefault();
            }

            return user == null;
        }
    }
}
