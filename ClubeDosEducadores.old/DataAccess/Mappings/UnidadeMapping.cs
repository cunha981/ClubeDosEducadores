using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    public class UnidadeMapping : EntityTypeConfiguration<Unidade>
    {
        public UnidadeMapping()
        {
            ToTable("Unidade");

            HasKey(a => new { a.Id });

            Property(a => a.Nome).IsRequired();

            HasRequired(a => a.Endereco)
                .WithRequiredPrincipal(b => b.Unidade);

            HasRequired(a => a.TipoUnidade)
                .WithMany(b => b.Unidades)
                .HasForeignKey(a => a.TipoUnidadeId);

            HasOptional(a => a.RegiaoUnidade)
                .WithMany(b => b.Unidades)
                .HasForeignKey(a => a.RegiaoUnidadeId);
        }
    }
}