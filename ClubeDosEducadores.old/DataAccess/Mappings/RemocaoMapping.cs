using Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    public class RemocaoMapping : EntityTypeConfiguration<Remocao>
    {
        public RemocaoMapping()
        {
            ToTable("Remocao");

            HasKey(a => new { a.VagaRemocaoId, a.FuncionarioId });

            HasRequired(a => a.VagaRemocao)
                .WithMany(b => b.Remocoes)
                .HasForeignKey(a => a.VagaRemocaoId)
                .WillCascadeOnDelete(false);

            HasRequired(a => a.Funcionario)
                .WithMany(b => b.Remocoes)
                .HasForeignKey(a => a.FuncionarioId);

            Property(a => a.FuncionarioId).HasColumnAnnotation(
                IndexAnnotation.AnnotationName, 
                new IndexAnnotation(
                    new IndexAttribute("IX_Remocao_FuncionarioId_VagaRemocaoId_Preferencia", 0) { IsUnique = true }));

            Property(a => a.VagaRemocaoId).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_Remocao_FuncionarioId_VagaRemocaoId_Preferencia", 1) { IsUnique = true }));

            Property(a => a.Preferencia).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_Remocao_FuncionarioId_VagaRemocaoId_Preferencia", 2) { IsUnique = true }));
        }
    }
}