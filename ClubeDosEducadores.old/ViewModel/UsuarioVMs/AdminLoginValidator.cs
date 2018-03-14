using FluentValidation;
using Model;
using System.Linq;

namespace ViewModel.UsuarioVMs
{
    class AdminLoginValidator : ViewModelValidator<UsuarioFuncionario, AdminLoginVM>
    {
        public AdminLoginValidator()
        {
            RuleFor(a => a.Email).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Senha).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a).Must(vm =>
            {
                return Data.Any(a => a.Email == vm.Email && a.Senha == vm.Senha && a.Ativo);
            }).WithMessage("E-mail ou Senha incorreto").WithName("Email");

            RuleFor(a => a.Email).Must(email =>
            {
                return Data.Any(a => a.Email == email && a.Atributos.Any(b => b.Atributo == Model.Enums.UsuarioAtributoEnum.Administrador));
            }).WithMessage("Você não tem os privilégios necessários!");
        }

    }
}
