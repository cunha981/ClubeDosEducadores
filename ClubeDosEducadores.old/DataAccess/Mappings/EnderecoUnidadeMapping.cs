using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    public class EnderecoUnidadeMapping : EntityTypeConfiguration<EnderecoUnidade>
    {
        public EnderecoUnidadeMapping()
        {
            ToTable("EnderecoUnidade");
            Property(a => a.EnderecoCompleto).IsRequired();
        }
    }
}