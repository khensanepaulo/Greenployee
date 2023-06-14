using FluentValidation;
using Greenployee.MODELS.DTO;

namespace Greenployee.MODELS.Validation
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.dsLogin)
                .NotEmpty()
                .NotNull()
                .WithMessage("Login deve ser informado!");

            RuleFor(x => x.dsSenha)
                .NotEmpty()
                .NotNull()
                .WithMessage("Senha deve ser informada!");
        }
    }
}
