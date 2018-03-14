using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    internal class UsuarioNotificacaoMapping : EntityTypeConfiguration<UsuarioNotificacao>
    {
        public UsuarioNotificacaoMapping()
        {
            ToTable("UsuarioNotificacao");

            HasKey(a => new { a.UsuarioId, a.TipoEvento });

            HasRequired(a => a.Usuario)
                .WithMany(b => b.Notificacoes)
                .HasForeignKey(a => a.UsuarioId)
                .WillCascadeOnDelete(false);
        }
    }
}
