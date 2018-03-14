using Model;

namespace ViewModel.UsuarioVMs
{
    public class AdminOnline : IUser
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SessionId { get; set; }
    }
}
