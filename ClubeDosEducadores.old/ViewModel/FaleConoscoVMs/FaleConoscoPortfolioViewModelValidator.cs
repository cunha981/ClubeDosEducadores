using FluentValidation;
using Helper;

namespace ViewModel.FaleConoscoVMs
{
    class FaleConoscoPortfolioViewModelValidator : AbstractValidator<FaleConoscoPortfolioVM>
    {
        public FaleConoscoPortfolioViewModelValidator()
        {
            RuleFor(a => a.Nome).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Email).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Celular).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Mensagem).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.Email).Must(a => a.IsValidEmail()).WithMessage("Digite um e-mail válido");
        }
    }
}
