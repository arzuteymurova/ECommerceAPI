using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<CreateProductViewModel>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Enter product name, please!")
                .MaximumLength(100)
                .MinimumLength(5)
                    .WithMessage("Product name length must be between 5 and 150 characters!");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Enter stock number, please!")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Stock number can't be negative!");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Enter product price, please!")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Price can't be negative!");


        }
    }
}
