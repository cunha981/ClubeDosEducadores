using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.PermutaVMs
{
    public class PermutaEditVM
    {
        public int Id { get; set; }
        public string ObservacaoFuncionario { get; set; }
        public string ObservacaoPermuta { get; set; }
        public DateTime DtPublicacao { get; set; } = DateTime.Now;
        public IEnumerable<int> Regioes { get; set; } = new HashSet<int>();
        public IEnumerable<int> TiposUnidade { get; set; } = new HashSet<int>();
    }
}
