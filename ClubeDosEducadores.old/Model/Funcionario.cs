using System.Collections.Generic;

namespace Model
{
    public class Funcionario : IKey
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public int? UnidadeTrabalhoId { get; set; }
        public virtual Unidade UnidadeTrabalho { get; set; }

        public string Telefone { get; set; }
        public string Celular { get; set; }

        public virtual UsuarioFuncionario Usuario { get; set; }
        public virtual ICollection<Remocao> Remocoes { get; set; }
        public virtual ICollection<Permuta> Permutas { get; set; }
        public virtual ICollection<UnidadeAvaliacao> Avaliacoes { get; set; }
    }
}