using System.Collections.Generic;

namespace Model
{
    public class Unidade : IKey
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string NomeDiarioOficial { get; set; }
        public string Email { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneSecundario { get; set; }
        public int? DificilAcesso { get; set; }
        public string Url { get; set; }
        public virtual EnderecoUnidade Endereco { get; set; }
        public int TipoUnidadeId { get; set; }
        public int? RegiaoUnidadeId { get; set; }
        public virtual TipoUnidade TipoUnidade { get; set; }
        public virtual RegiaoUnidade RegiaoUnidade { get; set; }
        public virtual ICollection<VagaRemocao> Vagas { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<UnidadeAvaliacao> Avaliacoes { get; set; }
    }
}