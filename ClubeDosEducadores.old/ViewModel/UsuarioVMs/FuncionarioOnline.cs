using Model;

namespace ViewModel.UsuarioVMs
{
    public class FuncionarioOnline : IUser
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string SessionId { get; set; }

        public int CargoId { get; set; }
        public int? TipoUnidadeId { get; set; }
        public int? RegiaoUnidadeId { get; set; }
        public int? UnidadeId { get; set; }
        public string Cargo { get; set; }
    }
}