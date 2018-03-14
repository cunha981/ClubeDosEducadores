using FluentValidation;
using Model;
using System.Linq;

namespace ViewModel.UsuarioVMs
{
    public class UsuarioLoginValidator : ViewModelValidator<UsuarioFuncionario, UsuarioLoginVM>
    {
        public UsuarioLoginValidator()
        {
            RuleFor(a => a.Email).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Senha).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a).Must(vm =>
            {
                return Data.Any(a => a.Email == vm.Email && a.Senha == vm.Senha && a.Ativo);
            }).WithMessage("E-mail ou Senha incorreto").WithName("Email");
        }
    }
}