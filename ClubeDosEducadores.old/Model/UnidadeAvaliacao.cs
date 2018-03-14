using System;

namespace Model
{
    public class UnidadeAvaliacao : IKey
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public int Nota { get; set; }
        public DateTime DtAvaliacao { get; set; } = DateTime.Now;

        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public int UnidadeId { get; set; }
        public virtual Unidade Unidade { get; set; }
    }
}
