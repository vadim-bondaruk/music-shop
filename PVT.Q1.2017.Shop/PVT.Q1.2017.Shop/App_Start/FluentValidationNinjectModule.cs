namespace PVT.Q1._2017.Shop.App_Start
{
    using System.Data.Entity;
    using FluentValidation;
    using Ninject.Modules;
    using PVT.Q1._2017.Shop.ViewModels;
    using global::Shop.Common.Models;
    using global::Shop.Common.Validators;
    using global::Shop.DAL.Context;
    using global::Shop.DAL.Repositories;
    using global::Shop.Infrastructure.Repositories;

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
            Bind<IValidator<UserViewModel>>().To<UserRegistrationValidator>();
            Bind<IRepository<User>>().To<UserRepository>();
            Bind<DbContext>().To<ShopContext>();
        }
    }
}