using Helper;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.PerguntaFrequenteVMs
{
    public static class PerguntaFrequenteViewModelExtensions
    {
        public static IEnumerable<PerguntaFrequenteVM> ToListViewModel(this IEnumerable<PerguntaFrequente> models)
        {
            return models.Select(model => new PerguntaFrequenteVM
            {
                Id = model.Id,
                Pergunta = model.Pergunta,
                Resposta = model.Resposta,
                Tags = model.Tags
            }).OrderByDescending(a => a.Id).ToArray();
        }

        public static PerguntaFrequenteVM ToViewModel(this PerguntaFrequente model)
        {
            return model.ConvertTo<PerguntaFrequenteVM>();
        }
    }
}
