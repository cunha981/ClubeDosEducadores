using System.Collections.Generic;

namespace Model
{
    public class Cargo : IKey
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Abreviacao { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<VagaRemocao> Vagas { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
    }
}