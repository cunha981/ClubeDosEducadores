using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class RegiaoUnidadeMapping : EntityTypeConfiguration<RegiaoUnidade>
    {
        public RegiaoUnidadeMapping()
        {
            ToTable("RegiaoUnidade");

            Property(a => a.Regiao).IsRequired();
        }
    }
}
