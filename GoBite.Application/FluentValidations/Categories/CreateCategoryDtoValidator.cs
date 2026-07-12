using FluentValidation;
using GoBite.Application.DTOs.Categories;

namespace GoBite.Application.FluentValidations.Categories;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500).WithMessage("Image URL must not exceed 500 characters")
            .When(x => x.ImageUrl is not null);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0).WithMessage("Display order must be zero or greater");
    }
}
