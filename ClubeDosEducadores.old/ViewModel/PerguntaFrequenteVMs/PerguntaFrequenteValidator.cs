using FluentValidation;
using Model;
using System.Linq;

namespace ViewModel.PerguntaFrequenteVMs
{
    internal class PerguntaFrequenteValidator : ViewModelValidator<PerguntaFrequente, PerguntaFrequenteVM>
    {
        public PerguntaFrequenteValidator()
        {
            RuleFor(a => a.Pergunta).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Resposta).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Tags).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.Pergunta).Must((vm, pergunta) =>
            {
                return !Data.Any(a => a.Id != vm.Id && a.Pergunta == pergunta);
            }).WithMessage("Pergunta já cadastrada");
        }
    }
}
