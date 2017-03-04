namespace Shop.BLL.Services
{
    using System;
    using Exceptions;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Models;
    using Shop.Infrastructure.Repositories;
    using Shop.Infrastructure.Services;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The model service.
    /// </summary>
    public abstract class Service<TRepository, TEntity> : IService<TEntity> where TEntity : BaseEntity, new()
                                                                            where TRepository : IRepository<TEntity>
    {
        #region Fields

        /// <summary>
        /// The factory.
        /// </summary>
        private readonly IFactory _factory;

        /// <summary>
        /// The models validator.
        /// </summary>
        private readonly IValidator<TEntity> _validator;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Service{TRepository, TEntity}"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">
        /// The model validator.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="factory"/> is null or <paramref name="validator"/> is null.
        /// </exception>
        protected Service(IFactory factory, IValidator<TEntity> validator)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this._factory = factory;
            this._validator = validator;
        }

        #endregion //Constructors

        #region Properties

        /// <summary>
        /// Gets the factory.
        /// </summary>
        protected IFactory Factory
        {
            get { return this._factory; }
        }

        /// <summary>
        /// Gets the validator.
        /// </summary>
        protected IValidator<TEntity> Validator
        {
            get { return this._validator; }
        }

        #endregion //Properties

        #region IService Members
        
        /// <summary>
        /// Registers the specified <paramref name="model"/> in the system.
        /// </summary>
        /// <param name="model">
        /// The model to register.
        /// </param>
        public virtual void Register(TEntity model)
        {
            this._validator.Validate(model);

            using (var repository = this._factory.Create<TRepository>())
            {
                repository.AddOrUpdate(model);
            }
        }

        /// <summary>
        /// Updates the specified <paramref name="model"/> in the system.
        /// </summary>
        /// <param name="model">
        /// The model to update.
        /// </param>
        public virtual void Update(TEntity model)
        {
            if (!this.IsRegistered(model))
            {
                this.OnUpdateEntityNotFoundException();
            }

            using (var repository = this._factory.Create<TRepository>())
            {
                repository.AddOrUpdate(model);
            }
        }

        /// <summary>
        /// Unregisters the specified <paramref name="model"/> from the system.
        /// </summary>
        /// <param name="model">
        /// The model to unregister.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="model"/> was unregistered successfully; otherwise <b>false</b>.
        /// </returns>
        public virtual bool Unregister(TEntity model)
        {
            if (!this.IsRegistered(model))
            {
                return false;
            }

            using (var repository = this._factory.Create<TRepository>())
            {
                repository.Delete(model);
                return true;
            }
        }

        /// <summary>
        /// Determines whether the specified <paramref name="model"/> is valid.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="model"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public virtual bool IsValid(TEntity model)
        {
            return this._validator.IsValid(model);
        }

        /// <summary>
        /// Determines whether the specified <paramref name="model"/> is registered
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="model"/> is registered; otherwise <b>false</b>.
        /// </returns>
        public virtual bool IsRegistered(TEntity model)
        {
            if (!this.IsValid(model))
            {
                return false;
            }

            return this.IsRegistered(model.Id);
        }

        /// <summary>
        /// Determines whether model with the specified <paramref name="id"/> is registered.
        /// </summary>
        /// <param name="id">A model id.</param>
        /// <returns>
        /// <b>true</b> if model with the specified <paramref name="id"/> is registered; otherwise <b>false</b>.
        /// </returns>
        public bool IsRegistered(int id)
        {
            using (var repository = this._factory.Create<TRepository>())
            {
                return repository.GetById(id) != null;
            }
        }

        #endregion //IService Members

        #region Protected Methods

        /// <summary>
        /// Occurs when the service tries to update a non-existent model.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        /// When a model doesn't exist in the system.
        /// </exception>
        protected virtual void OnUpdateEntityNotFoundException()
        {
            throw new EntityNotFoundException("The model doesn't exist. Nothing to update.");
        }

        #endregion //Protected Methods
    }
}