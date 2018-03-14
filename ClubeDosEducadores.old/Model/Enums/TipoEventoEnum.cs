using System.ComponentModel;

namespace Model.Enums
{
    public enum TipoEventoEnum
    {
        Indefinido,
        [Description("Cursos")]
        Curso,
        [Description("Notícias")]
        Noticia,
        [Description("Avisos")]
        Aviso,
        [Description("Concursos")]
        Concurso
    }
}
