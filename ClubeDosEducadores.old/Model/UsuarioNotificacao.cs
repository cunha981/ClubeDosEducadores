using Model.Enums;

namespace Model
{
    /// <summary>
    /// Configuracao utilizada para informar se usuario deseja ou nao ser notificado
    /// de determinado tipo de evento
    /// </summary>
    public class UsuarioNotificacao
    {
        public TipoEventoEnum TipoEvento { get; set; }
        public string Descricao
        {
            get
            {
                return TipoEvento.ToString();
            }
        }
        public bool Notificar { get; set; } = true;
        public int UsuarioId { get; set; }
        public virtual UsuarioFuncionario Usuario { get; set; }
    }
}
