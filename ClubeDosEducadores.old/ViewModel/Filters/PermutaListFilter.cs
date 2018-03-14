using System.Collections.Generic;

namespace ViewModel.Filters
{
    public class PermutaListFilter
    {
        public IEnumerable<int> Regioes { get; set; } = new HashSet<int>();
        public IEnumerable<int> Tipos { get; set; } = new HashSet<int>();
        public int Page { get; set; } = 1;
    }
}
