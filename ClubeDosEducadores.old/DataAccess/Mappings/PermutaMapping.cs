using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class PermutaMapping : EntityTypeConfiguration<Permuta>
    {
        public PermutaMapping()
        {
            ToTable("Permuta");

            Property(a => a.ObservacaoFuncionario).IsRequired();
            Property(a => a.ObservacaoPermuta).IsRequired();
            Property(a => a.DtPublicacao).IsRequired();

            HasRequired(a => a.Funcionario)
                .WithMany(b => b.Permutas)
                .HasForeignKey(a => a.FuncionarioId);
        }
    }
}
