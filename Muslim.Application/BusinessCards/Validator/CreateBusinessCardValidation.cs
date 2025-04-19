using FluentValidation;
using Muslim.Domain.BusinessCards.Dtos;

namespace Muslim.Application.BusinessCards.Validator;



public class CreateBusinessCardValidation : AbstractValidator<CreateBusinessCardDto>
{
    public CreateBusinessCardValidation()
    {
        //RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

    }
}

