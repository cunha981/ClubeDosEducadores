using Model;
using System.Linq;

namespace ViewModel.TipoUnidadeVMs
{
    public static class TipoUnidadeViewModelExtensions
    {
        public static IQueryable<TipoUnidadeOptionVM> ToOptions(this IQueryable<TipoUnidade> models)
        {
            return models.Select(a => new TipoUnidadeOptionVM
            {
                Id = a.Id,
                Tipo = a.Tipo
            });
        }
    }
}
