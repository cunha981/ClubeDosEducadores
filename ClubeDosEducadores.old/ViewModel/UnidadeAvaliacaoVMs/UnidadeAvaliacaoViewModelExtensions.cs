using Helper;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.UnidadeAvaliacaoVMs
{
    public static class UnidadeAvaliacaoViewModelExtensions
    {
        public static UnidadeAvaliacaoPostVM ToPostVM(this UnidadeAvaliacao model, int unidadeId)
        {
            var vm = model.ConvertTo<UnidadeAvaliacaoPostVM>();
            vm.UnidadeId = unidadeId;
            return vm;
        }

        public static IEnumerable<UnidadeAvaliacaoListVM> ToListVM(this IEnumerable<UnidadeAvaliacao> models)
        {
            return models.Select(a => new UnidadeAvaliacaoListVM
            {
                Comentario = a.Comentario,
                DtAvaliacao = a.DtAvaliacao,
                Nota = a.Nota
            }).OrderByDescending(a => a.DtAvaliacao);
        }
    }
}
