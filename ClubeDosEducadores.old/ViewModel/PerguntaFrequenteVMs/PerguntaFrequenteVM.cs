using Model;
using System.Web.Mvc;

namespace ViewModel.PerguntaFrequenteVMs
{
    public class PerguntaFrequenteVM : IKey
    {
        public int Id { get; set; }
        public string Pergunta { get; set; }
        [AllowHtml]
        public string Resposta { get; set; }
        public string Tags { get; set; }
    }
}
