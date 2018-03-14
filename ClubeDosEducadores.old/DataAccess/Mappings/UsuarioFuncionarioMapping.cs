using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    public class UsuarioFuncionarioMapping : EntityTypeConfiguration<UsuarioFuncionario>
    {
        public UsuarioFuncionarioMapping()
        {
            ToTable("UsuarioFuncionario");

            Property(a => a.Email).IsRequired();
            Property(a => a.Senha).IsRequired();
        }
    }
}