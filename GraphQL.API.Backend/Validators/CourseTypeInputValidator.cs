using FluentValidation;
using GraphQL.API.Backend.Models;

namespace GraphQL.API.Backend.Validators
{
    public class CourseTypeInputValidator : AbstractValidator<CourseInputType>
    {
        public CourseTypeInputValidator()
        {
            RuleFor(c => c.Name)
                .MinimumLength(3)
                .MaximumLength(20)
                .NotEmpty()
                .WithMessage("Название курса должно имет длину более 3 символов но менее 50")
                .WithErrorCode("COURSE_NAME_LENGTH");

            RuleFor(c => c.Subject)
                .NotEmpty()
                .NotNull()
                .WithErrorCode("SUBJECT_NAME_EMPTY_OR_NULL");
        }
    }
}
