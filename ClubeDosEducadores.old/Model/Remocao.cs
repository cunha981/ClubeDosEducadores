using System;

namespace Model
{
    public class Remocao
    {
        public int VagaRemocaoId { get; set; }
        public VagaRemocao VagaRemocao { get; set; }
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public int Preferencia { get; set; }
        public DateTime Data { get; set; }
    }
}