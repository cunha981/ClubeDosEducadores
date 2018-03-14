using FluentValidation;
using Helper;
using Model;
using System.Linq;

namespace ViewModel.UsuarioVMs
{
    public class UsuarioCadastrarValidator : ViewModelValidator<UsuarioFuncionario, UsuarioCadastrarVM>
    {
        public UsuarioCadastrarValidator()
        {
            RuleFor(a => a.Nome).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.Nome).Length(4, 200).WithMessage("Digite seu nome completo");

            RuleFor(a => a.CargoId).Must(cargoId =>
                {
                    return GetData<Cargo>().Any(a => a.Id == cargoId);
                }).WithMessage("Cargo não encontrado");

            RuleFor(a => a.Email).Must(email =>
                {
                    return email.IsValidEmail();
                }).WithMessage("E-mail inválido");

            RuleFor(a => a.Email).Must(email =>
                {
                    return !Data.Any(a => a.Email == email);
                }).WithMessage("E-mail já cadastrado");

            RuleFor(a => a.Senha).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.ReSenha).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a).Must(model =>
                {
                    return model.Senha == model.ReSenha;
                }).WithMessage("Senhas não conferem").WithName("ReSenha");
        }
    }
}