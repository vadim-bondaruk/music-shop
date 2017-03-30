namespace PVT.Q1._2017.Shop.App_Start
{
    using Ninject.Modules;

    /// <summary>
    /// 
    /// </summary>
    public class FluentValidationNinjectModule : NinjectModule
    {
        /// <summary>
        /// Load Fluent Validation dependency injection bindings
        /// </summary>
        public override void Load()
        {
            //Bind<IValidator<UserViewModel>>().To<UserRegistrationValidator>();
            //Bind<IRepository<User>>().To<UserRepository>();
            //Bind<DbContext>().To<ShopContext>();
            //Bind<IValidator<LoginViewModel>>().To<LoginViewValidator>();
            //Bind<IValidator<UserPersonalViewModel>>().To<UserPersonalValidator>();
            //Bind<IValidator<ForgotPasswordViewModel>>().To<ForgotPassworValidator>();
            //Bind<IValidator<ChangePasswordViewModel>>().To<ChangePasswordValidator>();
        }
    }
}