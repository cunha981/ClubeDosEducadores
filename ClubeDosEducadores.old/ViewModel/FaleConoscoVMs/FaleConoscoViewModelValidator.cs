using FluentValidation;

namespace ViewModel.FaleConoscoVMs
{
    internal class FaleConoscoViewModelValidator : AbstractValidator<FaleConoscoVM>
    {
        public FaleConoscoViewModelValidator()
        {
            RuleFor(a => a.Assunto).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Mensagem).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
