using Model;
using System.Linq;

namespace ViewModel.RegiaoUnidadeVMs
{
    public static class RegiaoUnidadeViewModelExtensions
    {
        public static IQueryable<RegiaoUnidadeOptionVM> ToOptions(this IQueryable<RegiaoUnidade> models)
        {
            return models.Select(a => new RegiaoUnidadeOptionVM
            {
                Id = a.Id,
                Regiao = a.Regiao
            });
        }
    }
}
