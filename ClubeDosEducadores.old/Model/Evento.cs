using Model.Enums;
using System;

namespace Model
{
    public class Evento : IKey
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Resumo { get; set; }
        public TipoEventoEnum TipoEvento { get; set; }
        public string TipoEventoDescricao
        {
            get
            {
                return TipoEvento.ToString();
            }
        }
        public bool EnviarEmail { get; set; }
        public string Url { get; set; }
        public DateTime DtEvento { get; set; }
        public DateTime DtCadastro { get; set; } = DateTime.Now;
    }
}
