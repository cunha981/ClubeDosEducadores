using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    internal class UsuarioAtributoMapping : EntityTypeConfiguration<UsuarioAtributo>
    {
        public UsuarioAtributoMapping()
        {
            ToTable("UsuarioAtributo");

            HasKey(a => new { a.UsuarioId, a.Atributo });

            HasRequired(a => a.Usuario)
                .WithMany(b => b.Atributos)
                .HasForeignKey(a => a.UsuarioId)
                .WillCascadeOnDelete(false);
        }
    }
}
