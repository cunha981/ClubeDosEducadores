using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class PerguntaFrequenteMapping : EntityTypeConfiguration<PerguntaFrequente>
    {
        public PerguntaFrequenteMapping()
        {
            ToTable("PerguntaFrequente");

            Property(a => a.Pergunta).IsRequired();
            Property(a => a.Resposta).IsRequired();
            Property(a => a.Tags).IsRequired();
        }
    }
}
