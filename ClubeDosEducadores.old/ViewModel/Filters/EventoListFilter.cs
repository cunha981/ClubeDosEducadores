using Model.Enums;
using System;

namespace ViewModel.Filters
{
    public class EventoListFilter
    {
        public TipoEventoEnum? TipoEvento { get; set; }
        public string Filtro { get; set; }
        public DateTime? De { get; set; }
        public DateTime? Ate { get; set; }
        public int Page { get; set; } = 1;
    }
}
