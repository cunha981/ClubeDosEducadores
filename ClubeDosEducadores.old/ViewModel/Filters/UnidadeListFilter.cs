using System.Collections.Generic;

namespace ViewModel.Filters
{
    public class UnidadeListFilter
    {
        public IEnumerable<int> Tipos { get; set; } = new HashSet<int>();
        public string Filtro { get; set; }
        public int? Distancia { get; set; }
        public int Page { get; set; } = 1;
    }
}
