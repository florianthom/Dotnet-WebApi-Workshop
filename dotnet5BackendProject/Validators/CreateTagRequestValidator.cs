using dotnet5BackendProject.Contracts.V1.Requests;
using FluentValidation;

namespace dotnet5BackendProject.Validators
{
    public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
    {
        public CreateTagRequestValidator()
        {
            RuleFor(a => a.TagName)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}