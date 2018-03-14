using Model.Enums;
using System.Collections.Generic;

namespace ViewModel.FuncionarioVMs
{
    public class FuncionarioEditVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargoId { get; set; }

        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }

        public int? UnidadeTrabalhoId { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Email { get; set; }
        public string Senha { get; set; }
        public string ReSenha { get; set; }

        public bool SendEmailPermuta { get; set; } = true;

        public ICollection<TipoEventoEnum> Notificacoes { get; set; }
    }
}