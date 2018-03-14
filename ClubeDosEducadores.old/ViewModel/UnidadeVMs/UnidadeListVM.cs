using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.UnidadeVMs
{
    public class UnidadeListVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Tipo { get; set; }
        public int? DificilAcesso { get; set; }
        public double? Distancia { get; set; }
    }
}
