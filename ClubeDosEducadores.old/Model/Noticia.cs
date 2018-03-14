using System;

namespace Model
{
    public class Noticia : IKey
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DtPublicacao { get; set; } = DateTime.Now;
    }
}
