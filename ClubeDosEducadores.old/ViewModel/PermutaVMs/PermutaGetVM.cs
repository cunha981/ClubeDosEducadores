using System;
using System.Collections.Generic;
using ViewModel.UnidadeAvaliacaoVMs;
using ViewModel.UnidadeVMs;

namespace ViewModel.PermutaVMs
{
    public class PermutaGetVM
    {
        public int Id { get; set; }
        public string ObservacaoFuncionario { get; set; }
        public string ObservacaoPermuta { get; set; }
        public DateTime DtPublicacao { get; set; }

        public string FuncionarioNome { get; set; }
        public string FuncionarioEmail { get; set; }
        public string FuncionarioTelefone { get; set; }
        public string FuncionarioCelular { get; set; }

        public UnidadeGetVM Unidade { get; set; }
        public IEnumerable<UnidadeAvaliacaoListVM> Avaliacoes { get; set; }

        public IEnumerable<string> Regioes { get; set; }
        public IEnumerable<string> TiposUnidade { get; set; }
    }
}
