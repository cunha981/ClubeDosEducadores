using FluentValidation;
using Model;
using System.Linq;

namespace ViewModel.PermutaVMs
{
    class PermutaEditViewModelValidator : ViewModelValidator<Permuta, PermutaEditVM>
    {
        public PermutaEditViewModelValidator()
        {
            When(a => a.Id == 0, () =>
            {
                RuleFor(a => a).Must(model =>
                {
                    return !Data.Any(a => a.FuncionarioId == User.Id && !a.DtExclusao.HasValue);
                }).WithMessage("Não é possível criar novas permutas enquanto houver uma ativa!");
            });

            RuleFor(a => a.ObservacaoFuncionario).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.ObservacaoPermuta).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Regioes).NotEmpty().WithMessage("Selecione as região que tem preferência para permuta");
            RuleFor(a => a.TiposUnidade).NotEmpty().WithMessage("Selecione os tipos de unidade que tem preferência para permuta");
        }
    }
}
