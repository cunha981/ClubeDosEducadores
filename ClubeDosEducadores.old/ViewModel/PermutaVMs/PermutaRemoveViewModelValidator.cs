using FluentValidation;
using Model;
using System.Linq;

namespace ViewModel.PermutaVMs
{
    class PermutaRemoveViewModelValidator : ViewModelValidator<Permuta, PermutaRemoveVM>
    {
        public PermutaRemoveViewModelValidator()
        {
            RuleFor(a => a).Must(model =>
            {
                return Data.Any(a => a.FuncionarioId == User.Id && !a.DtExclusao.HasValue);
            }).WithMessage("Não existe proposta de permuta ativa em seu nome para exclusão");

            RuleFor(a => a.MotivoExclusao).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
