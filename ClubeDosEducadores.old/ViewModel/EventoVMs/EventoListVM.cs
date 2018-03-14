using Model.Enums;
using System;

namespace ViewModel.EventoVMs
{
    public class EventoListVM
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public TipoEventoEnum TipoEvento { get; set; }
        public string TipoEventoDescricao
        {
            get
            {
                return TipoEvento.ToString();
            }
        }
        public DateTime DtEvento { get; set; }
        public DateTime DtCadastro { get; set; }
    }
}
