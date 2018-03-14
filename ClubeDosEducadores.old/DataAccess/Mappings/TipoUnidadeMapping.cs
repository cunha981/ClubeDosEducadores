using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    public class TipoUnidadeMapping : EntityTypeConfiguration<TipoUnidade>
    {
        public TipoUnidadeMapping()
        {
            ToTable("TipoUnidade");

            Property(a => a.Descricao).IsRequired();
            Property(a => a.Tipo).IsRequired();
        }
    }
}