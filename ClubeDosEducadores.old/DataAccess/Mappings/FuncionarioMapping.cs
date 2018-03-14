using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    public class FuncionarioMapping : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMapping()
        {
            ToTable("Funcionario");

            Property(a => a.Nome).IsRequired();

            HasRequired(a => a.Cargo)
                .WithMany(b => b.Funcionarios)
                .HasForeignKey(a => a.CargoId);

            HasRequired(a => a.Usuario)
                .WithRequiredDependent(b => b.Funcionario);

            HasOptional(a => a.UnidadeTrabalho)
                .WithMany(b => b.Funcionarios)
                .HasForeignKey(a => a.UnidadeTrabalhoId)
                .WillCascadeOnDelete(false);
        }
    }
}