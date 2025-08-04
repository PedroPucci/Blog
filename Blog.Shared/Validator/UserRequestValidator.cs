using Blog.Domain.Entity;
using Blog.Shared.Enums;
using Blog.Shared.Helpers;
using FluentValidation;

namespace Blog.Shared.Validator
{
    public class UserRequestValidator : AbstractValidator<UserEntity>
    {
        public UserRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                    .WithMessage(UserErrors.User_Error_EmailCanNotBeNullOrEmpty.Description())
                .MinimumLength(4)
                    .WithMessage(UserErrors.User_Error_EmailLenghtLessFour.Description())
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                        .WithMessage(UserErrors.User_Error_InvalidEmailFormat.Description());
        }
    }
}