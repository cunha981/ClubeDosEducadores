using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Filters
{
    public class RemocaoListFilter
    {
        public IEnumerable<int> Tipos { get; set; }
        public int? Distancia { get; set; }
        public IEnumerable<int?> DificilAcessos { get; set; }
        public int Linhas { get; set; } = 10;
        public string Ordenacao { get; set; } = "Distancia";
    }
}
