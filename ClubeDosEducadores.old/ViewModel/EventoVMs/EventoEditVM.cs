using Model;
using Model.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ViewModel.EventoVMs
{
    public class EventoEditVM : IKey
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        [AllowHtml]
        public string Descricao { get; set; }
        public TipoEventoEnum TipoEvento { get; set; }
        public bool EnviarEmail { get; set; } = false;
        public string Url { get; set; }
        [DisplayName("Data do Evento"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DtEvento { get; set; } = DateTime.Now;
    }
}
