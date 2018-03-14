using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    public class CargoMapping : EntityTypeConfiguration<Cargo>
    {
        public CargoMapping()
        {
            ToTable("Cargo");
            Property(a => a.Descricao).IsRequired();
            Property(a => a.Abreviacao).IsRequired();
        }
    }
}