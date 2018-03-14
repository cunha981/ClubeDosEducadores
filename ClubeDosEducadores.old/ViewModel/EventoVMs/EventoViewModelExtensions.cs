using Helper;
using Model;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using ViewModel.Filters;

namespace ViewModel.EventoVMs
{
    public static class EventoViewModelExtensions
    {
        public static IEnumerable<EventoListVM> ToListVM(this IEnumerable<Evento> models, EventoListFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.TipoEvento.HasValue)
                {
                    models = models.Where(a => a.TipoEvento == filter.TipoEvento.Value);
                }

                if (!string.IsNullOrWhiteSpace(filter.Filtro))
                {
                    models = models.Where(a => a.Titulo.ToUpper().Contains(filter.Filtro.ToUpper()));
                }

                if (filter.De.HasValue)
                {
                    models = models.Where(a => a.DtEvento >= filter.De.Value);
                }

                if (filter.Ate.HasValue)
                {
                    models = models.Where(a => a.DtEvento <= filter.Ate.Value);
                }
            }

            return models.OrderByDescending(a => a.DtCadastro).Select(a => new EventoListVM
            {
                Id = a.Id,
                DtEvento = a.DtEvento,
                Resumo = a.Resumo,
                TipoEvento = a.TipoEvento,
                Titulo = a.Titulo,
                DtCadastro = a.DtCadastro
            });
        }

        public static IPagedList ToPagedList(this IEnumerable<Evento> models, EventoListFilter filter = null, int rows = 10)
        {
            return models.ToListVM(filter).OrderByDescending(a => a.DtEvento).ToPagedList(filter?.Page ?? 1, rows);
        }

        public static EventoEditVM ToEditVM(this Evento model)
        {
            return model.ConvertTo<EventoEditVM>();
        }
    }
}
