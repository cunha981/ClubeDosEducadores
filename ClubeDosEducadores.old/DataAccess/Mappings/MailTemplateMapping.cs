using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class MailTemplateMapping : EntityTypeConfiguration<MailTemplate>
    {
        public MailTemplateMapping()
        {
            ToTable("MailTemplate");

            HasKey(a => a.Key);
            Property(a => a.Subject).IsRequired();
            Property(a => a.Body).IsRequired();
        }
    }
}
