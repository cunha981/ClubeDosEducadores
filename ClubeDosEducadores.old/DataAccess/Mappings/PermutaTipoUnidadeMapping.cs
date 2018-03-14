using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class PermutaTipoUnidadeMapping : EntityTypeConfiguration<PermutaTipoUnidade>
    {
        public PermutaTipoUnidadeMapping()
        {
            ToTable("PermutaTipoUnidade");

            HasKey(a => new { a.PermutaId, a.TipoUnidadeId });

            HasRequired(a => a.Permuta)
                .WithMany(b => b.TiposUnidade)
                .HasForeignKey(a => a.PermutaId);

            HasRequired(a => a.TipoUnidade)
                .WithMany(b => b.PermutasTipoUnidade)
                .HasForeignKey(a => a.TipoUnidadeId);
        }
    }
}
