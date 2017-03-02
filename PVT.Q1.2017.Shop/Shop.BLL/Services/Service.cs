namespace Shop.BLL.Services
{
    using System;
    using Exceptions;
    using Infrastructure.Models;
    using Infrastructure.Repositories;
    using Infrastructure.Services;
    using Infrastructure.Validators;

    /// <summary>
    /// The model service.
    /// </summary>
    public abstract class Service<TRepository, TEntity> : IService<TEntity> where TEntity : BaseEntity, new()
                                                                   where TRepository : IRepository<TEntity>
    {
        #region Fields

        /// <summary>
        /// The models repository.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// The models validator.
        /// </summary>
        private readonly IValidator<TEntity> _validator;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Service{TRepository, TEntity}"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">
        /// The model validator.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="repositoryFactory"/> is null or <paramref name="validator"/> is null.
        /// </exception>
        protected Service(IRepositoryFactory repositoryFactory, IValidator<TEntity> validator)
        {
            if (repositoryFactory == null)
            {
                throw new ArgumentNullException(nameof(repositoryFactory));
            }

            this._repositoryFactory = repositoryFactory;
            this._validator = validator;
        }

        #endregion //Constructors

        #region Properties

        /// <summary>
        /// Gets the repository factory.
        /// </summary>
        protected IRepositoryFactory RepositoryFactory
        {
            get { return this._repositoryFactory; }
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

            using (var repository = this._repositoryFactory.CreateRepository<TRepository>())
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

            using (var repository = this._repositoryFactory.CreateRepository<TRepository>())
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

            using (var repository = this._repositoryFactory.CreateRepository<TRepository>())
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

            using (var repository = this._repositoryFactory.CreateRepository<TRepository>())
            {
                return repository.GetById(model.Id) != null;
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