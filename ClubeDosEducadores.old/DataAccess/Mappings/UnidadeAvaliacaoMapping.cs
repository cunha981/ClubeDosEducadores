using Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class UnidadeAvaliacaoMapping : EntityTypeConfiguration<UnidadeAvaliacao>
    {
        public UnidadeAvaliacaoMapping()
        {

            ToTable("UnidadeAvaliacao");
            Property(a => a.DtAvaliacao).IsRequired();

            HasRequired(a => a.Funcionario)
                .WithMany(b => b.Avaliacoes)
                .HasForeignKey(a => a.FuncionarioId);

            HasRequired(a => a.Unidade)
                .WithMany(b => b.Avaliacoes)
                .HasForeignKey(a => a.UnidadeId);

            Property(a => a.FuncionarioId).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_UnidadeAvaliacao_FuncionarioId_UnidadeId", 0) { IsUnique = true }));

            Property(a => a.UnidadeId).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_UnidadeAvaliacao_FuncionarioId_UnidadeId", 1) { IsUnique = true }));

        }
    }
}
