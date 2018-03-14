using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class NoticiaMapping : EntityTypeConfiguration<Noticia>
    {
        public NoticiaMapping()
        {
            ToTable("Noticia");

            Property(a => a.Titulo).IsRequired();
            Property(a => a.Conteudo).IsRequired();
        }
    }
}
