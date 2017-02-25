// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the BaseController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop.Controllers
{
    using AutoMapper;

    /// <summary>
    /// </summary>
    public abstract class BaseController
    {
        /// <summary>
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        protected IMapper Mapper()
        {
            return null;
        }
    }
}