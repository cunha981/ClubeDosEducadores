using FluentValidation;
using Helper.RegexHelper;
using Model;
using System.Linq;

namespace ViewModel.FuncionarioVMs
{
    class FuncionarioEditValidator : ViewModelValidator<Funcionario, FuncionarioEditVM>
    {
        public FuncionarioEditValidator()
        {
            RuleFor(a => a.Nome).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.CargoId).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.Cep).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Cep).Must(cep =>
            {
                return cep.IsValidCep();
            }).WithMessage("Cep inválido");

            RuleFor(a => a.Logradouro).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Bairro).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Cidade).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Uf).NotEmpty().WithMessage("Obrigatório");
            RuleFor(a => a.Numero).NotEmpty().WithMessage("Obrigatório");

            RuleFor(a => a.Senha).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.ReSenha).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.Latitude).NotEmpty().WithMessage("Endereço não encontrado no mapa");
            RuleFor(a => a.Longitude).NotEmpty().WithMessage("Endereço não encontrado no mapa");

            RuleFor(a => a).Must(model =>
            {
                return model.Senha == model.ReSenha;
            }).WithMessage("Senhas não conferem").WithName("Senha");

            RuleFor(a => a.Email).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.Email).Must(email =>
            {
                return email.IsValidEmail();
            }).WithMessage("E-mail inválido");

            When(a => a.Telefone != null, () =>
            {
                RuleFor(a => a.Telefone).Must(telefone =>
                {
                    return telefone.IsValidTelephone();
                }).WithMessage("Telefone inválido");
            });

            When(a => a.Celular != null, () =>
            {
                RuleFor(a => a.Celular).Must(celular =>
                {
                    return celular.IsValidTelephone();
                }).WithMessage("Celular inválido");
            });

            When(a => a.UnidadeTrabalhoId.HasValue, () =>
            {
                RuleFor(a => a.UnidadeTrabalhoId).Must(unidadeId =>
                {
                    return GetData<Unidade>().Any(a => a.Id == unidadeId);
                }).WithMessage("Unidade não encontrada");
            });

        }
    }
}
