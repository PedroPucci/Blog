using Blog.Domain.Entity;
using Blog.Shared.Enums;
using Blog.Shared.Helpers;
using FluentValidation;

namespace Blog.Shared.Validator
{
    public class PublicationRequestValidator : AbstractValidator<PublicationEntity>
    {
        public PublicationRequestValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                    .WithMessage(PublicationErrors.Publication_Error_PublicationCanNotBeNullOrEmpty.Description())
                .MinimumLength(3)
                    .WithMessage(PublicationErrors.Publication_Error_PublicationTooShort.Description())
                .MaximumLength(100)
                    .WithMessage(PublicationErrors.Publication_Error_PublicationTooLong.Description());
        }
    }
}