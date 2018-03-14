using System;
using System.Collections.Generic;

namespace Model
{
    public class Permuta : IKey
    {
        public int Id { get; set; }
        public string ObservacaoFuncionario { get; set; }
        public string ObservacaoPermuta { get; set; }
        public DateTime DtPublicacao { get; set; }
        public DateTime? DtExclusao { get; set; }
        public string MotivoExclusao { get; set; } //consegui a permuta, nao quero mais... ** Deve excluir para abrir outra

        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public virtual ICollection<PermutaRegiaoUnidade> Regioes { get; set; }
        public virtual ICollection<PermutaTipoUnidade> TiposUnidade { get; set; }
    }
}
