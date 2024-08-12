
using FluentValidation;
using Para.Schema.Entities.DTOs.Category;

namespace Para.Business.Validators.Category;

public class CategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Category description is required.");
    }
}
