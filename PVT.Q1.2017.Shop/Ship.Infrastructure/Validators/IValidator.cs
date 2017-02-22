using Shop.Infrastructure.Models;

namespace Shop.Infrastructure.Validators
{
    public interface IValidator<in T> where T: BaseEntity
    {
        void Validate(T entity);

        bool IsValid(T entity);
    }
}