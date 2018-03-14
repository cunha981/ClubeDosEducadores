using System;
using System.Collections.Generic;

namespace Model
{
    public class VagaRemocao : IKey
    {
        public int Id { get; set; }
        public int UnidadeId { get; set; }
        public virtual Unidade Unidade { get; set; }
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }
        public int Vagas { get; set; }
        public int? VagasPotenciais { get; set; }
        public int? VagasIniciais { get; set; }
        public DateTime Data { get; set; }
        public virtual ICollection<Remocao> Remocoes { get; set; }
    }
}