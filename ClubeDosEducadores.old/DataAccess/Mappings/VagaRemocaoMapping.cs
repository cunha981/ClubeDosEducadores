using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    public class VagaRemocaoMapping : EntityTypeConfiguration<VagaRemocao>
    {
        public VagaRemocaoMapping()
        {
            ToTable("VagaRemocao");

            HasRequired(a => a.Unidade)
                .WithMany(b => b.Vagas)
                .HasForeignKey(a => a.UnidadeId);

            HasRequired(a => a.Cargo)
                .WithMany(b => b.Vagas)
                .HasForeignKey(a => a.CargoId);
        }
    }
}