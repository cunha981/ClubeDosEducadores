using FluentValidation;
using Model;
using System;
using System.Linq;

namespace ViewModel.RemocaoVMs
{
    internal class RemocoesViewModelValidator : ViewModelValidator<Remocao, RemocoesViewModel>
    {
        public RemocoesViewModelValidator()
        {
            RuleFor(a => a.Remocoes).Must((models) =>
            {
                return !models.GroupBy(vagaId => vagaId).Any(a => a.Count() > 1);
            }).WithMessage("Não é possível salvar linhas repetidas");

            RuleFor(a => a.Remocoes).Must((models) =>
            {
                var user = User;
                var vagasId = models.Select(a => a.VagaId);
                return GetData<VagaRemocao>().Any(a => vagasId.Any(b => a.Id == b && a.Data.Year == DateTime.Now.Year && a.CargoId == User.CargoId && a.Vagas > 0));
            }).WithMessage("Não é possível salvar remoção para vagas inexistentes");

            RuleFor(a => a.Remocoes).SetCollectionValidator(new RemocaoViewModelValidator());
        }
    }
}
