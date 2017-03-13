namespace Shop.Common.Validators
{
    using System;
    using System.Web.Mvc;

    using FluentValidation;
    using FluentValidation.Mvc;
    using FluentValidation.Results;

    /// <summary>
    /// </summary>
    public class Interceptor : IValidatorInterceptor
    {
        /// <summary>
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="validationContext">
        /// The validation context.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// ***
        /// </exception>
        public ValidationResult AfterMvcValidation(
            ControllerContext controllerContext,
            ValidationContext validationContext,
            ValidationResult result)
        {
            result = new ValidationResult() { Errors = { null } };
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="validationContext">
        /// The validation context.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// ***
        /// </exception>
        public ValidationContext BeforeMvcValidation(
            ControllerContext controllerContext,
            ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}