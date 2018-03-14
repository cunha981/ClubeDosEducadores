using Model.Enums;

namespace Model
{
    public class UsuarioAtributo
    {
        public int UsuarioId { get; set; }
        public virtual UsuarioFuncionario Usuario { get; set; }
        public UsuarioAtributoEnum Atributo { get; set; }
        public string Descricao {
            get
            {
                return Atributo.ToString();
            }
        }
    }
}
