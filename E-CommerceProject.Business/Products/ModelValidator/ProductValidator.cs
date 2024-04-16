using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using FluentValidation;
namespace E_CommerceProject.Business.Products.ModelValidator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProductValidator(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(NameIsUnique)
                .WithMessage("Name is already exist");;

            RuleFor(c => c.Description)
                .MaximumLength(500)
                .NotEmpty();

            RuleFor(c => c.Price)
                .NotEmpty()
                .GreaterThan(100);

            RuleFor(c => c.ProductImages)
                .NotEmpty();
            
        }

        private async Task<bool> NameIsUnique(Product entity, string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.ProductsRepository.IsNameExist(entity);
        }
    }
}
