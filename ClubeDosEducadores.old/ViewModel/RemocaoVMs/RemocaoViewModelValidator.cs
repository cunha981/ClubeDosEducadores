using FluentValidation;
using Model;
using System;
using System.Linq;

namespace ViewModel.RemocaoVMs
{
    internal class RemocaoViewModelValidator : ViewModelValidator<Remocao, RemocaoViewModel>
    {
        public RemocaoViewModelValidator()
        {
            //RuleFor(a => a.VagaId).Must(vagaId =>
            //{
            //    return GetData<VagaRemocao>().Any(a => a.Id == vagaId && a.Data.Year == DateTime.Now.Year && a.CargoId == User.CargoId);
            //}).WithMessage("Não é possível salvar remoção para vagas inexistentes - {0}", a => a.VagaId);
        }
    }
}
