namespace PVT.Q1._2017.Shop.App_Start
{
    using System;
    using FluentValidation;
    using Ninject;

    /// <summary>
    /// Custom validator factory for implementing dependency injction 
    /// </summary>
    public class CustomValidatorFactory : ValidatorFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IKernel _kernel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        public CustomValidatorFactory(IKernel kernel)
        {
            this._kernel = kernel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatorType"></param>
        /// <returns></returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            return this._kernel.Get(validatorType) as IValidator;
        }
    }    
}