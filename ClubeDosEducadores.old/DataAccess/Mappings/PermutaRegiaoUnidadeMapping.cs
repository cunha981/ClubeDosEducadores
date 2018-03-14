using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class PermutaRegiaoUnidadeMapping : EntityTypeConfiguration<PermutaRegiaoUnidade>
    {
        public PermutaRegiaoUnidadeMapping()
        {
            ToTable("PermutaRegiaoUnidade");

            HasKey(a => new { a.PermutaId, a.RegiaoUnidadeId });

            HasRequired(a => a.Permuta)
                .WithMany(b => b.Regioes)
                .HasForeignKey(a => a.PermutaId);

            HasRequired(a => a.RegiaoUnidade)
                .WithMany(b => b.PermutasRegiaoUnidade)
                .HasForeignKey(a => a.RegiaoUnidadeId);
        }
    }
}
