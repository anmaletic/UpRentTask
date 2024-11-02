namespace UpRentTask.Library.Validators;

public class UserValidator : AbstractValidator<UserModel>
{
    public UserValidator()
    {
        RuleFor(user => user.Username).NotEmpty().WithMessage("Korisničko ime je obavezno");
        RuleFor(user => user.Roles).NotEmpty().WithMessage("Korisnik mora pripadati barem jednoj grupi");
    }
}