using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using FluentValidation;

namespace E_CommerceProject.Business.Brands.Validator
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public BrandValidator(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
         
            RuleFor(c => c.Name)
               .NotEmpty()
               .MaximumLength(100)
                .MustAsync(NameIsUnique)
                .WithMessage("Name is already exist");
        }
        private async Task<bool> NameIsUnique(Brand entity, string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.BrandRepository.IsNameExist(entity);
        }
    }
}
