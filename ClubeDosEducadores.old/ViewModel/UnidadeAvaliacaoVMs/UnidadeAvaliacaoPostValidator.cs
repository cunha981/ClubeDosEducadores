using FluentValidation;
using Model;
using System.Linq;

namespace ViewModel.UnidadeAvaliacaoVMs
{
    class UnidadeAvaliacaoPostValidator : ViewModelValidator<UnidadeAvaliacao, UnidadeAvaliacaoPostVM>
    {
        public UnidadeAvaliacaoPostValidator()
        {
            RuleFor(a => a.Comentario).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.Nota).GreaterThan(0).WithMessage("Dê uma nota para esta Unidade");
            RuleFor(a => a.Nota).LessThanOrEqualTo(5).WithMessage("A nota máxima é 5");

            RuleFor(a => a.UnidadeId).Must(unidadeId =>
            {
                return GetData<Unidade>().Any(a => a.Id == unidadeId);
            }).WithMessage("Unidade não encontrada");
        }
    }
}
