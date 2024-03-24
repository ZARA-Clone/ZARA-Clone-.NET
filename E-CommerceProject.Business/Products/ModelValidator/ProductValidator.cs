using E_CommerceProject.Models;
using FluentValidation;
namespace E_CommerceProject.Business.Products.ModelValidator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(c => c.Description)
                .MaximumLength(500)
                .NotEmpty();

            RuleFor(c => c.Price)
                .NotEmpty()
                .GreaterThan(0);
               
            RuleFor(c => c.Quantity)
                .NotEmpty();
        }
    }
}
