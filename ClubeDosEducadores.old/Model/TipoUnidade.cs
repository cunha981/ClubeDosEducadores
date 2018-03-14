using System.Collections.Generic;

namespace Model
{
    public class TipoUnidade : IKey
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Unidade> Unidades { get; set; }
        public virtual ICollection<PermutaTipoUnidade> PermutasTipoUnidade { get; set; }
    }
}