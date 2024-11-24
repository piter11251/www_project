using FluentValidation;

namespace TicketReservationSystem.DTO.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(TicketSystemDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Empty email")
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage("Password should contain at least 6 characters");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Both passwords must be equal");

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "This email is taken");
                    }
                });
        }
    }
}
