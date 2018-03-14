using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    internal class EventoMapping : EntityTypeConfiguration<Evento>
    {
        public EventoMapping()
        {
            ToTable("Evento");

            Property(a => a.Titulo).IsRequired();
            Property(a => a.Descricao).IsRequired();
            Property(a => a.Resumo).IsRequired();
        }
    }
}
