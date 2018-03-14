using System;
using System.Collections.Generic;

namespace Model
{
    public class UsuarioFuncionario : IKey
    {
        public int Id { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string SessionId { get; set; }
        public DateTime? DtAtividade { get; set; }
        public bool Ativo { get; set; } = true;

        public bool SendEmailPermuta { get; set; } = true; //Enviar email quando houver permutas compativeis

        public virtual ICollection<UsuarioAtributo> Atributos { get; set; }
        public virtual ICollection<UsuarioNotificacao> Notificacoes { get; set; }
    }
}